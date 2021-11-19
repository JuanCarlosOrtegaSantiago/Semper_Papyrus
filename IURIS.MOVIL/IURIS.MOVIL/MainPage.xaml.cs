using Acr.UserDialogs;
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
using Xamarin.Essentials;
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
            HabilitarBotones(false);
            DatosAValidar();
            DatosAIniciar();
        }

        private async void DatosAValidar()
        {
            UserDialogs.Instance.ShowLoading("Validando\npor favor espere.", MaskType.None);
            await Task.Delay(1000);

            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await Task.Delay(100);

                var users = await App.Database.GetPeopleAsync();
                App.MyUser = users.FirstOrDefault();
                if (App.MyUser != null)
                {
                    Intentos = 0;
                    await Navigation.PushAsync(new FirtsView(), false);

                    await Task.Delay(1000);
                    UserDialogs.Instance.HideLoading();
                    return;
                }
                else
                {
                    HabilitarBotones(true);
                }
            });

            UserDialogs.Instance.HideLoading();
        }

        private void HabilitarBotones(bool v)
        {
            btnCrearCuenta.IsEnabled = v;
            BtnEntrar.IsEnabled = v;
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
