using IURIS.BIZ;
using IURIS.COMMON.Entidades.Ley.ComponentesDeLey;
using IURIS.COMMON.Entidades.UsuariosDeAplicacion;
using IURIS.COMMON.Entidades.UsuariosDeAplicacion.ComponentesDeUsuario;
using IURIS.COMMON.Interfaces;
using IURIS.DAL;
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
        Clasificacion _Clasificacion;
        Usuarios _Usuarios;
        public List<Articulo> _Articulos;

        IManejadorDeUsuarioAplicacion manejadorDeUsuarioAplicacion;

        public ViewMiClasificacionPersonalizada(Clasificacion clasificacion, Usuarios usuarios)
        {

            InitializeComponent();

            _Clasificacion = clasificacion;
            _Usuarios = usuarios;
            
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

            var aser = clltionMiClasificacionPersonalziada.SelectedItems;
            foreach (var item in aser)
            {
                _Clasificacion.MisArticulos.Remove(item as Articulo);

            }

            manejadorDeUsuarioAplicacion = new ManejadorDeUsuarioAplicacion(new RepositorioGenerico<Usuarios>());

            try
            {

                if (manejadorDeUsuarioAplicacion.Modificar(_Usuarios))
                {
                    await DisplayAlert("Corecto", "Se eliminaron los elementos", "Aceptar");
                    IMGBasura.IsVisible = true;
                    ActualizarTabla();
                }
                else
                {
                    await DisplayAlert("Error", "Por favor intenta mas tarde", "Aceptar");
                }

            }
            catch (Exception ex)
            {

                await DisplayAlert("Error", "Por el momento no se peude agregar su clasificacion\n por favor intente mas tarde\nError:" + ex.Message, "Aceptar");
                return;
            }


        }
    }
}