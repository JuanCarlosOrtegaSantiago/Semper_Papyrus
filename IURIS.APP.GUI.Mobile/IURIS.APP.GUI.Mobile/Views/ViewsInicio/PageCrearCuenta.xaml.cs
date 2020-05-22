using IURIS.APP.GUI.Mobile.Views.ViewsInicio;
using IURIS.BIZ;
using IURIS.COMMON.Entidades.Ley;
using IURIS.COMMON.Entidades.UsuariosDeAplicacion;
using IURIS.COMMON.Interfaces;
using IURIS.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IURIS.APP.GUI
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PageCrearCuenta : ContentPage
	{

        IManejadorDeUsuarioAplicacion manejadorDeUsuarioAplicacion;
        IManejadorDeLeyes manejadorDeLeyes;

        //List<Usuarios> Usuarios;
        public PageCrearCuenta ()
		{
			InitializeComponent ();

            manejadorDeUsuarioAplicacion = new ManejadorDeUsuarioAplicacion(new RepositorioGenerico<Usuarios>());
            //Usuarios = manejadorDeUsuarioAplicacion.Listar;


            manejadorDeLeyes=new ManejadorDeLeyes(new RepositorioGenerico<Leyes>());


            stackCodigoDeUsuario.IsVisible = false;
        }

        private async void BtnOK_Clicked(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(EntryCorreoElectronico.Text) || string.IsNullOrWhiteSpace(EntryApellidoMaterno.Text) || string.IsNullOrWhiteSpace(EntryApellidoPaterno.Text) || string.IsNullOrWhiteSpace(EntryNombre.Text) || string.IsNullOrWhiteSpace(EntryConfirmarContrasenia.Text) || string.IsNullOrWhiteSpace(EntryContrasenia.Text))
            {
                await DisplayAlert("Nuevo reguistro", "Faltan datos por llenar", "OK");
                return;
            }

            if (!email_bien_escrito(EntryCorreoElectronico.Text))
            {
                await DisplayAlert("Nuevo reguistro", "Al correo ingresado le faltan datos", "OK");
                return;
            }
            if (manejadorDeUsuarioAplicacion.ExisteCorreo(EntryCorreoElectronico.Text))
            {
                lblCorreoExistente.IsVisible = true;
                //await DisplayAlert("Nuevo reguistro", "El correo ingresado ya está registrado", "OK");
                return;
            }
            else
            {
                lblCorreoExistente.IsVisible = false;
            }

            if (EntryConfirmarContrasenia.Text != EntryContrasenia.Text)
            {
                lblContraseniaNoCoinside.IsVisible = true;
                return;
            }
            else
            {
                lblContraseniaNoCoinside.IsVisible = false;
            }





            int numUsuario = manejadorDeUsuarioAplicacion.Listar.Count;
            Usuarios usuarios = new Usuarios()
            {
                Nombre = EntryNombre.Text,
                ApellidoPaterno = EntryApellidoPaterno.Text,
                ApellidoMaterno = EntryApellidoMaterno.Text,
                Correo = EntryCorreoElectronico.Text,
                IdApp = numUsuario + 1,
                Contrasenia = int.Parse(EntryContrasenia.Text)

            };
            Leyes leyes = manejadorDeLeyes.BuscarLey("MiEjemplo");
            List<Leyes> Mleyes = new List<Leyes>();
            Mleyes.Add(leyes);
            usuarios.MisLeyes = Mleyes;


            if (!manejadorDeUsuarioAplicacion.AGREGAR(usuarios))
            {

                await DisplayAlert("Crear cuenta", "No se puede efectuar por el momento\n intente mas tarde", "OK");
                return;
            }
            stackCodigoDeUsuario.IsVisible = true;
            lblCodigoUsuario.Text = numUsuario.ToString();
            await DisplayAlert("Usuaro creado", "Su reguistro fue exitoso", "OK");
            //await Navigation.PushAsync(new PageInicioDeSesion(), true);
            //await Navigation.PopAsync();

        }

        private void BtnCanselar_Clicked(object sender, EventArgs e)
        {
            //Navigation.PushAsync(new PageIniciandoApp(), true);
            Navigation.PopAsync();

        }

        private bool email_bien_escrito(String email)
        {
            String expresion;
            expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(email, expresion))
            
                return Regex.Replace(email, expresion, String.Empty).Length == 0 ? true : false;
            
            else
            
                return false;
            
        }
    }
}