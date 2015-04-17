using SMSGatewayWpf.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Texaco.Database;
using Texaco.Database.Petapoco;

namespace SMSGatewayWpf.ViewModels.Message
{
    public abstract class BaseMessageViewModel<T> : BaseBindableModel, IMessageCommand
    {
        public ICommand MarkAsUnreadCommand { get; set; }
        public ICommand MarkAsReadCommand { get; set; }
        public ICommand MouseDoubleClickCommand { get; set; } 
        public ICommand DeleteCommand { get; set; }
        public ICommand MarkAsSpamCommand { get; set; }
        public ICommand ReplyCommand { get; set; }
        public ICommand CheckedHeaderCommand { get; set; }
        public ICommand CheckedRowCommand { get; set; }  
        public ICommand PermanentlyDeleteCommand { get; set; }
        public ICommand RestoreCommand { get; set; }
        public ICommand SelectionChangedCommand { get; set; }
        public ICommand ForwardCommand { get; set; }

        private bool checkedHeader;
        public bool CheckedHeader
        {
            get
            {
                return checkedHeader;
            }
            set
            {
                foreach (BaseMessageModel message in SelectableRow.ToList())
                {
                    message.IsSelected = checkedHeader;
                }
                NotifyIfChanged(ref checkedHeader, value);
            }
        }

        private MutableObservableCollection<BaseDataRow> selectableRow;

        public MutableObservableCollection<BaseDataRow> SelectableRow
        {
            get
            {
                return selectableRow;
            }
            set
            {
                NotifyIfChanged(ref selectableRow, value);
            }
        }

        private int unread;
        public int Unread
        {
            get
            {
                return unread;
            }
            set
            {
                NotifyIfChanged(ref unread, value);
            }
        }

        public abstract MutableObservableCollection<T> Source { get; set; }

        public BaseMessageViewModel()
        {
            MarkAsUnreadCommand = new DelegateCommand<object>(new Action<object>(OnMarkAsUnread));
            MarkAsReadCommand = new DelegateCommand<object>(new Action<object>(OnMarkAsRead));
            MouseDoubleClickCommand = new DelegateCommand<object>(new Action<object>(OnMouseDoubleClick));
            DeleteCommand = new DelegateCommand<string>(new Action<string>(OnDelete));
            MarkAsSpamCommand = new DelegateCommand<string>(new Action<string>(OnMarkAsSpam));
            ReplyCommand = new DelegateCommand<object>(new Action<object>(OnReply));
            CheckedHeaderCommand = new DelegateCommand<object>(new Action<object>(OnCheckedHeader));
            CheckedRowCommand = new DelegateCommand<object>(new Action<object>(OnCheckedRow));
            SelectionChangedCommand = new DelegateCommand<IList>(new Action<IList>(OnSelectionChangedCommand));
            PermanentlyDeleteCommand = new DelegateCommand(new Action(OnPermanentlyDelete));
            RestoreCommand = new DelegateCommand(new Action(OnRestore));
            ForwardCommand = new DelegateCommand<object>(new Action<object>(OnForward));
            SelectableRow = new MutableObservableCollection<BaseDataRow>();
        }

        private void OnMarkAsUnread(object obj)
        {
            MarkReadUnread((int)MessageRead.Unread);
            Unread = SelectableRow.Count;
        }

        private void OnMarkAsRead(object source)
        {
            MarkReadUnread((int)MessageRead.Read);
            Unread = 0;
        }

        private void MarkReadUnread(int read)
        { 
            Task.Factory.StartNew(() =>
            {
                IDbManager dbManager = ObjectPool.Instance.Resolve<IDbManager>();
                IDataCommand db = dbManager.GetDatabase(DatabaseNames.SMSGW);
                IsBusy = true;

                foreach (Inbox message in SelectableRow.ToList())
                {
                    message.IsRead = read;
                    db.Execute("UpdateMessageRead", new
                    {
                        IsRead = read,
                        SeqNbr = message.SeqNbr
                    });
                    message.IsSelected = false;
                    if (read == (int)MessageRead.Read)
                        Unread -= 1;
                }

                MessageCollaborator mcsm = ObjectPool.Instance.Resolve<MessageCollaborator>();
                mcsm.ForceSyncronizing();
                SelectableRow.Clear();
                CheckedHeader = false;

                db.Close();
                IsBusy = false;
            });
        }

        private void OnPermanentlyDelete()
        { 
            Task.Factory.StartNew(() =>
            {
                IDbManager dbManager = ObjectPool.Instance.Resolve<IDbManager>();
                IDataCommand db = dbManager.GetDatabase(DatabaseNames.SMSGW);
                IsBusy = true;
                string number = string.Empty;
                Type messageType;

                foreach (BaseMessageModel message in SelectableRow.ToList())
                {
                    messageType = message.GetType();
                    if (messageType == typeof(Trash))
                    {
                        if (((Trash)message).Source.ToLower() == "outbox") 
                            number = ((Trash)message).Receiver;
                    } 

                    db.Execute("DeleteMessage", new
                    {
                        SeqNbr = message.SeqNbr,
                        Sender = message.Sender,
                        Receiver = number
                    });
                    message.IsSelected = false;
                }
                MessageCollaborator mcsm = ObjectPool.Instance.Resolve<MessageCollaborator>();
                mcsm.ForceSyncronizing();
                SelectableRow.Clear();
                CheckedHeader = false;

                db.Close();
                IsBusy = false;
            });
        }

        private void OnForward(object args)
        {
            OnMessageOpen(args);
        }

        private void OnRestore()
        { 
            Task.Factory.StartNew(() =>
            {
                IDbManager dbManager = ObjectPool.Instance.Resolve<IDbManager>();
                IDataCommand db = dbManager.GetDatabase(DatabaseNames.SMSGW);
                IsBusy = true;
                string number; 
                Type messageType;

                foreach (BaseMessageModel message in SelectableRow.ToList())
                {
                    messageType = message.GetType();
                    if (messageType == typeof(Trash))
                    {
                        if (((Trash)message).Source.ToLower() == "inbox")
                            number = ((Trash)message).Sender;
                        else
                            number = ((Trash)message).Receiver;
                    }
                    else
                        number = message.Sender;

                    db.Execute("UpdateMessageStatus", new
                    {
                        Status = "R",
                        SeqNbr = message.SeqNbr,
                        Sender = number,
                        Source = ""
                    });
                    message.IsSelected = false;

                }
                MessageCollaborator mcsm = ObjectPool.Instance.Resolve<MessageCollaborator>();
                mcsm.ForceSyncronizing();
                SelectableRow.Clear();
                CheckedHeader = false;

                db.Close();
                IsBusy = false;
            });
        }
         
        private void OnMessageOpen(object args)
        { 
            ComposeMessageModel forward = new ComposeMessageModel();
            if (args == null)
                return;
            else if(args.GetType() == typeof(ComposeMessageModel))
            {
                DialogService.Instance.ShowDialog<Views.Dialogs.ComposeMessage>(args); 
                return;
            } 
            Type typeArgs = args.GetType();

            if (typeArgs == typeof(Inbox) || typeArgs == typeof(Spam) || typeArgs == typeof(Trash))
            {
                BaseMessageModel inbox = (BaseMessageModel)args;
                if (typeArgs == typeof(Inbox))
                    ((Inbox)inbox).IsRead = (int)MessageRead.Read;
                else if (typeArgs == typeof(Trash))
                    ((Trash)inbox).IsRead = (int)MessageRead.Read;

                IDbManager dbManager = ObjectPool.Instance.Resolve<IDbManager>();
                IDataCommand db = dbManager.GetDatabase(DatabaseNames.SMSGW); 
                db.Execute("UpdateMessageRead", new
                {
                    IsRead = (int)MessageRead.Read,
                    SeqNbr = inbox.SeqNbr
                });
                db.Close();

                forward.Message = inbox.Message;
                forward.Phonenumber = inbox.Sender;
            } 

            Outbox outbox = new Outbox();
            if (typeArgs == typeof(Outbox))
            {
                outbox = (Outbox)args;
                forward.Message = outbox.Message;
            }
            else if (typeArgs.Name == "SelectedItemCollection")
            {
                outbox = (Outbox)((System.Collections.IList)args)[0];
                forward.Message = outbox.Message;
            } 
            DialogService.Instance.ShowDialog<Views.Dialogs.ComposeMessage>(forward);
        }

        private void OnMouseDoubleClick(object args)
        {
            OnMessageOpen(args);
        }

        private void OnMarkAsSpam(string source)
        {
            if (source == null)
                throw new ArgumentNullException("Source");

            Task.Factory.StartNew(() =>
            {
                IDbManager dbManager = ObjectPool.Instance.Resolve<IDbManager>();
                IDataCommand db = dbManager.GetDatabase(DatabaseNames.SMSGW);
                IsBusy = true;

                foreach (BaseMessageModel message in SelectableRow.ToList())
                { 
                    db.Execute("UpdateMessageStatus", new
                    {
                        Status = "S",
                        SeqNbr = message.SeqNbr,
                        Sender = message.Sender,
                        Source = source.ToUpper()
                    });
                    message.IsSelected = false;
                }

                MessageCollaborator mcsm = ObjectPool.Instance.Resolve<MessageCollaborator>();
                mcsm.ForceSyncronizing();
                SelectableRow.Clear();
                CheckedHeader = false;

                db.Close();
                IsBusy = false;
            });
        }
         
        private void OnDelete(string source)
        {
            if (source == null)
                throw new ArgumentNullException("Source");

            Task.Factory.StartNew(() =>
            {
                IDbManager dbManager = ObjectPool.Instance.Resolve<IDbManager>();
                IDataCommand db = dbManager.GetDatabase(DatabaseNames.SMSGW);
                IsBusy = true;
                string number;
                foreach (BaseDataRow message in SelectableRow.ToList())
                {
                    BaseMessageModel model;
                    if (source == "O") // source from Outbox
                    {
                        model = (BaseMessageModel)message;
                        number = ((Outbox)message).Receiver;
                        db.Execute("UpdateMessageStatus", new
                        {
                            Status = "D",
                            SeqNbr = model.SeqNbr,
                            Sender = number,
                            Source = source.ToUpper()
                        });
                    }
                    else if (source == "I") // source from Inbox
                    {
                        model = (BaseMessageModel)message;
                        number = model.Sender;
                        db.Execute("UpdateMessageStatus", new
                        {
                            Status = "D",
                            SeqNbr = model.SeqNbr,
                            Sender = number,
                            Source = source.ToUpper()
                        });
                    }
                    else if (source == "D") // source from Draft
                    {
                        ComposeMessageModel compose = (ComposeMessageModel)message;
                        
                        db.Execute("DeleteQueue", new
                        {
                            SeqNbr = compose.SeqNbr
                        });
                    }
                    message.IsSelected = false;
                }
                MessageCollaborator mcsm = ObjectPool.Instance.Resolve<MessageCollaborator>();
                mcsm.ForceSyncronizing();
                SelectableRow.Clear();
                CheckedHeader = false;

                db.Close(); 
                IsBusy = false;
            });
        }

        private void OnCheckedHeader(object args)
        {
            bool check = false;
            MutableObservableCollection<object> listArgs = new MutableObservableCollection<object>((IEnumerable<BaseDataRow>)args);

            if (listArgs.Count > 0)
                check = !((BaseDataRow)listArgs[0]).SelectAll;
            
            Task.Factory.StartNew(() =>
            {
                SelectableRow.Clear();
                foreach (BaseDataRow message in listArgs)
                {
                    message.IsSelected = check;
                    message.SelectAll = check;

                    if (message.IsSelected)
                    {
                        SelectableRow.Add(message);
                    }
                    else
                    {
                        SelectableRow.Remove(message);
                    }
                }
            });
        }

        private void OnCheckedRow(object args)
        {
            MutableObservableCollection<object> listArgs;
            if (args.GetType() == typeof(List<ComposeMessageModel>)) 
                listArgs = new MutableObservableCollection<object>((MutableObservableCollection<BaseBindableModel>)args);  
            else if (args.GetType() == typeof(MutableObservableCollection<Inbox>)) 
                listArgs = new MutableObservableCollection<object>((MutableObservableCollection<Inbox>)args); 
            else if (args.GetType() == typeof(MutableObservableCollection<ComposeMessageModel>)) 
                listArgs = new MutableObservableCollection<object>((MutableObservableCollection<ComposeMessageModel>)args); 
            else if (args.GetType() == typeof(MutableObservableCollection<Outbox>)) 
                listArgs = new MutableObservableCollection<object>((MutableObservableCollection<Outbox>)args); 
            else if (args.GetType() == typeof(MutableObservableCollection<Trash>)) 
                listArgs = new MutableObservableCollection<object>((MutableObservableCollection<Trash>)args); 
            else 
                listArgs = new MutableObservableCollection<object>((MutableObservableCollection<BaseMessageModel>)args);  

            SelectableRow.Clear();
            foreach (BaseDataRow message in listArgs.ToList())
            {
                if (message.IsSelected) 
                    SelectableRow.Add(message); 
                else 
                    SelectableRow.Remove(message); 
            } 
        }

        private void OnSelectionChangedCommand(IList selectedItem)
        {
            foreach (BaseMessageModel message in selectedItem)
            {
                message.IsSelected = !message.IsSelected;
            }   
        }
         
        private void OnReply(object selectedItem)
        {
            OnMessageOpen(selectedItem);
        } 
    }
}
