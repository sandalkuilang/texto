using SMSGatewayWpf.Core;
using SMSGatewayWpf.ViewModels.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Texaco.Database;

namespace SMSGatewayWpf.ViewModels.Contact
{
    public abstract class BaseContactViewModel<T> : BaseBindableModel, IContactCommand
    {
        private bool checkedAll;

        private IList<BaseDataRow> selectableRow;

        public IList<BaseDataRow> SelectableRow
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

        public abstract IList<T> Source { get; set; }

        public System.Windows.Input.ICommand MouseDoubleClickCommand { get; set; }
        public System.Windows.Input.ICommand DeleteCommand { get; set; }
        public System.Windows.Input.ICommand EditCommand { get; set; }
        public System.Windows.Input.ICommand CheckedHeaderCommand { get; set; }
        public System.Windows.Input.ICommand CheckedRowCommand { get; set; } 

        public BaseContactViewModel()
        {
            checkedAll = false;
            SelectableRow = new List<BaseDataRow>();
            MouseDoubleClickCommand = new DelegateCommand<object>(new Action<object>(OnMouseDoubleClick));
            EditCommand = new DelegateCommand<object>(new Action<object>(OnEdit));
            DeleteCommand = new DelegateCommand<object>(new Action<object>(OnDelete));
            CheckedHeaderCommand = new DelegateCommand<object>(new Action<object>(OnCheckedHeader));
            CheckedRowCommand = new DelegateCommand<object>(new Action<object>(OnCheckedRow));

        }
        private void OnItemOpen(object args)
        {
            if (args == null)
                return;
            ComposeNewContactModel data = new ComposeNewContactModel();
            Contact contact = (Contact)args;
            data.ID = contact.ID;
            data.Name = contact.Name;
            data.PhoneNumber = contact.PhoneNumber;

            DialogService.Instance.ShowDialog<Views.Dialogs.ComposeContact>(data);
            ObjectPool.Instance.Resolve<DatabaseCollectionViewSource>().StartSyncronizing();
        }

        private void OnMouseDoubleClick(object args)
        {
            OnItemOpen(args);
        }

        private void OnDelete(object args)
        { 
            Task.Factory.StartNew(() =>
            {
                IDbManager dbManager = ObjectPool.Instance.Resolve<IDbManager>();
                IDataCommand db = dbManager.GetDatabase(DatabaseNames.SMSGW);
                IsBusy = true;

                foreach (BaseContactModel item in SelectableRow)
                {
                    db.Execute("DeleteContact", new
                    {
                        ID = item.ID 
                    });
                }

                IDataSyncronize data = ObjectPool.Instance.Resolve<DatabaseCollectionViewSource>();
                data.StartSyncronizing();

                SelectableRow.Clear();

                db.Close();
                IsBusy = false;
            });
        }

        private void OnEdit(object args)
        {
            OnItemOpen(args);
        }
         
        private void OnCheckedHeader(object args)
        {
            checkedAll = !checkedAll;

            List<object> listArgs = new List<object>((IEnumerable<BaseDataRow>)args);
 
            Task.Factory.StartNew(() =>
            {
                SelectableRow.Clear();
                foreach (BaseDataRow message in listArgs)
                {
                    message.IsSelected = checkedAll;
                    message.SelectAll = checkedAll;

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
            List<object> listArgs;
            if (args.GetType() == typeof(List<ComposeNewContactModel>))
            {
                listArgs = new List<object>((IEnumerable<BaseBindableModel>)args);
            }
            else
            {
                listArgs = new List<object>((IEnumerable<BaseContactModel>)args);
            }

            SelectableRow.Clear();
            foreach (BaseDataRow message in listArgs)
            {
                if (message.IsSelected)
                {
                    SelectableRow.Add(message);
                }
                else
                {
                    SelectableRow.Remove(message);
                }
            } 
        }

    }
}
