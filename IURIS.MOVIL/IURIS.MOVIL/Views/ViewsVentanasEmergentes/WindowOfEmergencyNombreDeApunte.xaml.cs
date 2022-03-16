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
        readonly string _TextoApunte;
        public WindowOfEmergencyNombreDeApunte(string TextoApunte)
        {
            InitializeComponent();
            _TextoApunte = TextoApunte;
        }

        private void BtnCancelar_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync(false);
        }

        private async void BtnGuardar_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(EntryNombreApunte.Text)) return;

            Apunte apunte = new Apunte
            {
                MiApunte = _TextoApunte,
                Nombre = EntryNombreApunte.Text
            };

            App.MyUser.Apuntes.Add(apunte);
            if (!await App.Database.UpdateUserAsync(App.MyUser)) return;

            try
            {
                await PopupNavigation.Instance.PushAsync(new WindowAlert("Apunte agregado correctamente"), false);
                await Task.Delay(2000);
                await PopupNavigation.Instance.PopAsync(false);
                App.masterDetail.IsPresented = false;
                App.masterDetail.Detail = new NavigationPage(new MisApuntes(null));
                return;
            }
            catch (Exception)
            {
                return;

            }

        }
    }
}