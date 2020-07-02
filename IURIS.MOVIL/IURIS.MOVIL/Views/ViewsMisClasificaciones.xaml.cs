using IURIS.BIZ;
using IURIS.COMMON.Entidades.UsuariosDeAplicacion;
using IURIS.COMMON.Entidades.UsuariosDeAplicacion.ComponentesDeUsuario;
using IURIS.COMMON.Interfaces;
using IURIS.DAL;
using IURIS.MOVIL.Detail;
using IURIS.MOVIL.Modelos_y_clases;
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
        Usuarios User;
        public ViewsMisClasificaciones(Usuarios Usuario)
        {
            InitializeComponent();
            User = Usuario;

            DatosAInicializar();
        }

        private void DatosAInicializar()
        {

            lblNoHayClasificacion.IsVisible = User.Clasificaciones.Count <= 0 ? true : false;
            ActualizarDatos();

        }


        void ActualizarDatos()
        {
            clltionClasificaciones.ItemsSource = null;
            clltionClasificaciones.ItemsSource = User.Clasificaciones;
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

            Clasificacion clasificacion = new Clasificacion()
            {
                Nombre = await DisplayPromptAsync("", "", accept: "Aceptar", cancel: "Cancelar", placeholder: "Nombre de la nueva clasificación")
            };

            if (string.IsNullOrWhiteSpace(clasificacion.Nombre))
                return;


            User.Clasificaciones.Add(clasificacion);

            manejadorDeUsuarioAplicacion = new ManejadorDeUsuarioAplicacion(new RepositorioGenerico<Usuarios>());

            try
            {

            if (manejadorDeUsuarioAplicacion.Modificar(User))
            DatosAInicializar();

            }
            catch (Exception ex)
            {

                await DisplayAlert("Error", "Por el momento no se peude agregar su clasificacion\n por favor intente mas tarde\nError:"+ex.Message, "Aceptar");
                return;
            }
        }

    }
}