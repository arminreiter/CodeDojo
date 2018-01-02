using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.ObjectModel;

namespace CodingDojo5.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private ItemVm currentItem;
        public ItemVm CurrentItem
        {
            get { return currentItem; }
            set
            {
                currentItem = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<ItemVm> Items { get; set; }
        public ObservableCollection<ItemVm> ShoppingCart { get; set; }

        public RelayCommand<ItemVm> BuyCommand
        {
            get
            {
                return new RelayCommand<ItemVm>(x => ShoppingCart.Add(x));
            }
        }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            Items = new ObservableCollection<ItemVm>();
            ShoppingCart = new ObservableCollection<ItemVm>();
            LoadDemoData();
        }

        private void LoadDemoData()
        {
            var lego = new ItemVm("MY Lego", "Images/lego1.jpg", "");
            lego.AddItem("Lego 1", "Images/lego1.jpg", "5+");
            lego.AddItem("Lego 2", "Images/lego2.jpg", "5+");
            lego.AddItem("Lego 3", "Images/lego3.jpg", "10+");
            lego.AddItem("Lego 4", "Images/lego4.jpg", "15+");

            var playmobil = new ItemVm("MY Playmobil", "Images/playmobil1.jpg", "");
            playmobil.AddItem("Playmobil 1", "Images/playmobil1.jpg", "5+");
            playmobil.AddItem("Playmobil 2", "Images/playmobil2.jpg", "5+");
            playmobil.AddItem("Playmobil 3", "Images/playmobil3.jpg", "5+");

            Items.Add(lego);
            Items.Add(playmobil);
        }
    }
}