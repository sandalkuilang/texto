using SMSGatewayWpf.Core;
using SMSGatewayWpf.ViewModels.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Texaco.Database;

namespace SMSGatewayWpf.ViewModels.Contact
{
    public class DatabaseCollectionViewSource : BaseContactViewModel<Contact>, IDataSyncronize
    { 
        
        private IList<Contact> source;
        public override IList<Contact> Source
        {
            get
            {
                return source;
            }
            set
            {
                NotifyIfChanged(ref source, value);
            }
        }

        public DatabaseCollectionViewSource()
        { 
            Source = new List<Contact>();
            DbContactMaster = new List<Contact>();

            SearchCommand = new DelegateCommand(new Action(OnSearch));
            ShowComposeDialog = new ContactMessageBar();
        }

        private IList<Contact> DbContactMaster { get; set; }

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
         
        private void OnSearch()
        {
            Task.Factory.StartNew(() =>
            {
                Source = ContactSearchEngine.Filter(Source, searchText).ToList();
            }).ContinueWith(x =>
            {
                StartSyncronizing();
            });
        }

        public void StopSyncronizing()
        {
             
        }

        public void ForceSyncronizing()
        {
             
        }

        public void StartSyncronizing()
        { 
            IDbManager dbManager = ObjectPool.Instance.Resolve<IDbManager>();
            IDataCommand db = dbManager.GetDatabase(DatabaseNames.SMSGW);

            List<Contact> newContact;
             
            {
                newContact = db.Query<Contact>("GetContactAll");
                // save current view source
                DbContactMaster = newContact;
                // refresh all view source when search text box is empty
                if (string.IsNullOrEmpty(searchText))
                { 
                    Source = newContact;
                }  
            }
            db.Close(); 
        }
    }
}
