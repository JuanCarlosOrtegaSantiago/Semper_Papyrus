using Acr.UserDialogs;
using IURIS.MOVIL.Modelos_y_clases;
using IURIS.MOVIL.Modelos_y_clases.DB_Local.COMMON;
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
            DatosAValidar();
            InitializeComponent();
            HabilitarBotones(false);
            
            DatosAIniciar();
        }

        private async void DatosAValidar()
        {
            UserDialogs.Instance.ShowLoading("Validando\npor favor espere.", MaskType.None);
            await Task.Delay(500);

            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await Task.Delay(100);

                var ultimoUsers = await App.ultimoUser.GetUltimoUserAsync();
                    UltimoUser _UltimoUser = ultimoUsers.FirstOrDefault();
                if (_UltimoUser!=null)
                {

                    var usuarios = await App.Database.GetPeopleAsync();
                    App.MyUser = usuarios.Find(r => r.Id == _UltimoUser.IdUser);
                    if (App.MyUser != null)
                    {
                        Intentos = 0;
                        await Navigation.PushAsync(new FirtsView(), false);

                        UserDialogs.Instance.HideLoading();
                        return;
                    }
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

            await Navigation.PushAsync(new PageCrearCuenta(), false);
            Intentos = 0;
        }

        private async void BtnEntrar_Clicked(object sender, EventArgs e)
        {
            Intentos++;
            if (Intentos != 1) return;

            await Navigation.PushAsync(new PageInicioDeSesion(), false);
            Intentos = 0;
        }
    }

}
