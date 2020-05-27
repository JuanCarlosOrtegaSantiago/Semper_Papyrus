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
	public partial class MasterDetail : MasterDetailPage
	{
		public MasterDetail ()
		{
			InitializeComponent ();
            this.Master = new MasterPage();
            this.Detail = new MiEjemplo();
            Masterpage.List.ItemSelected += onItemSelected;

        }

        void onItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MasterMenuItem;
            if (item != null)
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(item.targetTipe));
                Masterpage.List.SelectedItem = null;
                IsPresented = false;
            }
        }

    }
}