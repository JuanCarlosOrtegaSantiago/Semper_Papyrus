using System;

using IURIS.APP.GUI.MOBIL.Models;

namespace IURIS.APP.GUI.MOBIL.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Item Item { get; set; }
        public ItemDetailViewModel(Item item = null)
        {
            Title = item?.Text;
            Item = item;
        }
    }
}
