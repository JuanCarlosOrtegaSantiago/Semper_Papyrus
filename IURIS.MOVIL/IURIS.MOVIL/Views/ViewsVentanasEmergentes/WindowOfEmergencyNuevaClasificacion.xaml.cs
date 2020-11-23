using IURIS.BIZ;
using IURIS.COMMON.Entidades.Ley;
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
        Leyes _Leyes;
        public WindowOfEmergencyNuevaClasificacion(Usuarios usuarios,Leyes leyes)
        {
            InitializeComponent();
            _User = usuarios;
            _Leyes = leyes;
        }

        private void BtnAceptar_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(EntryNuevaClasificacion.Text))
            {
                ClasificacionPUsuario clasificacion = new ClasificacionPUsuario()
                {
                    Nombre = EntryNuevaClasificacion.Text,
                    MisArticulos = new List<Articulo>()
                };

                if (string.IsNullOrWhiteSpace(clasificacion.Nombre))
                    return;
                //comprobar por que truena
                _User.MisLeyes.Where(w => w.CodigoLey == _Leyes.CodigoLey).SingleOrDefault().Clasificaciones.Add(clasificacion);
                //_User.MisLeyesLeyes leyes.Clasificaciones.Add(clasificacion);

                manejadorDeUsuarioAplicacion = new ManejadorDeUsuarioAplicacion(new RepositorioGenerico<Usuarios>());

                try
                {

                    if (manejadorDeUsuarioAplicacion.Modificar(_User))
                    {
                        DisplayAlert("Hecho","Agregada satisfactoriamente", "OK");
                        App.masterDetail.IsPresented = false;
                        App.masterDetail.Detail = new NavigationPage(new ViewsMisClasificaciones(_User, null,_Leyes));
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