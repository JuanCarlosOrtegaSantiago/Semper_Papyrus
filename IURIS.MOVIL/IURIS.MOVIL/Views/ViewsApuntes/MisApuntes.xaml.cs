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
                TxtMiApunte.Text = null;

                if (!await App.Database.UpdateUserAsync(App.MyUser))
                {
                    try
                    {
                        await PopupNavigation.Instance.PushAsync(new WindowAlert("Error", "Intenta mas tarde", "Aceptar"), false);
                        await Task.Delay(3000);
                        await PopupNavigation.Instance.PopAsync(false);
                        return;
                    }
                    catch (Exception)
                    {
                        return;

                    }
                }

            }
            else
            {
                WindowOfEmergencyNombreDeApunte pantalla = new WindowOfEmergencyNombreDeApunte(TxtMiApunte.Text);
                await PopupNavigation.Instance.PushAsync(pantalla);
            }
        }
    }
}