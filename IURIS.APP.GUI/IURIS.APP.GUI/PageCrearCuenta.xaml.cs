using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IURIS.APP.GUI
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PageCrearCuenta : ContentPage
	{
		public PageCrearCuenta ()
		{
			InitializeComponent ();
            stackCodigoDeUsuario.IsVisible = false;
        }

        private void BtnOK_Clicked(object sender, EventArgs e)
        {
            stackCodigoDeUsuario.IsVisible = true;
        }

        private void BtnCanselar_Clicked(object sender, EventArgs e)
        {
            stackCodigoDeUsuario.IsVisible = false;
        }
    }
}