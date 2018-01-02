using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace CodingDojo5.ViewModel
{
    public class ItemVm
    {
        public string Description { get; set; }
        public BitmapImage Image { get; set; }
        public string AgeRecommendation { get; set; }
        public ObservableCollection<ItemVm> Items { get; set; }

        public ItemVm()
        {
            Items = new ObservableCollection<ItemVm>();
        }

        public ItemVm(string description, string imagePath, string ageRecommendation) : this()
        {
            Description = description;
            Image = new BitmapImage(new Uri(imagePath, UriKind.Relative));
            AgeRecommendation = ageRecommendation;
        }

        public void AddItem(ItemVm item)
        {
            if (Items == null) Items = new ObservableCollection<ItemVm>();

            Items.Add(item);
        }

        public void AddItem(string description, string imagePath, string ageRecommendation)
        {
            AddItem(new ItemVm(description, imagePath, ageRecommendation));
        }
    }
}
