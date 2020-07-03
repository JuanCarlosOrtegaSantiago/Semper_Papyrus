using IURIS.BIZ;
using IURIS.COMMON.Entidades.UsuariosDeAplicacion;
using IURIS.COMMON.Interfaces;
using IURIS.DAL;
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
    public partial class PageRecuperarCuenta : ContentPage
    {
        IManejadorDeUsuarioAplicacion manejadorDeUsuarioAplicacion;
        public Usuarios User=null;
        public PageRecuperarCuenta()
        {
            InitializeComponent();
            manejadorDeUsuarioAplicacion = new ManejadorDeUsuarioAplicacion(new RepositorioGenerico<Usuarios>());
        }

        private async void btnBuscar_Clicked(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(EntryApellidoMaterno.Text) || string.IsNullOrEmpty(EntryApellidoPaterno.Text) || string.IsNullOrEmpty(EntryNombre.Text) || string.IsNullOrEmpty(EntryCorreoElectronico.Text))
            {
                await DisplayAlert("Error", "Faltan datos por ingresar", "Aceptar");
                return;
            }

            //if (EntryNumeroDeTelefono.Text.Length < 10)
            //{
            //    await DisplayAlert("Error", "Faltan dijitos al numero de telefono", "Aceptar");
            //    return;
            //}

            User = manejadorDeUsuarioAplicacion.NoRecuerdoMiContrasenia(EntryNombre.Text, EntryApellidoPaterno.Text, EntryApellidoMaterno.Text, EntryCorreoElectronico.Text);

            if ( User!= null)
            {
                //try
                //{
                //    var message = new SmsMessage(User.Contrasenia.ToString(), new[] { EntryNumeroDeTelefono.Text });
                //    await Sms.ComposeAsync(message);

                //}
                //catch (Exception ex)
                //{
                //    // Other error has occurred.
                //await DisplayAlert("Usuario", "Ocurrio un error", "Aceptar");
                //}
                string Nombre = string.Format("{0} {1}\ntu contraseña es: {2}", EntryNombre.Text, EntryApellidoPaterno.Text, User.Contrasenia.ToString());
                await DisplayAlert("Usuario", Nombre, "Aceptar");
                await Navigation.PopAsync();
                await Navigation.PushAsync(new PageInicioDeSesion(), true);
            }
            else
            {

                await DisplayAlert("Usuario", "Usuario no encontrado\n por favor verifique sus datos", "Aceptar");
            }
            
        }
            //public async Task SendSms(string messageText, string recipient)
            //{
            //    try
            //    {
            //        var message = new SmsMessage(messageText, new[] { recipient });
            //        await Sms.ComposeAsync(message);
            //    }
            //    catch (FeatureNotSupportedException ex)
            //    {
            //        // Sms is not supported on this device.
            //    }
            //    catch (Exception ex)
            //    {
            //        // Other error has occurred.
            //    }
            //}
    
    }
}