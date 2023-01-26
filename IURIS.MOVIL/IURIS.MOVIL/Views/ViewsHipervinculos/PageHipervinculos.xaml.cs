using IURIS.COMMON.Entidades.Ley.ComponentesDeLey;
using IURIS.MOVIL.Detail;
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
using static Xamarin.Essentials.Permissions;

namespace IURIS.MOVIL.Views.ViewsHipervinculos
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageHipervinculos : ContentPage
    {
        protected override bool OnBackButtonPressed()
        {
            Navigation.PopAsync(false);
            Navigation.PushModalAsync(new FirtsView(), false);
            return true;
        }

        MyLey _MyLey;
        public PageHipervinculos(MyLey MyLey)
        {
            InitializeComponent();

            _MyLey = MyLey;
            Inicializacion();
        }

        private void Inicializacion()
        {
            lblTitle.Text = _MyLey.NombreLey;
            lblCodigo.Text = _MyLey.CodigoLey;
                        
            clltionHipervinculos.ItemsSource = null;
            List<hipervinculo> hipervinculos= App.MyUser.MisLeyes.Where(e => e.CodigoLey.Equals(_MyLey.CodigoLey)).FirstOrDefault().Hipervinculos;
            clltionHipervinculos.ItemsSource = hipervinculos;

            lblhiper.Text = $"Mostrar hipervinculos: Tiene {hipervinculos.Count} hipervínculos";
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

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            try
            {
                var Hiper = ((Image)sender).BindingContext as hipervinculo;
                await Navigation.PushAsync(new PageMotrarCapitulosConCodigos(Hiper._Titulo, _MyLey, null, Hiper._Capitulo, Hiper._Articulo), false);
            }
            catch (Exception)
            {
                return;
            }
        }
    }

}