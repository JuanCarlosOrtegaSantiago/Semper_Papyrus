using Acr.UserDialogs;
using IURIS.BIZ;
using IURIS.COMMON.Entidades.Ley;
using IURIS.COMMON.Entidades.Ley.ComponentesDeLey;
using IURIS.COMMON.Entidades.UsuariosDeAplicacion;
using IURIS.COMMON.Entidades.UsuariosDeAplicacion.ComponentesDeUsuario;
using IURIS.COMMON.Interfaces;
using IURIS.DAL;
using IURIS.MOVIL.Utils;
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
        private string CorreoSinEspacios = "";

        //List<Usuarios> Usuarios;
        public PageCrearCuenta()
        {
            InitializeComponent();

            manejadorDeUsuarioAplicacion = new ManejadorDeUsuarioAplicacion(new RepositorioGenerico<Usuarios>());
        }

        private async void BtnOK_Clicked(object sender, EventArgs e)
        {
            UserDialogs.Instance.ShowLoading("Creando usuario\npor favor espere");
            await Task.Delay(200);
            try
            {

                if (string.IsNullOrWhiteSpace(EntryCorreoElectronico.Text) || string.IsNullOrWhiteSpace(EntryApellidoMaterno.Text) || string.IsNullOrWhiteSpace(EntryApellidoPaterno.Text) || string.IsNullOrWhiteSpace(EntryNombre.Text) || string.IsNullOrWhiteSpace(EntryConfirmarContrasenia.Text) || string.IsNullOrWhiteSpace(EntryContrasenia.Text))
                {
                    await DisplayAlert("Nuevo reguistro", "Faltan datos por llenar", "OK");
                    return;
                }

                if (!CorreoCorrecto || !TamanioDeContraseniaCorrecta) return;

                if (manejadorDeUsuarioAplicacion.ExisteCorreo(EntryCorreoElectronico.Text))
                {
                    lblCorreoExistente.IsVisible = true;
                    return;
                }

                lblCorreoExistente.IsVisible = false;

                if (!ContraseniasIguales) return;

                int numUsuario = manejadorDeUsuarioAplicacion.Listar.Count + 1;
                Usuarios usuarios = new Usuarios()
                {
                    Nombre = EntryNombre.Text,
                    ApellidoPaterno = EntryApellidoPaterno.Text,
                    ApellidoMaterno = EntryApellidoMaterno.Text,
                    Correo = CorreoSinEspacios,
                    IdApp = numUsuario,
                    Contrasenia = int.Parse(EntryContrasenia.Text),
                    Apuntes = new List<Apunte>(),

                };

                manejadorDeLeyes = new ManejadorDeLeyes(new RepositorioGenerico<Leyes>());
                Leyes leyes = manejadorDeLeyes.Listar.Where(w => w.CodigoLey.ToUpper() == "ley#1".ToUpper()).FirstOrDefault();
                leyes.Clasificaciones = new List<ClasificacionPUsuario>();

                List<Leyes> Mleyes = new List<Leyes>();
                Mleyes.Add(leyes);
                usuarios.MisLeyes = Mleyes;
                usuarios.MiUltimaLeyCargada = leyes.CodigoLey;

                Settings.NumAleatorio = 1;
                Settings.CountParaNumAleatorio = "1";

                if (!manejadorDeUsuarioAplicacion.AGREGAR(usuarios))
                {
                    await DisplayAlert("Crear cuenta", "No se puede efectuar por el momento\n intente mas tarde", "OK");
                    return;
                }

                stackCodigoDeUsuario.IsVisible = true;
                lblCodigoUsuario.Text = usuarios.IdApp.ToString();

                UserDialogs.Instance.HideLoading();

                await DisplayAlert("Usuario creado", "Su reguistro fue exitoso", "OK");

                await Navigation.PushAsync(new PageInicioDeSesion(), true);
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.HideLoading();
                await DisplayAlert("Error", "Error: " + ex.Message, "ok");
                return;
            }
        }

        private async void BtnCanselar_Clicked(object sender, EventArgs e)
        {
           await Navigation.PopAsync();
        }

        private bool email_bien_escrito(String email)
        {
            String expresion;
            expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            
            if (Regex.IsMatch(email, expresion)) return Regex.Replace(email, expresion, String.Empty).Length == 0 ? true : false;
            else return false;
        }

        private void EntryCorreoElectronico_TextChanged(object sender, TextChangedEventArgs e)
        {
            char space = ' ';
            CorreoSinEspacios=EntryCorreoElectronico.Text.TrimStart(space);
            CorreoSinEspacios =CorreoSinEspacios.TrimEnd(space);

            if (email_bien_escrito(CorreoSinEspacios))
            {
                CorreoCorrecto = true;
                lblFaltantesDeCorreo.IsVisible = false;
            }
            else
            {
                CorreoCorrecto = false;
                lblFaltantesDeCorreo.IsVisible = true;
            }




        }

        private void EntryContrasenia_TextChanged(object sender, TextChangedEventArgs e)
        {
            lblTamanioDeContrasenia.IsVisible = EntryContrasenia.Text.Length < 4 ? true : false;
            if (!lblTamanioDeContrasenia.IsVisible) TamanioDeContraseniaCorrecta = true;

            lblContraseniaNoCoinside.IsVisible = EntryConfirmarContrasenia.Text != EntryContrasenia.Text ? true : false;
            if (!lblContraseniaNoCoinside.IsVisible) ContraseniasIguales = true;
        }

        private void EntryConfirmarContrasenia_TextChanged(object sender, TextChangedEventArgs e)
        {
            lblContraseniaNoCoinside.IsVisible = EntryConfirmarContrasenia.Text != EntryContrasenia.Text ? true : false;
            if (!lblContraseniaNoCoinside.IsVisible) ContraseniasIguales = true;
        }
    }
}