using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IURIS.MOVIL.Views.ViewsVentanasEmergentes
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WindowAlert : PopupPage
	{
		public WindowAlert(string Mensaje)
		{
			InitializeComponent();
			LblMensaje.IsVisible = true;
		
			LblMensaje.Text = Mensaje;
		}

		public WindowAlert(string Titulo, string Mensaje)
		{
			InitializeComponent();
			LblMensaje.IsVisible = true;
			LblTitulo.IsVisible = true;

			LblMensaje.Text = Mensaje;
			LblTitulo.Text= Titulo;

		}

		public WindowAlert(string Titulo, string Mensaje, string MensajeAceptar)
		{
			InitializeComponent();
			LblMensaje.IsVisible = true;
			LblTitulo.IsVisible=true;
			LblAceptar.IsVisible=true;

			LblMensaje.Text = Mensaje;
			LblAceptar.Text = MensajeAceptar;
			LblTitulo.Text= Titulo;

		}

		public WindowAlert(string Titulo, string Mensaje, string MensajeAceptar, string MensajeCancelar)
		{
			InitializeComponent();
			LblMensaje.Text = Mensaje;
			for (int i = 0; i < 5; i++)
			{
				Task.Delay(700);
			}

		}

        private async void LblAceptar_Clicked(object sender, EventArgs e)
        {

			await PopupNavigation.Instance.PopAsync(false);
		}
    }
}