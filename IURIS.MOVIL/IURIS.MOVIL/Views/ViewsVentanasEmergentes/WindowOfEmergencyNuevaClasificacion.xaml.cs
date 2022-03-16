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
        MyLey _MyLey;
        Articulo _Articulo;
        public WindowOfEmergencyNuevaClasificacion(MyLey leyes, Articulo articulo)
        {
            InitializeComponent();
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

            App.MyUser.MisLeyes.Find(w => w.CodigoLey == _MyLey.CodigoLey).Clasificaciones.Add(clasificacion);

            try
            {

                if (!await App.Database.UpdateUserAsync(App.MyUser))
                {
                    await showAlert("No se pudo completar la operación");
                    return;
                }
                
                await showAlert("Agregada correctamente");
                if (_Articulo != null)
                {
                    await PopupNavigation.Instance.PopAsync(false);
                    return;
                }
                    App.masterDetail.IsPresented = false;
                    App.masterDetail.Detail = new NavigationPage(new ViewsMisClasificaciones(null, _MyLey));
            }
            catch
            {
                return;
            }
            await PopupNavigation.Instance.PopAllAsync(false);
            
        }

        private void BtnCancelar_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync(false);
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