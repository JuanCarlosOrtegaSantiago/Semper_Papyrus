using IURIS.COMMON.Entidades.Ley.ComponentesDeLey;
using IURIS.MOVIL.Modelos_y_clases.DB_Local.COMMON;
using IURIS.MOVIL.Views.ViewsVentanasEmergentes;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Shapes;
using Xamarin.Forms.Xaml;

namespace IURIS.MOVIL.Views.ViewsCategoriaPorColor
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageClasificacionPorColor : ContentPage
    {
        MyLey _MyLey;
        string ColorInicial;
        Ellipse Ellipse_Cargado = null;
        private string ColorHex;

        protected override bool OnBackButtonPressed()
        {
            Navigation.PopAsync(false);
            Navigation.PushModalAsync(new FirtsView(), false);
            return true;
        }


        public PageClasificacionPorColor(MyLey MyLey)
        {
            InitializeComponent();
            _MyLey = MyLey;
            ColorInicial = "#FFF7FE2E";
            //App.MyUser.MisLeyes.Find(w => w.CodigoLey == _MyLey.CodigoLey);
            lblTitle.Text = _MyLey.NombreLey;
            lblCodigo.Text = _MyLey.CodigoLey;
            filtradoPorclor(ColorInicial);

        }

        private void filtradoPorclor(string colorInicial)
        {
            try
            {
                if (_MyLey.ClasificacionesPorColores == null)
                    _MyLey.ClasificacionesPorColores = new List<ClasificacionPorColor>();

            clltionCategoriaPorColor.ItemsSource = null;
            clltionCategoriaPorColor.ItemsSource = _MyLey.ClasificacionesPorColores.Where(e=>e.ColorHexDeClasificado.Equals(colorInicial));

            }
            catch (Exception)
            {
                clltionCategoriaPorColor.ItemsSource = null;
            }
        }

        private void EllipceColor(object sender, EventArgs e)
        {
            if (Ellipse_Cargado != null)
            {
                Ellipse_Cargado.StrokeThickness = 0;
                Ellipse_Cargado.Stroke = Brush.Transparent;
            }


            ColorHex = ((SolidColorBrush)(((Ellipse)sender).Fill)).Color.ToHex();
            eclipPrincipal.Fill = Color.FromHex(ColorHex);

            filtradoPorclor(ColorHex);

            Ellipse_Cargado = ((Ellipse)sender);
            ((Ellipse)sender).Stroke = Brush.Black;
            ((Ellipse)sender).StrokeThickness = 4;
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