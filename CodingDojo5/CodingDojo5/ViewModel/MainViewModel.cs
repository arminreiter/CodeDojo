using GalaSoft.MvvmLight;
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
        public ObservableCollection<ItemVm> Items { get; set; }
        public ObservableCollection<ItemVm> ShoppingCart { get; set; }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            Items = new ObservableCollection<ItemVm>();
            LoadDemoData();
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}
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