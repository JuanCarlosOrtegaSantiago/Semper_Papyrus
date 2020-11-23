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
        int tocadas = 0;
        Usuarios _User;
        Leyes _LeyeComprada;

        public Command<Leyes> FavoriteCommand { get; set; }
        public ViewCargarLey(Usuarios usuarios)
        {
            InitializeComponent();
            BindingContext = this;
            _User = usuarios;

            CargarDatos();
            manejadorDeUsuarioAplicacion = new ManejadorDeUsuarioAplicacion(new RepositorioGenerico<Usuarios>());

        }

        private void CargarDatos()
        {
            clltionLeyes.ItemsSource = null;
            clltionLeyes.ItemsSource = _User.MisLeyes.OrderByDescending(e=>e.FechaDeDescarga);
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            manejadorDeLeyes = new ManejadorDeLeyes(new RepositorioGenerico<Leyes>());

            bool ExisteLey = false;

            if (_User.MisLeyes.Count < 4)
            {

                _LeyeComprada = manejadorDeLeyes.BuscarPorCodigo(EntryCodigo.Text);

                if (_LeyeComprada != null)
                {

                    foreach (var Ley in _User.MisLeyes)
                        if (_LeyeComprada.id == Ley.id)
                            ExisteLey = true;

                    if (!ExisteLey)
                    {
                        _LeyeComprada.FechaDeDescarga = DateTime.UtcNow.ToLocalTime();

                        _User.MisLeyes.Add(_LeyeComprada);
                        _User.MisLeyes.Where(w => w.CodigoLey == _LeyeComprada.CodigoLey).SingleOrDefault().Clasificaciones= new List<ClasificacionPUsuario>();
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
                    }
                    else
                    {
                        await DisplayAlert("", "El codigo ingresado\nCorresponde a una ley que ya \nse encuentra en tu coleccion", "OK");
                    }
                }
                else
                {
                    await DisplayAlert("Error", "Codigo incorrecto\nIntenta de nuevo", "OK");
                }
            }
            else
            {
                await PopupNavigation.Instance.PushAsync(new WindowOfComprarEspacio());
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
            if(_User.MisLeyes.Where(w => w.CodigoLey == _User.MiUltimaLeyCargada).SingleOrDefault().Clasificaciones==null) _User.MisLeyes.Where(w => w.CodigoLey == _User.MiUltimaLeyCargada).SingleOrDefault().Clasificaciones = new List<ClasificacionPUsuario>();

            if (manejadorDeUsuarioAplicacion.Modificar(_User))
            {
                App.masterDetail.IsPresented = false;
                App.masterDetail.Detail = new NavigationPage(new ViewDetail(_User));
            }
            else
            {
                await DisplayAlert("error", "No se ha podido cargar la ley\n por favor intente mas tarde", "OK");
            }

        }

        private void SwipeItem_Invoked(object sender, EventArgs e)
        {
            var MiLey = ((SwipeItemView)sender).BindingContext as Leyes;

            if (MiLey == null) return;

            if (_User.MiUltimaLeyCargada.Equals(MiLey.CodigoLey)) 
                _User.MiUltimaLeyCargada = _User.MisLeyes.Where(w=> w.id!=MiLey.id).First().CodigoLey;

            _User.MisLeyes.Remove(MiLey);
            if (manejadorDeUsuarioAplicacion.Modificar(_User))
            {
                DisplayAlert("", "Se borro la ley de tu colección", "Ok");
                CargarDatos();
            }
        }
    }
}