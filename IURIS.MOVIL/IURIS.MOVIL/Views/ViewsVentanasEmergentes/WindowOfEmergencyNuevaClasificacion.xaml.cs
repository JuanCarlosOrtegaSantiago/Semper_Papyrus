using IURIS.BIZ;
using IURIS.COMMON.Entidades.Ley.ComponentesDeLey;
using IURIS.COMMON.Entidades.UsuariosDeAplicacion;
using IURIS.COMMON.Entidades.UsuariosDeAplicacion.ComponentesDeUsuario;
using IURIS.COMMON.Interfaces;
using IURIS.DAL;
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
        public WindowOfEmergencyNuevaClasificacion(Usuarios usuarios)
        {
            InitializeComponent();
            _User = usuarios;
        }

        private void BtnAceptar_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(EntryNuevaClasificacion.Text))
            {
                Clasificacion clasificacion = new Clasificacion()
                {
                    Nombre = EntryNuevaClasificacion.Text,
                    MisArticulos = new List<Articulo>()
                };

                if (string.IsNullOrWhiteSpace(clasificacion.Nombre))
                    return;


                _User.Clasificaciones.Add(clasificacion);

                manejadorDeUsuarioAplicacion = new ManejadorDeUsuarioAplicacion(new RepositorioGenerico<Usuarios>());

                try
                {

                    if (manejadorDeUsuarioAplicacion.Modificar(_User))
                    {
                        DisplayAlert("Hecho","Agregada satisfactoriamente", "OK");
                        App.masterDetail.IsPresented = false;
                        App.masterDetail.Detail = new NavigationPage(new ViewsMisClasificaciones(_User, null));
                    }
                    else
                    {
                        DisplayAlert("Error","No se pudo completar la operación", "OK");

                    }

                }
                catch
                {

                    return;
                }
            }


            PopupNavigation.Instance.PopAsync(false);
        }

        private void BtnCancelar_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync(false);
        }
    }
}