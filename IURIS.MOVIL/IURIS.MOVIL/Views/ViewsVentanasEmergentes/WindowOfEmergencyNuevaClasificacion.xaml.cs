using IURIS.BIZ;
using IURIS.COMMON.Entidades.Ley;
using IURIS.COMMON.Entidades.Ley.ComponentesDeLey;
using IURIS.COMMON.Entidades.UsuariosDeAplicacion;
using IURIS.COMMON.Entidades.UsuariosDeAplicacion.ComponentesDeUsuario;
using IURIS.COMMON.Interfaces;
using IURIS.DAL;
using IURIS.MOVIL.Modelos_y_clases.DB_Local.COMMON;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IURIS.MOVIL.Views.ViewsVentanasEmergentes
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WindowOfEmergencyNuevaClasificacion : PopupPage
    {
        IManejadorDeUsuarioAplicacion manejadorDeUsuarioAplicacion;
        Usuarios _User;
        MyLey _MyLey;
        Articulo _Articulo;
        public WindowOfEmergencyNuevaClasificacion(Usuarios usuarios,MyLey leyes, Articulo articulo)
        {
            InitializeComponent();
            _User = usuarios;
            _MyLey = leyes;
            _Articulo = articulo;
        }

        private async void BtnAceptar_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(EntryNuevaClasificacion.Text)) return;

            ClasificacionPUsuario clasificacion = new ClasificacionPUsuario
            {
                Nombre = EntryNuevaClasificacion.Text,
                MisArticulos = new List<Articulo>()
            };


            if (string.IsNullOrWhiteSpace(clasificacion.Nombre)) return;

            if (_Articulo != null) 
                clasificacion.MisArticulos.Add(_Articulo);

            _User.MisLeyes.Where(w => w.CodigoLey == _MyLey.CodigoLey).SingleOrDefault().Clasificaciones.Add(clasificacion);

            try
            {

                manejadorDeUsuarioAplicacion = new ManejadorDeUsuarioAplicacion(new RepositorioGenerico<Usuarios>());

                if (manejadorDeUsuarioAplicacion.Modificar(_User))
                {
                    await DisplayAlert("Hecho", "Agregada correctamente", "Ok");
                    App.masterDetail.IsPresented = false;
                    App.masterDetail.Detail = new NavigationPage(new ViewsMisClasificaciones(_User, null, _MyLey));
                }
                else
                {
                    await DisplayAlert("Error", "No se pudo completar la operación", "OK");
                }

            }
            catch
            {
                return;
            }


            await PopupNavigation.Instance.PopAsync(false);
        }

        private void BtnCancelar_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync(false);
        }
    }
}