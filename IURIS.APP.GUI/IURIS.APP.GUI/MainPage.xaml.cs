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
        int Intentos = 0;
        public MainPage()
        {
            InitializeComponent();
        }

        private void BtnCrearCuenta_Clicked(object sender, EventArgs e)
        {
            Intentos++;
            if (Intentos == 1)
            {
                Navigation.PushAsync(new PageCrearCuenta(), false);
                Intentos = 0;
            }
        }

        //private void BtnRecuperarCuenta_Clicked(object sender, EventArgs e)
        //{
        //    lblCodigo.IsVisible = false;
        //    lblCodigoUsuario.IsVisible = false;
        //}

        private void BtnEntrar_Clicked(object sender, EventArgs e)
        {
            Intentos++;
            if (Intentos == 1)
            {
                Navigation.PushAsync(new PageInicioDeSesion(), false);
                Intentos = 0;
            }
        }
    }
}
