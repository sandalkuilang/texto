using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SMSGatewayWpf.Core
{
    public class DialogService : IDialogService
    {

        public static IDialogService Instance
        {
            get
            { 
                return ObjectPool.Instance.Resolve<IDialogService>();
            }
        }

        private List<FrameworkElement> bags;
        public System.Collections.ObjectModel.ReadOnlyCollection<System.Windows.FrameworkElement> Views
        {
            get { return new ReadOnlyCollection<FrameworkElement>(bags.ToList()); }
        }

        public DialogService()
        {
            bags = new List<FrameworkElement>(); 
        }

        public void Register(System.Windows.FrameworkElement view)
        {
            Window owner = GetOwner(view);
            if (owner == null)
            { 
                view.Loaded += OnLoaded;
                return;
            }
             
            owner.Closed += OnClosed; 
            bags.Add(view);
        }

        public void Unregister(System.Windows.FrameworkElement view)
        {
            bags.Remove(view);
        }
         
        public bool? ShowDialog<T>(object model) where T : System.Windows.Window
        {
            for(int i = 0; i < bags.Count - 1; i++)
            {
                if (bags[i].GetType() == typeof(T))
                {
                    return ShowDialog(bags[i], model, typeof(T));        
                }
            }
            return false;
        }

        private bool? ShowDialog(object ownerViewModel, object viewModel, Type dialogType)
        { 
            Window dialog = (Window)Activator.CreateInstance(dialogType);
            //dialog.Owner = FindOwnerWindow(ownerViewModel);
            dialog.DataContext = viewModel;

            // Show dialog
            return dialog.ShowDialog();
        }

        public static readonly DependencyProperty IsRegisteredViewProperty =
                                                        DependencyProperty.RegisterAttached(
                                                        "IsRegisteredView",
                                                        typeof(bool),
                                                        typeof(DialogService),
                                                        new UIPropertyMetadata(IsRegisteredViewPropertyChanged));

        public static bool GetIsRegisteredView(FrameworkElement target)
        {
            return (bool)target.GetValue(IsRegisteredViewProperty);
        }

        public static void SetIsRegisteredView(FrameworkElement target, bool value)
        {
            target.SetValue(IsRegisteredViewProperty, value);
        }

        private static void IsRegisteredViewPropertyChanged(DependencyObject target,
                                                                    DependencyPropertyChangedEventArgs e)
        {
            if (DesignerProperties.GetIsInDesignMode(target)) return;

            FrameworkElement view = target as FrameworkElement;
            if (view != null)
            {
                // Cast values
                bool newValue = (bool)e.NewValue;
                bool oldValue = (bool)e.OldValue;

                if (newValue)
                {
                    ObjectPool.Instance.Resolve<IDialogService>().Register(view);
                }
                else
                {
                    ObjectPool.Instance.Resolve<IDialogService>().Unregister(view);
                }
            }
        }
         
        private Window GetOwner(FrameworkElement view)
        {
            return view as Window ?? Window.GetWindow(view);
        }

        private Window FindOwnerWindow(object viewModel)
        {
            FrameworkElement view =
              bags.SingleOrDefault(v => ReferenceEquals(v.DataContext, viewModel));
            if (view == null)
            {
                throw new ArgumentException("Viewmodel is not referenced by any registered View.");
            }
             
            Window owner = view as Window;
            if (owner == null)
            {
                owner = Window.GetWindow(view);
            }
             
            if (owner == null)
            {
                throw new InvalidOperationException("View is not contained within a Window.");
            }

            return owner;
        }

        private void OnLoaded(object sender, EventArgs e)
        {
            FrameworkElement view = sender as FrameworkElement;
            if (view != null)
            { 
                view.Loaded -= OnLoaded; 
                Register(view);
            }
        }
            
        private void OnClosed(object sender, EventArgs e)
        {
            Window owner = sender as Window;
            if (owner != null)
            { 
                IEnumerable<FrameworkElement> windowViews =
                  from view in bags
                  where Window.GetWindow(view) == owner
                  select view;
                 
                foreach (FrameworkElement view in windowViews.ToArray())
                {
                    Unregister(view);
                }
            }
        }

    }
}
