using IURIS.BIZ;
using IURIS.COMMON.Entidades.UsuariosDeAplicacion;
using IURIS.COMMON.Entidades.UsuariosDeAplicacion.ComponentesDeUsuario;
using IURIS.COMMON.Interfaces;
using IURIS.DAL;
using IURIS.MOVIL.Views.ViewsApuntes;
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
    public partial class WindowOfEmergencyNombreDeApunte : PopupPage
    {
        IManejadorDeUsuarioAplicacion manejadorDeUsuarioAplicacion;
        Usuarios _User;
        string _TextoApunte;
        public WindowOfEmergencyNombreDeApunte(Usuarios usuarios, string TextoApunte)
        {
            InitializeComponent();

            _User = usuarios;
            _TextoApunte = TextoApunte;

        }

        private void BtnCancelar_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync(false);
        }

        private async void BtnGuardar_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(EntryNombreApunte.Text))
            {
                Apunte apunte = new Apunte
                {
                    MiApunte = _TextoApunte,
                    Nombre = EntryNombreApunte.Text
                };

                _User.Apuntes.Add(apunte);
                manejadorDeUsuarioAplicacion = new ManejadorDeUsuarioAplicacion(new RepositorioGenerico<Usuarios>());
                if (manejadorDeUsuarioAplicacion.Modificar(_User))
                {
                    await DisplayAlert("", "Apunte agregado correctamente", "Ok");
                    //await Navigation.PushAsync(new ViewsMisApuntes(_User), false);
                    await PopupNavigation.Instance.PopAsync(false);
                    App.masterDetail.IsPresented = false;
                    App.masterDetail.Detail = new NavigationPage(new MisApuntes(_User, null));
                }

            }

        }
    }
}