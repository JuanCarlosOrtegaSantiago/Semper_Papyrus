using Acr.UserDialogs;
using IURIS.BIZ;
using IURIS.COMMON.Constantes;
using IURIS.COMMON.Entidades.Ley;
using IURIS.COMMON.Entidades.Ley.ComponentesDeLey;
using IURIS.COMMON.Entidades.UsuariosDeAplicacion;
using IURIS.COMMON.Interfaces;
using IURIS.DAL;
using IURIS.MOVIL.Detail;
using IURIS.MOVIL.Modelos_y_clases.DB_Local;
using IURIS.MOVIL.Modelos_y_clases.DB_Local.COMMON;
using IURIS.MOVIL.Modelos_y_clases.Tools;
using IURIS.MOVIL.Utils;
using IURIS.MOVIL.Views.ViewsVentanasEmergentes;
using MarcTron.Plugin;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IURIS.MOVIL.Views.ViewsCargarLey 
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WindowDeCopmpa : ContentPage
    {
        IManejadorDeLeyes manejadorDeLeyes;
        //IManejadorDeUsuarioAplicacion manejadorDeUsuario;
        List<Leyes> Leyes;
        Const _Const = new Const();
        public WindowDeCopmpa()
        {

            InitializeComponent();
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await Task.Delay(30);
                CargarDatos();
                if (App.MyUser.MisLeyes.Count == 1)
                {

                    //UserDialogs.Instance.Toast(" Para eliminar una ley,\n desliza hacia la izquierda la ley y preciona eliminar", TimeSpan.FromMilliseconds(3000));
                    try
                    {
                        await PopupNavigation.Instance.PushAsync(new WindowAlert("Para eliminar una ley,\n desliza hacia la izquierda la ley y preciona eliminar"), false);
                        await Task.Delay(2000);
                        await PopupNavigation.Instance.PopAsync(false);
                        return;
                    }
                    catch (Exception)
                    {
                        return;

                    }
                }
                manejadorDeLeyes = new ManejadorDeLeyes(new RepositorioGenerico<Leyes>());

            });
            BindingContext = this;
         

            CrossMTAdmob.Current.LoadInterstitial("ca-app-pub-3940256099942544/1033173712");

            CrossMTAdmob.Current.LoadRewardedVideo("ca-app-pub-3940256099942544/5224354917");


        }

        private void CargarDatos()
        {
            clltionLeyes.ItemsSource = null;
            clltionLeyes.ItemsSource = App.MyUser.MisLeyes.OrderByDescending(e=>e.FechaDeDescarga);
            Limpiardatos();
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Validando datos\nEspere...", MaskType.Gradient);
                await Task.Delay(1000);

                //manejadorDeUsuario = new ManejadorDeUsuarioAplicacion(new RepositorioGenerico<Usuarios>(true));
                //Usuarios _User = manejadorDeUsuario.EncontrarUsuarioID(App.MyUser.IdApp);

                //if (_User == null)
                //{
                //    UserDialogs.Instance.HideLoading();
                //    return;
                //}



                //HerramientasGenerales herramientasGenerales = new HerramientasGenerales();
                //herramientasGenerales.RenovarSuscripcion();

                UserDialogs.Instance.HideLoading();
                UserDialogs.Instance.ShowLoading("Buscando ley", MaskType.Gradient);
                await Task.Delay(1000);


                HerramientasGenerales herramientasGenerales1 = new HerramientasGenerales();
                string x = await herramientasGenerales1.AgragarLEyAsync(EntryCodigo.Text);
                string[] subs = x.Split('|');

                UserDialogs.Instance.HideLoading();

                if (subs[0] != "OK")
                {
                    await PopupNavigation.Instance.PushAsync(new WindowAlert(subs[0],true), false);
                    await Task.Delay(5000);
                    await PopupNavigation.Instance.PopAsync(false);
                    return;
                }

                await PopupNavigation.Instance.PushAsync(new WindowAlert("Ley agregada"), false);
                await Task.Delay(2000);
                await PopupNavigation.Instance.PopAsync(false);
             
                CargarDatos();
                Limpiardatos();
            }
            catch (Exception)
            {
                UserDialogs.Instance.HideLoading();
                //await DisplayAlert("Error", "Esta acción no se puede realizar por el momento\nIntente mas tarde", "OK");
                await PopupNavigation.Instance.PushAsync(new WindowAlert("Error", "Esta acción no se puede realizar por el momento\nIntente mas tarde", "OK"), false);
                await Task.Delay(2000);
                await PopupNavigation.Instance.PopAsync(false);
                return;
            }
        }

        private void Limpiardatos()
        {
            EntryCodigo.Text = null;
        }

        private async void clltionLeyes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserDialogs.Instance.ShowLoading("Cargando ley");
            await Task.Delay(200);
            if (clltionLeyes.SelectedItem == null) return;

            App.MyUser.MiUltimaLeyCargada = ((MyLey)clltionLeyes.SelectedItem).CodigoLey;
            Settings.LastCode = App.MyUser.MiUltimaLeyCargada;

            UserDialogs.Instance.HideLoading();
            if (!await App.Database.UpdateUserAsync(App.MyUser))
            {

                await PopupNavigation.Instance.PushAsync(new WindowAlert("error", "No se ha podido cargar la ley\n por favor intente mas tarde", "OK"), false);
                await Task.Delay(2000);
                await PopupNavigation.Instance.PopAsync(false);
                
                return;
            }
            
            App.masterDetail.IsPresented = false;
            App.masterDetail.Detail = new NavigationPage(new ViewDetail());
        }

        private async void SwipeItem_Invoked(object sender, EventArgs e)
        {
            UserDialogs.Instance.ShowLoading("Borrando ley", MaskType.Gradient);
            await Task.Delay(1000);
            var MiLey = ((SwipeItemView)sender).BindingContext as MyLey;

            if (MiLey == null || MiLey.CodigoLey=="ley#1") return;

            if (App.MyUser.MiUltimaLeyCargada.Equals(MiLey.CodigoLey)) App.MyUser.MiUltimaLeyCargada = App.MyUser.MisLeyes.Where(w => w.CodigoLey != MiLey.CodigoLey).First().CodigoLey;

            App.MyUser.MisLeyes.Remove(MiLey);
            
            if (!await App.Database.UpdateUserAsync(App.MyUser)) return;

            UserDialogs.Instance.HideLoading();
            CargarDatos();

            await PopupNavigation.Instance.PushAsync(new WindowAlert("Se borro la ley de tu lista"), false);
            await Task.Delay(2000);
            await PopupNavigation.Instance.PopAsync(false);

        }
    }
}