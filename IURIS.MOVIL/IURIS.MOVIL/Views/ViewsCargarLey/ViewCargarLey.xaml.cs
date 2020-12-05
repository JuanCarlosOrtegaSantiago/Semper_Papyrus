using IURIS.BIZ;
using IURIS.COMMON.Entidades.Ley;
using IURIS.COMMON.Entidades.Ley.ComponentesDeLey;
using IURIS.COMMON.Entidades.UsuariosDeAplicacion;
using IURIS.COMMON.Interfaces;
using IURIS.DAL;
using IURIS.MOVIL.Detail;
using IURIS.MOVIL.Utils;
using IURIS.MOVIL.Views.ViewsVentanasEmergentes;
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
            manejadorDeLeyes = new ManejadorDeLeyes(new RepositorioGenerico<Leyes>());

            bool ExisteLey = false;
            Leyes _LeyeComprada=null;

            if (_User.MisLeyes.Count >= 4)
            {
                await PopupNavigation.Instance.PushAsync(new WindowOfComprarEspacio());
                return;
            }

            if (string.IsNullOrEmpty(EntryCodigo.Text)) return;
            _LeyeComprada = manejadorDeLeyes.BuscarPorCodigo(EntryCodigo.Text.ToUpper());

            if (_LeyeComprada == null)
            {
                await DisplayAlert("Error", "Codigo incorrecto\nIntenta de nuevo", "OK");
                return;
            }

            foreach (var Ley in _User.MisLeyes)
                if (_LeyeComprada.id == Ley.id)
                    ExisteLey = true;

            if (ExisteLey)
            {
                await DisplayAlert("", "El codigo ingresado\nCorresponde a una ley que ya \nse encuentra en tu coleccion", "OK");
                return;
            }
            _LeyeComprada.FechaDeDescarga = DateTime.UtcNow.ToLocalTime();

            _User.MisLeyes.Add(_LeyeComprada);
            _User.MisLeyes.Where(w => w.CodigoLey == _LeyeComprada.CodigoLey).SingleOrDefault().Clasificaciones = new List<ClasificacionPUsuario>();
            if (manejadorDeUsuarioAplicacion.Modificar(_User))
            {
                _LeyeComprada.numDescargas += 1;
                manejadorDeLeyes.Modificar(_LeyeComprada);
                CargarDatos();
            }
            else
            {

                await DisplayAlert("", "Ocurrio un error\nIntente mas tarde", "OK");
            }

            Limpiardatos();
        }

        private void Limpiardatos()
        {
            EntryCodigo.Text = null;
        }

        private async void clltionLeyes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (clltionLeyes.SelectedItem == null) return;

            _User.MiUltimaLeyCargada = ((Leyes)clltionLeyes.SelectedItem).CodigoLey;

            if (!manejadorDeUsuarioAplicacion.Modificar(_User))
            {
                await DisplayAlert("error", "No se ha podido cargar la ley\n por favor intente mas tarde", "OK");
                return;
            }
         
            App.masterDetail.IsPresented = false;
            App.masterDetail.Detail = new NavigationPage(new ViewDetail(_User));
        }

        private void SwipeItem_Invoked(object sender, EventArgs e)
        {

            var MiLey = ((SwipeItemView)sender).BindingContext as Leyes;

            if (MiLey == null || MiLey.numDescargas < 1) return;

            if (_User.MiUltimaLeyCargada.Equals(MiLey.CodigoLey)) _User.MiUltimaLeyCargada = _User.MisLeyes.Where(w => w.id != MiLey.id).First().CodigoLey;

            _User.MisLeyes.Remove(MiLey);
            
            if (!manejadorDeUsuarioAplicacion.Modificar(_User)) return;

            DisplayAlert("", "Se borro la ley de tu colección", "Ok");
            CargarDatos();
        }
    }
}