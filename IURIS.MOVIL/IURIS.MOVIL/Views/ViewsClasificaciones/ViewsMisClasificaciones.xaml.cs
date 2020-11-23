using IURIS.BIZ;
using IURIS.COMMON.Entidades.Ley;
using IURIS.COMMON.Entidades.Ley.ComponentesDeLey;
using IURIS.COMMON.Entidades.UsuariosDeAplicacion;
using IURIS.COMMON.Entidades.UsuariosDeAplicacion.ComponentesDeUsuario;
using IURIS.COMMON.Interfaces;
using IURIS.DAL;
using IURIS.MOVIL.Detail;
using IURIS.MOVIL.Modelos_y_clases;
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
    public partial class ViewsMisClasificaciones : ContentPage
    {
        IManejadorDeUsuarioAplicacion manejadorDeUsuarioAplicacion;
        Usuarios _User;
        Articulo _Articulo;
        Leyes _Leyes;
        public bool nuevoArticulo = false;

        public ViewsMisClasificaciones(Usuarios Usuario,Articulo articulo, Leyes leyes)
        {
            InitializeComponent();
            manejadorDeUsuarioAplicacion = new ManejadorDeUsuarioAplicacion(new RepositorioGenerico<Usuarios>());
            _Leyes = leyes;
            _Articulo = articulo;

            DatosAInicializar(Usuario);
        }

        private void DatosAInicializar(Usuarios usuarios)
        {


            _User = manejadorDeUsuarioAplicacion.EncontrarUsuario(usuarios.Correo, usuarios.Contrasenia);


            ActualizarDatos();

        }


        void ActualizarDatos()
        {
            clltionClasificaciones.ItemsSource = null;
            clltionClasificaciones.ItemsSource = _User.MisLeyes.Where(w => w.CodigoLey == _Leyes.CodigoLey).SingleOrDefault().Clasificaciones;
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new WindowOfEmergencyNuevaClasificacion(_User, _Leyes,_Articulo));
        }

        private async void clltionClasificaciones_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
                ClasificacionPUsuario clasificacion = (ClasificacionPUsuario)clltionClasificaciones.SelectedItem;
            if (_Articulo != null)
            {

                clasificacion.MisArticulos.Add(_Articulo);

                manejadorDeUsuarioAplicacion = new ManejadorDeUsuarioAplicacion(new RepositorioGenerico<Usuarios>());

                try
                {

                    if (manejadorDeUsuarioAplicacion.Modificar(_User))
                    {
                        await DisplayAlert("Hecho", "Agregado correctamente", "Aceptar");
                        _Articulo = null;
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
                await Navigation.PushAsync(new ViewMiClasificacionPersonalizada(clasificacion, _User, _Leyes), false);
        }
    }
}