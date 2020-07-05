using IURIS.BIZ;
using IURIS.COMMON.Entidades.UsuariosDeAplicacion;
using IURIS.COMMON.Interfaces;
using IURIS.DAL;
using IURIS.MOVIL.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IURIS.MOVIL
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageInicioDeSesion : ContentPage
    {
        int Intentos = 0;

        IManejadorDeUsuarioAplicacion manejadorDeUsuarioAplicacion;

        public PageInicioDeSesion()
        {
            InitializeComponent();
            manejadorDeUsuarioAplicacion = new ManejadorDeUsuarioAplicacion(new RepositorioGenerico<Usuarios>());

            DatosAIniciar();
        }

        private void DatosAIniciar()
        {
            if (Settings.Email != "")
            {
                EntryCorreo.Text = Settings.Email;
            }
        }

        private async void BtnCanselar_Clicked(object sender, EventArgs e)
        {
            Intentos++;
            if (Intentos == 1)
            {
                await Navigation.PopAsync();
                await Navigation.PushAsync(new MainPage(), true);
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
            Intentos++;
            if (Intentos == 1)
            {

                if (!string.IsNullOrWhiteSpace(EntryPasswor.Text) && !string.IsNullOrWhiteSpace(EntryCorreo.Text))
                {
                    if (Connectivity.NetworkAccess == NetworkAccess.None)
                    {
                        await DisplayAlert("Error", "Sin conexión a internet", "Aceptar");
                        return;
                    }

                    Usuarios usuario = manejadorDeUsuarioAplicacion.EncontrarUsuario(EntryCorreo.Text, int.Parse(EntryPasswor.Text));
                    if (usuario!=null)
                    {
                        Settings.NumUsuario = usuario.IdApp.ToString();
                        Settings.Email = usuario.Correo;
                        await Navigation.PushAsync(new FirtsView(usuario), false);

                    }
                    else
                    {
                        await DisplayAlert("Error de usuario", "Por favor verifica los datos ingresados", "OK");
                    }
                }
            }
            Intentos = 0;
        }
    }
}