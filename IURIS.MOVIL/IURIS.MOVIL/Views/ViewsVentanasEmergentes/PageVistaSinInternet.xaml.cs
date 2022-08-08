using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IURIS.MOVIL.Views.ViewsVentanasEmergentes
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageVistaSinInternet : ContentPage
    {
        public PageVistaSinInternet(String Txt)
        {
            InitializeComponent();
            txtData.Text = Txt;
        } 
                

        private async void btnIntentarDeNuevo_Clicked(object sender, EventArgs e)
        {
            if (HayConexion())
            {
                await Navigation.PushAsync(new PagePrimeravista(), true);
                
            }
        }


        public static bool HayConexion()
        {
            try
            {
                string huesped = $"8.8.8.8";

                return new Ping().Send(huesped).Status == IPStatus.Success;
            }
            catch
            {
                return false;
            }
        }
    }
}