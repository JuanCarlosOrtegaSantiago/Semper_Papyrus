using IURIS.COMMON.Entidades.UsuariosDeAplicacion;
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
    public partial class WindowOfComprarEspacio : PopupPage
    {

        Usuarios _User;

        public WindowOfComprarEspacio(Usuarios usuarios)
        {
            InitializeComponent();
            _User = usuarios;
            lblNoCuentas.Text = "No cuentas con espacio\npara descargar otra ley";
            lblContent.Text = "Comprar un espacio \nmás para leyes por \n$9.00 M.N";
        }

        private void BtnCancelar_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync(false);
        }

        private void BtnAceptar_Clicked(object sender, EventArgs e)
        {
            App.masterDetail.IsPresented = false;
            App.masterDetail.Detail = new NavigationPage(new WindowOpcionDeCompra());
        }
    }
}