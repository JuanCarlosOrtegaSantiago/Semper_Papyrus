using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            BindingContext = this;
            txtData.Text = Txt;
        }

        public double Subtitle
        {
            get
            {
                return Device.GetNamedSize(NamedSize.Subtitle, typeof(Label));
            }
        }

        public double Large
        {
            get
            {
                return Device.GetNamedSize(NamedSize.Large, typeof(Label));
            }
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

        private void txtData_SizeChanged(object sender, EventArgs e)
        {
            const int max_size = 18;
            Label en = sender as Label;
            if (en.Text.Length * en.FontSize > en.Width)
            {
                en.FontSize--;
            }
            if (en.Text != null)
            {
                if (en.Text.Length < en.Text.Length & (en.Text.Length * (en.FontSize + 1)) < en.Width & en.FontSize < max_size)
                {
                    en.FontSize++;
                }
            }
        }
    }
}