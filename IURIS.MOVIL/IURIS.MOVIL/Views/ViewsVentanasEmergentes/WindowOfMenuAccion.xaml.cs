using IURIS.BIZ;
using IURIS.COMMON.Entidades.Ley;
using IURIS.COMMON.Entidades.Ley.ComponentesDeLey;
using IURIS.COMMON.Entidades.UsuariosDeAplicacion;
using IURIS.COMMON.Interfaces;
using IURIS.DAL;
using Plugin.Clipboard;
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
    public partial class WindowOfMenuAccion : PopupPage
    {
        readonly Titulo _Titulo;
        readonly Usuarios _User;
        readonly Articulo _Articulo;
        readonly Leyes _Ley;
        readonly Capitulo _Capitulo;
        IManejadorDeUsuarioAplicacion manejadorDeUsuarioAplicacion;

        public WindowOfMenuAccion(Titulo titulo, Usuarios usuarios, Articulo articulo, Leyes ley, Capitulo capitulo)
        {
            InitializeComponent();
            _Articulo = articulo;
            _Titulo = titulo;
            _User = usuarios;
            _Ley = ley;
            _Capitulo = capitulo;
            manejadorDeUsuarioAplicacion = new ManejadorDeUsuarioAplicacion(new RepositorioGenerico<Usuarios>());
        }

        private async void LblCrearNota(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync(false);
            await PopupNavigation.Instance.PushAsync(new WindowOfEmergencyCrearNota(_Titulo, _User, _Articulo, _Ley, _Capitulo), false);
        }

        private async void LblClasificacionPersonalizada(object sender, EventArgs e)
        {
            App.masterDetail.Detail = new NavigationPage(new ViewsMisClasificaciones(_User, _Articulo,_Ley));
            await PopupNavigation.Instance.PopAsync(false);
        }

        private async void GuardarSeleccion(object sender, EventArgs e)
        {
                await PopupNavigation.Instance.PopAsync(false);
         }

        private async void BorrarSubrayado(object sender, EventArgs e)
        {
            if (!_Articulo.TieneColorDeTexto)
                return;

            _Articulo.TextoContenidoSeleccionado = null;
            _Articulo.TieneColorDeTexto = false;
            _Articulo.ColorTextoHex = null;

            if (manejadorDeUsuarioAplicacion.Modificar(_User))
                await DisplayAlert("Informe", "Se borro el subrayado, refresca la página", "ok");

            await PopupNavigation.Instance.PopAsync(false);
        }
    }
}