using IURIS.MOVIL.Utils;
using IURIS.MOVIL.Views.ViewsVentanasEmergentes;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace IURIS.MOVIL
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        int Intentos = 0;
        public MainPage()
        {
            InitializeComponent();

            DatosAIniciar();
        }

        private async void DatosAIniciar()
        {
            if(Settings.NumUsuario !="")
            {
                lblCodigoUsuario.Text = Settings.NumUsuario;
            }
            await PopupNavigation.Instance.PushAsync(new WindowOfMembresiaPlatino());
        }

        private void BtnCrearCuenta_Clicked(object sender, EventArgs e)
        {
            Intentos++;
            if (Intentos == 1)
            {
                Navigation.PopAsync();
                Navigation.PushAsync(new PageCrearCuenta(), false);
                Intentos = 0;
            }
        }

        //private void BtnRecuperarCuenta_Clicked(object sender, EventArgs e)
        //{
        //    lblCodigo.IsVisible = false;
        //    lblCodigoUsuario.IsVisible = false;
        //}

        private async void BtnEntrar_Clicked(object sender, EventArgs e)
        {
            Intentos++;
            if (Intentos == 1)
            {
                await Navigation.PopAsync();
                await Navigation.PushAsync(new PageInicioDeSesion(), false);
                Intentos = 0;
            }
        }
    }

}
