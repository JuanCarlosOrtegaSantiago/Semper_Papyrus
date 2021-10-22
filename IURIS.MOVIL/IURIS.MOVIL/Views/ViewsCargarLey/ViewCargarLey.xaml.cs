using Acr.UserDialogs;
using IURIS.BIZ;
using IURIS.COMMON.Entidades.Ley;
using IURIS.COMMON.Entidades.Ley.ComponentesDeLey;
using IURIS.COMMON.Entidades.UsuariosDeAplicacion;
using IURIS.COMMON.Interfaces;
using IURIS.DAL;
using IURIS.MOVIL.Detail;
using IURIS.MOVIL.Utils;
using IURIS.MOVIL.Views.ViewsVentanasEmergentes;
using MarcTron.Plugin;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IURIS.MOVIL.Views.ViewsCargarLey
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewCargarLey : ContentPage
    {
        IManejadorDeLeyes manejadorDeLeyes;
        IManejadorDeUsuarioAplicacion manejadorDeUsuarioAplicacion;
        
        Usuarios _User;

        public ViewCargarLey(Usuarios usuarios)
        {
            InitializeComponent();
            BindingContext = this;
            _User = usuarios;


            UserDialogs.Instance.AlertAsync("\tPara eliminar una ley.\n Desliza hacia la izquierda la ley y preciona eliminar");
            CrossMTAdmob.Current.LoadInterstitial("ca-app-pub-3940256099942544/1033173712");

            CrossMTAdmob.Current.LoadRewardedVideo("ca-app-pub-3940256099942544/5224354917");
            //UserDialogs.Instance.Toast("\tPara eliminar una ley.\n Desliza hacia la izquierda la ley y preciona eliminar", TimeSpan.FromMilliseconds(5000));

            manejadorDeUsuarioAplicacion = new ManejadorDeUsuarioAplicacion(new RepositorioGenerico<Usuarios>());
            CargarDatos();

        }

        private void CargarDatos()
        {
            clltionLeyes.ItemsSource = null;
            clltionLeyes.ItemsSource = _User.MisLeyes.OrderByDescending(e=>e.FechaDeDescarga);
            Limpiardatos();
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            try
            {


            UserDialogs.Instance.ShowLoading("Buscando ley", MaskType.Gradient);
            await Task.Delay(1000);

            manejadorDeLeyes = new ManejadorDeLeyes(new RepositorioGenerico<Leyes>());

            bool ExisteLey = false;
            Leyes _LeyComprada=null;

            //if (_User.MisLeyes.Count >= 4)
            //{
            //    UserDialogs.Instance.HideLoading();
            //    await PopupNavigation.Instance.PushAsync(new WindowOfComprarEspacio());
            //    UserDialogs.Instance.ShowLoading("Buscando ley", MaskType.Gradient);
            //    await Task.Delay(1000);
            //    return;
            //}

            if (string.IsNullOrEmpty(EntryCodigo.Text)) return;
            _LeyComprada = manejadorDeLeyes.BuscarPorCodigo(EntryCodigo.Text.ToUpper());

            if (_LeyComprada == null)
            {
                UserDialogs.Instance.HideLoading();
                await DisplayAlert("Error", "Codigo incorrecto\nIntenta de nuevo", "OK");
                return;
            }

                 
            foreach (var Ley in _User.MisLeyes)
                if (_LeyComprada.id == Ley.id)
                    ExisteLey = true;

            //_User.MisLeyes.ForEach(r => { if (r.id == _LeyComprada.id) ExisteLey = true; });

            if (ExisteLey)
            {
                UserDialogs.Instance.HideLoading();
                await DisplayAlert("", "El codigo ingresado\nCorresponde a una ley que ya \nse encuentra en tu coleccion", "OK");
                return;
            }

            UserDialogs.Instance.HideLoading();
            UserDialogs.Instance.ShowLoading("Agregando ley a tu lista", MaskType.Gradient);
            await Task.Delay(1000);
            _LeyComprada.FechaDeDescarga = DateTime.UtcNow.ToLocalTime();

            _User.MisLeyes.Add(_LeyComprada);
            _User.MisLeyes.Where(w => w.CodigoLey == _LeyComprada.CodigoLey).SingleOrDefault().Clasificaciones = new List<ClasificacionPUsuario>();
            if (manejadorDeUsuarioAplicacion.Modificar(_User))
            {
                _LeyComprada.numDescargas += 1;
                manejadorDeLeyes.Modificar(_LeyComprada);
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

            _User.MiUltimaLeyCargada = ((Leyes)clltionLeyes.SelectedItem).CodigoLey;
            Settings.LastCode = _User.MiUltimaLeyCargada;

            if (!manejadorDeUsuarioAplicacion.Modificar(_User))
            {
                await DisplayAlert("error", "No se ha podido cargar la ley\n por favor intente mas tarde", "OK");
                return;
            }
            
            App.masterDetail.IsPresented = false;
            App.masterDetail.Detail = new NavigationPage(new ViewDetail(_User));
            UserDialogs.Instance.HideLoading();
        }

        private async void SwipeItem_Invoked(object sender, EventArgs e)
        {
            UserDialogs.Instance.ShowLoading("Borrando ley", MaskType.Gradient);
            await Task.Delay(1000);
            var MiLey = ((SwipeItemView)sender).BindingContext as Leyes;

            if (MiLey == null || MiLey.CodigoLey=="ley#1") return;

            if (_User.MiUltimaLeyCargada.Equals(MiLey.CodigoLey)) _User.MiUltimaLeyCargada = _User.MisLeyes.Where(w => w.id != MiLey.id).First().CodigoLey;

            _User.MisLeyes.Remove(MiLey);
            
            if (!manejadorDeUsuarioAplicacion.Modificar(_User)) return;

            UserDialogs.Instance.HideLoading();
            await DisplayAlert("", "Se borro la ley de tu lista", "Ok");
            CargarDatos();
        }
    }
}