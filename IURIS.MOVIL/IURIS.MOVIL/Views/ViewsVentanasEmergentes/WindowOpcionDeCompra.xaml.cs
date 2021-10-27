using IURIS.MOVIL.Views.ViewPayPal;
using PayPal.Forms;
using PayPal.Forms.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IURIS.MOVIL.Views.ViewsVentanasEmergentes
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WindowOpcionDeCompra : ContentPage
    {
        public WindowOpcionDeCompra()
        {
            InitializeComponent();
        }

        private void _Btn39Mensuales_Clicked(object sender, EventArgs e)
        {
            SendPagePayPal(39);
        }


        private void _Btn49Mensuales_Clicked(object sender, EventArgs e)
        {
            SendPagePayPal(49);
        }

        private void _Btn479Anuales_Clicked(object sender, EventArgs e)
        {
            SendPagePayPal(479);
        }

        private void _Btn19PorLey_Clicked(object sender, EventArgs e)
        {
            SendPagePayPal(19);
        }

        private async void SendPagePayPal(int Monto)
        {
            //await Navigation.PopAsync();
            //await Navigation.PushAsync(new PayPalPage(Monto), false);
            try
            {
                var result = await CrossPayPalManager.Current.Buy(new PayPalItem("Compra en IURIS", new Decimal(Monto), "MXN"),
                    new Decimal(0));
                if (result.Status == PayPalStatus.Cancelled)
                {
                    Debug.WriteLine("Cancelled");
                }
                else if (result.Status == PayPalStatus.Error)
                {
                    Debug.WriteLine(result.ErrorMessage);
                }
                else if (result.Status == PayPalStatus.Successful)
                {
                    Debug.WriteLine(result.ServerResponse.Response.Id);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}