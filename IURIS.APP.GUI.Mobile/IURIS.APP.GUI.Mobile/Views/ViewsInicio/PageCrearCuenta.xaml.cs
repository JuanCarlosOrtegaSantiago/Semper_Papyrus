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
	public partial class PageCrearCuenta : ContentPage
	{

        IManejadorDeUsuarioAplicacion manejadorDeUsuarioAplicacion;
        public PageCrearCuenta ()
		{
			InitializeComponent ();

            manejadorDeUsuarioAplicacion = new ManejadorDeUsuarioAplicacion(new RepositorioGenerico<Usuarios>());
            stackCodigoDeUsuario.IsVisible = false;
        }

        private async void BtnOK_Clicked(object sender, EventArgs e)
        {
            stackCodigoDeUsuario.IsVisible = true;

            if(!string.IsNullOrWhiteSpace(EntryCorreoElectronico.Text) && !string.IsNullOrWhiteSpace(EntryApellidoMaterno.Text) && !string.IsNullOrWhiteSpace(EntryApellidoPaterno.Text) && !string.IsNullOrWhiteSpace(EntryNombre.Text) && !string.IsNullOrWhiteSpace(EntryConfirmarContrasenia.Text) && !string.IsNullOrWhiteSpace(EntryContrasenia.Text))
            {

                int numUsuario=manejadorDeUsuarioAplicacion.Listar.Count;
                string Contrasenia = null;

                if (EntryConfirmarContrasenia.Text == EntryContrasenia.Text) {
                    Contrasenia = EntryContrasenia.Text;
                    Usuarios usuarios = new Usuarios()
                    {
                        Nombre = EntryNombre.Text,
                        ApellidoPaterno = EntryApellidoPaterno.Text,
                        ApellidoMaterno = EntryApellidoMaterno.Text,
                        Correo = EntryCorreoElectronico.Text,
                        IdApp = numUsuario + 1,
                        Contrasenia = int.Parse(Contrasenia)

                    };
                    if (manejadorDeUsuarioAplicacion.AGREGAR(usuarios))
                    {
                        stackCodigoDeUsuario.IsVisible = true;
                        lblCodigoUsuario.Text = numUsuario.ToString();
                        await DisplayAlert("Usuaro creado", "Su reguistro fue exitoso", "OK");
                        await Application.Current.MainPage.Navigation.PopAsync();
                        await Navigation.PushAsync(new PageInicioDeSesion(), true);
                    }
                }
                else
                {
                        await DisplayAlert("Nuevo reguistro", "Faltan datos por llenar", "OK");

                }
            }
        }

        private void BtnCanselar_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PageIniciandoApp(), true);
            ////Navigation.PopAsync();

        }
    }
}