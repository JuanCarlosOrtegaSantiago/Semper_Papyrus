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

namespace IURIS.MOVIL
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageCrearCuenta : ContentPage
    {
        IManejadorDeUsuarioAplicacion manejadorDeUsuarioAplicacion;
        IManejadorDeLeyes manejadorDeLeyes;
        bool CorreoCorrecto = false;
        private bool TamanioDeContraseniaCorrecta = false;
        private bool ContraseniasIguales = false;

        //List<Usuarios> Usuarios;
        public PageCrearCuenta()
        {
            InitializeComponent();

            manejadorDeUsuarioAplicacion = new ManejadorDeUsuarioAplicacion(new RepositorioGenerico<Usuarios>());
            //Usuarios = manejadorDeUsuarioAplicacion.Listar;


            manejadorDeLeyes = new ManejadorDeLeyes(new RepositorioGenerico<Leyes>());


            stackCodigoDeUsuario.IsVisible = false;
        }

        private async void BtnOK_Clicked(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(EntryCorreoElectronico.Text) || string.IsNullOrWhiteSpace(EntryApellidoMaterno.Text) || string.IsNullOrWhiteSpace(EntryApellidoPaterno.Text) || string.IsNullOrWhiteSpace(EntryNombre.Text) || string.IsNullOrWhiteSpace(EntryConfirmarContrasenia.Text) || string.IsNullOrWhiteSpace(EntryContrasenia.Text))
            {
                await DisplayAlert("Nuevo reguistro", "Faltan datos por llenar", "OK");
                return;
            }

            if (!CorreoCorrecto)
                return;

            if (!TamanioDeContraseniaCorrecta)
                return;


            //lblFaltantesDeCorreo.IsVisible = false;

            if (manejadorDeUsuarioAplicacion.ExisteCorreo(EntryCorreoElectronico.Text))
            {
                lblCorreoExistente.IsVisible = true;
                //await DisplayAlert("Nuevo reguistro", "El correo ingresado ya está registrado", "OK");
                return;
            }

            lblCorreoExistente.IsVisible = false;

            if (!ContraseniasIguales)
                return;

            int numUsuario = manejadorDeUsuarioAplicacion.Listar.Count + 1;
            Usuarios usuarios = new Usuarios()
            {
                Nombre = EntryNombre.Text,
                ApellidoPaterno = EntryApellidoPaterno.Text,
                ApellidoMaterno = EntryApellidoMaterno.Text,
                Correo = EntryCorreoElectronico.Text,
                IdApp = numUsuario,
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
            lblCodigoUsuario.Text = usuarios.IdApp.ToString();
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

        private void EntryCorreoElectronico_TextChanged(object sender, TextChangedEventArgs e)
        {

            lblFaltantesDeCorreo.IsVisible = !email_bien_escrito(EntryCorreoElectronico.Text) ? true : false;
            if (!lblFaltantesDeCorreo.IsVisible)
                CorreoCorrecto = true;
        }

        private void EntryContrasenia_TextChanged(object sender, TextChangedEventArgs e)
        {
            lblTamanioDeContrasenia.IsVisible = EntryContrasenia.Text.Length < 4 ? true : false;
            if (!lblTamanioDeContrasenia.IsVisible)
                TamanioDeContraseniaCorrecta = true;
        }

        private void EntryConfirmarContrasenia_TextChanged(object sender, TextChangedEventArgs e)
        {
            lblContraseniaNoCoinside.IsVisible = EntryConfirmarContrasenia.Text != EntryContrasenia.Text ? true : false;
            if (!lblContraseniaNoCoinside.IsVisible)
                ContraseniasIguales = true;
        }
    }
}