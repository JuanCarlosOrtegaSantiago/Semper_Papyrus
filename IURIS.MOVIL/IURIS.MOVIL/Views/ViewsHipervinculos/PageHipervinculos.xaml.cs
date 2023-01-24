using IURIS.MOVIL.Modelos_y_clases.DB_Local.COMMON;
using IURIS.MOVIL.Views.ViewsVentanasEmergentes;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IURIS.MOVIL.Views.ViewsHipervinculos
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageHipervinculos : ContentPage
    {
        MyLey _MyLey;
        public PageHipervinculos(MyLey MyLey)
        {
            InitializeComponent();

            _MyLey = MyLey;

            lblTitle.Text = _MyLey.NombreLey;
            lblCodigo.Text = _MyLey.CodigoLey;
        }

        private async void TapGestureRecognizer_Tapped_7(object sender, EventArgs e)
        {
            try
            {
                await PopupNavigation.Instance.PushAsync(new WindowAlert(_MyLey.NombreLey), false);
                await Task.Delay(2500);
                await PopupNavigation.Instance.PopAsync(false);
            }
            catch (Exception)
            {
                return;
            }
        }
    }

}