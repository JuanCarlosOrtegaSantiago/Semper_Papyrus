using IURIS.APP.GUI.Mobile.Views;
using IURIS.APP.GUI.Mobile.Views.ViewsInicio;
using IURIS.BIZ;
using IURIS.COMMON.Entidades.UsuariosDeAplicacion;
using IURIS.COMMON.Interfaces;
using IURIS.DAL;
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

        IManejadorDeUsuarioAplicacion manejadorDeUsuarioAplicacion;

        public PageInicioDeSesion ()
		{
			InitializeComponent ();
            manejadorDeUsuarioAplicacion = new ManejadorDeUsuarioAplicacion(new RepositorioGenerico<Usuarios>());
        }

        private void BtnCanselar_Clicked(object sender, EventArgs e)
        {
            Intentos++;
            if (Intentos == 1)
            {
                Navigation.PushAsync(new PageIniciandoApp(), true);
                //Navigation.PopAsync();
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

        private async void BtnAceptar_Clicked(object sender, EventArgs e)
        {
            //Intentos++;
            //if (Intentos == 1)
            //{

            //    if(!string.IsNullOrWhiteSpace(EntryPasswor.Text) && !string.IsNullOrWhiteSpace(EntryCorreo.Text))
            //    {
            //        Usuarios usuario = manejadorDeUsuarioAplicacion.EncontrarUsuario(EntryCorreo.Text, int.Parse(EntryPasswor.Text));
            //        if (usuario!=null){
                await Navigation.PushAsync(new MainPage(), false);

        //            }
        //            else
        //            {
        //                await DisplayAlert("Error de usuario", "Por favor verifica los datos ingresados", "OK");
        //            }
        //        }
        //    }
        //        Intentos = 0;
        }
    }
}