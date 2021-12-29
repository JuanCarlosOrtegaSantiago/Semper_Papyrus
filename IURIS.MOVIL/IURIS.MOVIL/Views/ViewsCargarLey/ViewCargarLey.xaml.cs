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
                await Task.Delay(1000);
                manejadorDeLeyes = new ManejadorDeLeyes(new RepositorioGenerico<Leyes>());
                Leyes = manejadorDeLeyes.Listar;
            });
            BindingContext = this;
         

            CrossMTAdmob.Current.LoadInterstitial("ca-app-pub-3940256099942544/1033173712");

            CrossMTAdmob.Current.LoadRewardedVideo("ca-app-pub-3940256099942544/5224354917");
            UserDialogs.Instance.Toast(" Para eliminar una ley,\n desliza hacia la izquierda la ley y preciona eliminar", TimeSpan.FromMilliseconds(3000));

            CargarDatos();

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

                

                HerramientasGenerales herramientasGenerales = new HerramientasGenerales();
                herramientasGenerales.RenovarSuscripcion();

                UserDialogs.Instance.HideLoading();
                UserDialogs.Instance.ShowLoading("Buscando ley", MaskType.Gradient);
                await Task.Delay(1000);

                bool ExisteLey = false;
                Leyes _LeyComprada = null;
                string _1LeyMas = null;

                if (App.MyUser.DatosSobreUsuario.TipoDeCompra != null) _1LeyMas = App.MyUser.DatosSobreUsuario.TipoDeCompra.Find(w => w == _Const._ComprarEspacio1Ley || w == _Const._39Mensuales);

                if (_1LeyMas != null && App.MyUser.MisLeyes.Count >= App.MyUser.DatosSobreUsuario.NumLeyesPermitidas)
                {
                    UserDialogs.Instance.HideLoading();
                    await PopupNavigation.Instance.PushAsync(new WindowOfComprarEspacio());
                    return;
                }


                if (string.IsNullOrEmpty(EntryCodigo.Text)) return;
                while (Leyes==null);


                _LeyComprada = Leyes.Find(w => w.CodigoLey.ToUpper() == EntryCodigo.Text.ToUpper());

                if (_LeyComprada == null)
                {
                    UserDialogs.Instance.HideLoading();
                    await DisplayAlert("Error", "Codigo incorrecto\nIntenta de nuevo", "OK");
                    return;
                }


                //foreach (var Ley in _User.MisLeyes)
                //    if (_LeyComprada.id == Ley.id)
                //        ExisteLey = true;

                App.MyUser.MisLeyes.ForEach(r =>
                {
                    //if (r.id == _LeyComprada.id) ExisteLey = true;
                    ExisteLey = r.CodigoLey == _LeyComprada.CodigoLey;
                });

                if (ExisteLey)
                {
                    UserDialogs.Instance.HideLoading();
                    await DisplayAlert("", "El codigo ingresado\nCorresponde a una ley que ya \nse encuentra en tu lista", "OK");
                    return;
                }

                UserDialogs.Instance.HideLoading();
                UserDialogs.Instance.ShowLoading("Agregando ley a tu lista", MaskType.Gradient);
                await Task.Delay(1000);
                _LeyComprada.FechaDeDescarga = DateTime.UtcNow.ToLocalTime();

                LocalSaveUser localSaveUser = new LocalSaveUser();

                App.MyUser.MisLeyes.Add(localSaveUser.LeyToMyley(_LeyComprada));
                App.MyUser.MisLeyes.Where(w => w.CodigoLey == _LeyComprada.CodigoLey).SingleOrDefault().Clasificaciones = new List<ClasificacionPUsuario>();

                if (await App.Database.UpdateUserAsync(App.MyUser))
                {
                    //_LeyComprada.numDescargas += 1;
                    //manejadorDeLeyes.Modificar(_LeyComprada);
                    UserDialogs.Instance.HideLoading();
                    CargarDatos();
                }
                else
                {
                    UserDialogs.Instance.HideLoading();
                    await DisplayAlert("", "Ocurrio un error\nIntente mas tarde", "OK");
                }

                Limpiardatos();
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.HideLoading();
                await DisplayAlert("Error", "Esta acción no se puede realizar por el momento\nIntente mas tarde", "OK");
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

            if (!await App.Database.UpdateUserAsync(App.MyUser))
            {
                await DisplayAlert("error", "No se ha podido cargar la ley\n por favor intente mas tarde", "OK");
                return;
            }
            
            App.masterDetail.IsPresented = false;
            App.masterDetail.Detail = new NavigationPage(new ViewDetail());
            UserDialogs.Instance.HideLoading();
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
            await DisplayAlert("", "Se borro la ley de tu lista", "Ok");
            CargarDatos();
        }
    }
}