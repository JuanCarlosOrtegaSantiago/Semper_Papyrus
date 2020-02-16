using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace IURIS.APP.GUI
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void BtnCrearCuenta_Clicked(object sender, EventArgs e)
        {
            //Navigation.PushAsync(new PageCrearCuenta());
            Navigation.PushAsync(new PageCrearCuenta(), false);
            //Navigation.PushModalAsync(new PageCrearCuenta(),true);
        }

        private void BtnRecuperarCuenta_Clicked(object sender, EventArgs e)
        {
            btnEntrar.IsVisible = false;
            lblCodigo.IsVisible = false;
            lblCodigoUsuario.IsVisible = false;
        }

        private void BtnEntrar_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PageInicioDeSesion(), false);
        }
    }
}
