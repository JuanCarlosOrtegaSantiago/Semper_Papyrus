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
	public partial class PageInicioDeSesion : ContentPage
	{
        int Intentos = 0;
		public PageInicioDeSesion ()
		{
			InitializeComponent ();
        }

        private void BtnCanselar_Clicked(object sender, EventArgs e)
        {
            Intentos++;
            if (Intentos == 1)
            {
                Navigation.PushAsync(new MainPage(), false);
                Intentos = 0;
            }
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Intentos++;
            if (Intentos == 1)
            {
                LblNoRecuerdoMiContrasenia.TextColor = Color.CadetBlue;
                Navigation.PushAsync(new PageRecuperarCuenta(), false);
                LblNoRecuerdoMiContrasenia.TextColor = Color.White;

                Intentos = 0;
            }
        }

        private void BtnAceptar_Clicked(object sender, EventArgs e)
        {
            Intentos++;
            if (Intentos == 1)
            {
                Application.Current.MainPage=new NavigationPage(new MasterDetailPage());
                //Navigation.PushAsync(new MasterDetailPage(), false);
                Intentos = 0;
            }
        }
    }
}