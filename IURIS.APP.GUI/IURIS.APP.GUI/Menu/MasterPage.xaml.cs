using IURIS.APP.GUI.Model;
using IURIS.APP.GUI.Views.DetailViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IURIS.APP.GUI.Menu
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MasterPage : ContentPage
	{
        public ListView List { get {  return Listview; } }
        public List<MasterMenuItem> item { get; set; }
        public MasterPage ()
		{
			InitializeComponent ();
            Setings();
		}

        private void Setings()
        {
            item = new List<MasterMenuItem>();
            item.Add(new MasterMenuItem("Inicio", "Logo.png", Color.Yellow, typeof(MiEjemplo)));
            item.Add(new MasterMenuItem("Mi otro inicio", "Logo.png", Color.Yellow, typeof(MiEjemplo2)));
            List.ItemsSource = item;

        }
    }
}