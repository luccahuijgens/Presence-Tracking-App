using OpenNETCF.IoC;
using praticeApp.Resources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace praticeApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NavMasterMaster : ContentPage
    {
        public ListView ListView;

        public NavMasterMaster()
        {
            InitializeComponent();

            BindingContext = new NavMasterMasterViewModel();
            ListView = MenuItemsListView;
        }

        class NavMasterMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<NavMasterMenuItem> MenuItems { get; set; }

            public NavMasterMasterViewModel()
            {
                MenuItems = new ObservableCollection<NavMasterMenuItem>(new[]
{
                        new NavMasterMenuItem { Id = 0, Title = "Start", TargetType=(typeof (NavMasterDetail))},
                        new NavMasterMenuItem { Id = 1, Title = "App koppelen", TargetType=(typeof (Scan))},
                        new NavMasterMenuItem { Id = 2, Title = "Feed", TargetType=(typeof (Feed))},
                        new NavMasterMenuItem { Id = 3, Title = "Aanwezigheid", TargetType=(typeof (BlueTooth))}
                    });
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}