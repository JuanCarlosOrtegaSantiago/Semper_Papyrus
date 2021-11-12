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
        public Apunte _Apunte;
        public Apunte _ApunteCopia;
        
        public MisApuntes(Apunte apunte)
        {
            InitializeComponent();
            _Apunte = apunte;
            _ApunteCopia = apunte;

            if (_Apunte != null) TxtMiApunte.Text = apunte.MiApunte;
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ViewsMisApuntes(), false);
        }

        private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtMiApunte.Text)) return;

            if (_Apunte != null)
            {
                if (_Apunte.MiApunte.Equals(TxtMiApunte.Text)) return;

                _Apunte.MiApunte = TxtMiApunte.Text;

                if (!await App.Database.UpdateUserAsync(App.MyUser))
                {
                    await DisplayAlert("Error", "Intenta mas tarde", "Aceptar");
                    return;
                }

                TxtMiApunte.Text = null;
            }
            else
            {
                WindowOfEmergencyNombreDeApunte pantalla = new WindowOfEmergencyNombreDeApunte(TxtMiApunte.Text);
                await PopupNavigation.Instance.PushAsync(pantalla);
            }
        }
    }
}