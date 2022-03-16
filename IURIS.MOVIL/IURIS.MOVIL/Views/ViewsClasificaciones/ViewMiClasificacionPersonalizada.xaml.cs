using IURIS.BIZ;
using IURIS.COMMON.Entidades.Ley;
using IURIS.COMMON.Entidades.Ley.ComponentesDeLey;
using IURIS.COMMON.Entidades.UsuariosDeAplicacion;
using IURIS.COMMON.Entidades.UsuariosDeAplicacion.ComponentesDeUsuario;
using IURIS.COMMON.Interfaces;
using IURIS.DAL;
using IURIS.MOVIL.Modelos_y_clases.DB_Local.COMMON;
using IURIS.MOVIL.Views.ViewsVentanasEmergentes;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IURIS.MOVIL.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewMiClasificacionPersonalizada : ContentPage
    {
        ClasificacionPUsuario _Clasificacion;
        MyLey _MyLey;
        public List<Articulo> _Articulos;

        public ViewMiClasificacionPersonalizada(ClasificacionPUsuario clasificacion, MyLey leyes)
        {

            InitializeComponent();

            _Clasificacion = clasificacion;
            _MyLey = leyes;
            DatosAInicializar();
        }

        private void DatosAInicializar()
        {

            lblTitle.Text = _Clasificacion.Nombre;
            _Articulos = new List<Articulo>();

            ActualizarTabla();
        }

        private void ActualizarTabla()
        {
            clltionMiClasificacionPersonalziada.ItemsSource = null;
            clltionMiClasificacionPersonalziada.ItemsSource = _Clasificacion.MisArticulos;
        }

        private void clltionMiClasificacionPersonalziada_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IMGBasura.IsVisible = e.CurrentSelection.Count() >= 1 ? true : false;
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

            var Articulo = clltionMiClasificacionPersonalziada.SelectedItems;
            foreach (var item in Articulo) _Clasificacion.MisArticulos.Remove(item as Articulo);

            try
            {

                if (!await App.Database.UpdateUserAsync(App.MyUser))
                {
                    await showAlert("Por favor intenta mas tarde");
                    //await App.HerramientasGenerales.showAlerta(new WindowAlert("Por favor intenta mas tarde"));
                    return;
                }

                await showAlert("Se eliminaron los elementos");
                
                IMGBasura.IsVisible = false;
                ActualizarTabla();
            }
            catch (Exception ex)
            {
                await showAlert("Por el momento no se peude agregar su clasificacion\n por favor intente mas tarde\nError:" + ex.Message);
                return;
            }


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