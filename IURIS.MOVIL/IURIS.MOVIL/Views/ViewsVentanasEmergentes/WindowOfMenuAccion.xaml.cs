using IURIS.BIZ;
using IURIS.COMMON.Entidades.Ley;
using IURIS.COMMON.Entidades.Ley.ComponentesDeLey;
using IURIS.COMMON.Entidades.UsuariosDeAplicacion;
using IURIS.COMMON.Interfaces;
using IURIS.DAL;
using IURIS.MOVIL.Modelos_y_clases.DB_Local.COMMON;
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
        readonly Articulo _Articulo;
        MyLey _MyLey;
        readonly Capitulo _Capitulo;

        public WindowOfMenuAccion(Titulo titulo, Articulo articulo, MyLey ley, Capitulo capitulo)
        {
            InitializeComponent();
            _Articulo = articulo;
            _Titulo = titulo;
            _MyLey = ley;
            _Capitulo = capitulo;
        }

        private async void LblCrearNota(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync(false);
            await PopupNavigation.Instance.PushAsync(new WindowOfEmergencyCrearNota(_Titulo, _Articulo, _MyLey, _Capitulo), false);
        }

        private async void LblClasificacionPersonalizada(object sender, EventArgs e)
        {
            App.masterDetail.Detail = new NavigationPage(new ViewsMisClasificaciones(_Articulo,_MyLey));
            await PopupNavigation.Instance.PopAsync(false);
        }

        private async void GuardarSeleccion(object sender, EventArgs e)
        {
                await PopupNavigation.Instance.PopAsync(false);
         }

    }
}