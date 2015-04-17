using SMSGatewayWpf.Core;
using SMSGatewayWpf.ViewModels.Configuration.Client;
using SMSGatewayWpf.ViewModels.Message.Comparer;
using SMSGatewayWpf.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Texaco.Database;
using Texaco.Database.Petapoco;

namespace SMSGatewayWpf.ViewModels.Message
{
    public class MessageCollaborator : BaseBindableModel, IDataSyncronize
    {

        private bool enable;
        private EventWaitHandle wait;
        private bool firstLoaded = true;

        private string inboxHeader;
        public string InboxHeader
        {
            get
            {
                return InboxHeader;
            }
            set
            {
                NotifyIfChanged(ref inboxHeader, value);
                if (InboxSource != null)
                {
                    if (InboxSource.Unread > 0)
                        inboxHeader = string.Format("inbox({0})", InboxSource.Unread);
                    else
                        inboxHeader = string.Format("inbox");
                }
            }
        }
           
        private InboxCollectionViewSource inbox;
        public InboxCollectionViewSource InboxSource
        {
            get
            {
                return inbox;
            }
            set
            {
                NotifyIfChanged(ref inbox, value);
            }
        }

        private OutboxCollectionViewSource outbox;
        public OutboxCollectionViewSource OutboxSource
        {
            get
            {
                return outbox;
            }
            set
            {
                NotifyIfChanged(ref outbox, value);
            }
        }

        private TrashCollectionViewSource trash;
        public TrashCollectionViewSource TrashSource
        {
            get
            {
                return trash;
            }
            set
            {
                NotifyIfChanged(ref trash, value);
            }
        }

        private SpamCollectionViewSource spam;
        public SpamCollectionViewSource SpamSource
        {
            get
            {
                return spam;
            }
            set
            {
                NotifyIfChanged(ref spam, value);
            }
        }

        private DraftCollectionViewSource draft;
        public DraftCollectionViewSource DraftSource
        {
            get
            {
                return draft;
            }
            set
            {
                NotifyIfChanged(ref draft, value);
            }
        }

        private QueueCollectionSource queue;
        public QueueCollectionSource QueueSource
        {
            get
            {
                return queue;
            }
            set
            {
                NotifyIfChanged(ref queue, value);
            }
        }

        private InboxCollectionViewSource InboxMaster { get; set; }
        private OutboxCollectionViewSource OutboxMaster { get; set; }
        private SpamCollectionViewSource SpamMaster { get; set; } 
        private TrashCollectionViewSource TrashMaster { get; set; }


        private string searchText;
        public string SearchText
        {
            get
            {
                return searchText;
            }
            set
            {
                NotifyIfChanged(ref searchText, value);
            }
        } 

        public ICommand SearchCommand { get; set; }

        public IMessageCommandBar ShowComposeDialog { get; set; }

        public MessageCollaborator()
        {
            InboxHeader = "inbox";

            wait = new EventWaitHandle(false, EventResetMode.AutoReset);
            SearchCommand = new DelegateCommand(new Action(OnSearch));

            InboxSource = new InboxCollectionViewSource();
            OutboxSource   = new OutboxCollectionViewSource();
            TrashSource = new TrashCollectionViewSource();
            SpamSource = new SpamCollectionViewSource();
            DraftSource = new DraftCollectionViewSource();

            ShowComposeDialog = new MessageCommandBar();
            InboxMaster = new InboxCollectionViewSource();
            OutboxMaster = new OutboxCollectionViewSource();
            TrashMaster = new TrashCollectionViewSource();
            SpamMaster = new SpamCollectionViewSource();
            DraftSource = new DraftCollectionViewSource();
            QueueSource = new QueueCollectionSource();
        }
         
        private void OnSearch()
        {
            Task.Factory.StartNew(() =>
                {
                    InboxSource.Source = MessageSearchEngine.Filter(InboxMaster.Source, searchText).Convert<Inbox>();
                    OutboxSource.Source = MessageSearchEngine.Filter(OutboxMaster.Source, searchText).Convert<Outbox>();
                    SpamSource.Source = MessageSearchEngine.Filter(SpamMaster.Source, searchText).Convert<Spam>();
                    TrashSource.Source = MessageSearchEngine.Filter(TrashMaster.Source, searchText).Convert<Trash>();
                }).ContinueWith(x => 
                {
                    ForceSyncronizing();                
                });
        }
         
        public void ForceSyncronizing()
        {
            wait.Set();
        }

        public void StartSyncronizing()
        {
            enable = true;
            Task.Factory.StartNew(() =>
            { 
                IDbManager dbManager = ObjectPool.Instance.Resolve<IDbManager>();
                IDataCommand db = dbManager.GetDatabase(DatabaseNames.SMSGW);

                List<Inbox> newInbox;
                List<Outbox> newOutbox;
                List<Trash> newTrash;
                List<Spam> newSpam;

                while (enable)
                {
                    newInbox = db.Query<Inbox>("GetInbox");
                    newOutbox = db.Query<Outbox>("GetOutbox");
                    newTrash = db.Query<Trash>("GetTrash");
                    newSpam = db.Query<Spam>("GetSpam");

                    // save current view source
                    InboxMaster.Source = newInbox.Convert<Inbox>();
                    OutboxMaster.Source = newOutbox.Convert<Outbox>();
                    SpamMaster.Source = newSpam.Convert<Spam>();
                    TrashMaster.Source = newTrash.Convert<Trash>();

                    DraftSource.Refresh();
                    QueueSource.Refresh(); 

                    // refresh all view source when search text box is empty
                    if (string.IsNullOrEmpty(searchText))
                    {

                        IEnumerable<Inbox> inboxAdded;
                        MutableObservableCollection<Inbox> inboxRemoved;

                        IEnumerable<Outbox> outboxAdded;
                        IEnumerable<Outbox> outboxRemoved;

                        IEnumerable<Spam> spamAdded;
                        IEnumerable<Spam> spamRemoved;

                        IEnumerable<Trash> trashAdded;
                        IEnumerable<Trash> trashRemoved;

                        if (firstLoaded)
                        {
                            firstLoaded = false;
                            InboxSource.Source = newInbox.Convert<Inbox>();
                            OutboxSource.Source = newOutbox.Convert<Outbox>();
                            SpamSource.Source = newSpam.Convert<Spam>();
                            TrashSource.Source = newTrash.Convert<Trash>();
                        }
                        else
                        {
                            if (newInbox.Count > InboxSource.Source.Count)
                            {
                                inboxAdded = newInbox.Except(InboxSource.Source, new InboxComparer());
                                if (inboxAdded.Any())
                                {
                                    foreach (Inbox message in inboxAdded)
                                    {
                                        InboxSource.Source.Insert(0, message);
                                    }
                                    InboxSource.Unread = inboxAdded.Count();
                                    InboxHeader = "inbox";

                                    ApplicationSettings settings = ObjectPool.Instance.Resolve<ApplicationSettings>();
                                    if (settings.General.Sounds)
                                    {
                                        IAudioService audio = ObjectPool.Instance.Resolve<IAudioService>();
                                        audio.PlayAsync(AudioEnum.ReceiveMessage);
                                    }
                                } 
                            }
                            else if (newInbox.Count < InboxSource.Source.Count)
                            {
                                inboxRemoved = InboxSource.Source.Except(newInbox, new InboxComparer()).Convert<Inbox>();
                                foreach (Inbox removed in inboxRemoved.ToList())
                                {
                                    InboxSource.Source.Remove(removed);
                                } 
                            }

                            if (newOutbox.Count > OutboxSource.Source.Count)
                            {
                                outboxAdded = newOutbox.Except(OutboxSource.Source, new OutboxComparer());
                                if (outboxAdded.Any())
                                {
                                    foreach (Outbox message in outboxAdded)
                                    {
                                        OutboxSource.Source.Insert(0, message);
                                    }
                                }
                                OutboxSource.Unread = outboxAdded.Count();
                            }
                            else if (newOutbox.Count < OutboxSource.Source.Count)
                            {
                                outboxRemoved = OutboxSource.Source.Except(newOutbox, new OutboxComparer());
                                foreach (Outbox removed in outboxRemoved.ToList())
                                {
                                    OutboxSource.Source.Remove(removed);
                                }  
                            }

                            if (newTrash.Count > TrashSource.Source.Count)
                            {
                                trashAdded = newTrash.Except(TrashSource.Source, new TrashComparer());
                                if (trashAdded.Any())
                                {
                                    foreach (Trash message in trashAdded)
                                    {
                                        TrashSource.Source.Insert(0, message);
                                    }
                                }
                                TrashSource.Unread = trashAdded.Count();
                            }
                            else if (newTrash.Count < TrashSource.Source.Count)
                            {
                                trashRemoved = TrashSource.Source.Except(newTrash, new TrashComparer());
                                foreach (Trash removed in trashRemoved.ToList())
                                {
                                    TrashSource.Source.Remove(removed);
                                }   
                            }
                             
                            if (newSpam.Count > SpamSource.Source.Count)
                            {
                                spamAdded = newSpam.Except(SpamSource.Source, new SpamComparer());
                                if (spamAdded.Any())
                                {
                                    foreach (Spam message in spamAdded)
                                    {
                                        SpamSource.Source.Insert(0, message);
                                    }
                                }
                                SpamSource.Unread = spamAdded.Count();
                            }
                            else if (newSpam.Count < SpamSource.Source.Count)
                            {
                                spamRemoved = SpamSource.Source.Except(newSpam, new SpamComparer());
                                foreach (Spam removed in spamRemoved.ToList())
                                {
                                    SpamSource.Source.Remove(removed);
                                }    
                            } 

                        } 
                            
                    }
                      
                    wait.WaitOne(3000);
                }
                db.Close();  
            });
        }
         
        public void StopSyncronizing()
        {
            enable = false;
        }
         
    }
}
