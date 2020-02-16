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
		public PageInicioDeSesion ()
		{
			InitializeComponent ();
		}

        private void BtnCanselar_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MainPage(), false);
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            LblNoRecuerdoMiContrasenia.TextColor = Color.CadetBlue;
            
            DisplayAlert("Semper_Papyrus", "Redirecciona a la plantilla de recuperar cuenta", "ok");
        }
    }
}