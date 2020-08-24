using IURIS.BIZ;
using IURIS.COMMON.Entidades.UsuariosDeAplicacion;
using IURIS.COMMON.Entidades.UsuariosDeAplicacion.ComponentesDeUsuario;
using IURIS.COMMON.Interfaces;
using IURIS.DAL;
using IURIS.MOVIL.Views.ViewsApuntes;
using IURIS.MOVIL.Views.ViewsVentanasEmergentes;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IURIS.MOVIL.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MisApuntes : ContentPage
    {
        readonly Usuarios _User;
        public Apunte _Apunte;
        public Apunte _ApunteCopia;
        IManejadorDeUsuarioAplicacion manejadorDeUsuarioAplicacion;
        public MisApuntes(Usuarios usuarios, Apunte apunte)
        {
            InitializeComponent();
            _User = usuarios;
            _Apunte = apunte;
            _ApunteCopia = apunte;

            if (_Apunte != null)
                TxtMiApunte.Text = apunte.MiApunte;
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ViewsMisApuntes(_User), false);
        }

        private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TxtMiApunte.Text))
            {
                if (_Apunte != null)
                {
                    _User.Apuntes.Remove(_ApunteCopia);
                    _Apunte.MiApunte = TxtMiApunte.Text;
                    _User.Apuntes.Add(_Apunte);
                    manejadorDeUsuarioAplicacion = new ManejadorDeUsuarioAplicacion(new RepositorioGenerico<Usuarios>());
                    if (manejadorDeUsuarioAplicacion.Modificar(_User))
                        TxtMiApunte.Text = null;

                }
                else
                {

                WindowOfEmergencyNombreDeApunte pantalla = new WindowOfEmergencyNombreDeApunte(_User, TxtMiApunte.Text);
                await PopupNavigation.Instance.PushAsync(pantalla);
                }
            }
            else
            {
                await DisplayAlert("", "No tienes ningun texto ingresado", "Ok");
            }
        }
    }
}