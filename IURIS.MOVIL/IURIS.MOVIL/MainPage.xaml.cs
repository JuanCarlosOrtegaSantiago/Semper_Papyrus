using IURIS.MOVIL.Modelos_y_clases;
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
        ClassAnuncio Anuncio = new ClassAnuncio();
        public MainPage()
        {
            InitializeComponent();
            DatosAIniciar();
        }

        private void DatosAIniciar()
        {
            if(Settings.NumUsuario !="") lblCodigoUsuario.Text = Settings.NumUsuario;

            Anuncio.MostrarAnuncioPantalla();

        }

        private async void BtnCrearCuenta_Clicked(object sender, EventArgs e)
        {
            Intentos++;
            if (Intentos != 1) return;

            await Navigation.PopAsync();
            await Navigation.PushAsync(new PageCrearCuenta(), false);
            Intentos = 0;
        }

        private async void BtnEntrar_Clicked(object sender, EventArgs e)
        {
            Intentos++;
            if (Intentos != 1) return;

            await Navigation.PopAsync();
            await Navigation.PushAsync(new PageInicioDeSesion(), false);
            Intentos = 0;
        }
    }

}
