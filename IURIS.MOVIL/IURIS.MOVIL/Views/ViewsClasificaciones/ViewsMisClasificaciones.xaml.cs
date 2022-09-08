using IURIS.BIZ;
using IURIS.COMMON.Entidades.Ley;
using IURIS.COMMON.Entidades.Ley.ComponentesDeLey;
using IURIS.COMMON.Entidades.UsuariosDeAplicacion;
using IURIS.COMMON.Entidades.UsuariosDeAplicacion.ComponentesDeUsuario;
using IURIS.COMMON.Interfaces;
using IURIS.DAL;
using IURIS.MOVIL.Detail;
using IURIS.MOVIL.Modelos_y_clases;
using IURIS.MOVIL.Modelos_y_clases.DB_Local.COMMON;
using IURIS.MOVIL.Views.ViewsVentanasEmergentes;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IURIS.MOVIL.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewsMisClasificaciones : ContentPage
    {
        Articulo _Articulo;
        public bool nuevoArticulo = false;
        readonly MyLey _MyLey;


        protected  override bool OnBackButtonPressed()
        {
            Navigation.PopAsync(false);
            Navigation.PushModalAsync(new FirtsView(), false);
            return true;
        }


        public ViewsMisClasificaciones(Articulo articulo, MyLey leyes)
        {
            InitializeComponent();
            
            _MyLey = leyes;
            _Articulo = articulo;
            ActualizarDatos();
        }

         void ActualizarDatos()
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await Task.Delay(100);

                clltionClasificaciones.ItemsSource = null;
                var clas = App.MyUser.MisLeyes.Find(w => w.CodigoLey == _MyLey.CodigoLey).Clasificaciones;
                clltionClasificaciones.ItemsSource = clas;
                if (_Articulo != null && clas.Count == 0)
                {
                    await PopupNavigation.Instance.PushAsync(new WindowOfEmergencyNuevaClasificacion(_MyLey, _Articulo));
                }
            });
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            try
            {

            await PopupNavigation.Instance.PushAsync(new WindowOfEmergencyNuevaClasificacion(_MyLey,_Articulo));
            }
            catch(Exception ex)
            {

            }

        }

        private async void clltionClasificaciones_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClasificacionPUsuario clasificacion = (ClasificacionPUsuario)clltionClasificaciones.SelectedItem;
            if (_Articulo != null)
            {


                if (!(clasificacion.MisArticulos.Where(w => w.NumArticulo == _Articulo.NumArticulo).Count() >= 1))
                {


                    clasificacion.MisArticulos.Add(_Articulo);
                    try
                    {

                        if (!await App.Database.UpdateUserAsync(App.MyUser))
                        {
                            await showAlert("Por favor intenta mas tarde");
                            return;
                        }

                        await showAlert("Agregada correctamente");
                        _Articulo = null;
                        return;
                    }
                    catch (Exception ex)
                    {

                        await showAlert("Por el momento no se peude agregar su clasificacion\n por favor intente mas tarde\nError:" + ex.Message);
                        return;
                    }
                }
            }
            await Navigation.PushAsync(new ViewMiClasificacionPersonalizada(clasificacion, _MyLey), false);
        }

        private async void SwipeItemView_Invoked(object sender, EventArgs e)
        {
            var MiClasificacion = ((SwipeItemView)sender).BindingContext as ClasificacionPUsuario;

            if (MiClasificacion == null) return;
            App.MyUser.MisLeyes.Where(w => w.CodigoLey == _MyLey.CodigoLey).SingleOrDefault().Clasificaciones.Remove(MiClasificacion);

            if (!await App.Database.UpdateUserAsync(App.MyUser)) return;
            ActualizarDatos();
        }

        private async Task showAlert(string mensaje)
        {
            try
            {
                await PopupNavigation.Instance.PushAsync(new WindowAlert(mensaje), false);
                await Task.Delay(2000);
                await PopupNavigation.Instance.PopAsync(false);
                return;
            }
            catch (Exception)
            {
                return;

            }
        }
    }
}