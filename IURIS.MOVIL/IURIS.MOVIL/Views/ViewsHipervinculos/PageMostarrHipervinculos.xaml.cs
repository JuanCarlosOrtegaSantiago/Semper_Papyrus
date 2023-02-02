using IURIS.COMMON.Entidades.Ley.ComponentesDeLey;
using IURIS.MOVIL.Modelos_y_clases.DB_Local.COMMON;
using IURIS.MOVIL.Views.ViewsApuntes;
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
    public partial class PageMostarrHipervinculos : ContentPage
    {

        protected override bool OnBackButtonPressed()
        {

            return false;
        }


        MyLey _MyLey;
        int? _Ind;
        int _Index;
        List<hipervinculo> _Hipervinculos;
        public PageMostarrHipervinculos(MyLey MyLey, List<hipervinculo> hipervinculos)
        {
            InitializeComponent();
            _MyLey = MyLey;
            _Hipervinculos = hipervinculos;
            IniciarDatos();
        }

        public PageMostarrHipervinculos(MyLey MyLey, List<hipervinculo> hipervinculos, int? index)
        {
            InitializeComponent();
            _MyLey = MyLey;
            _Ind = index;
            _Hipervinculos = hipervinculos;
            IniciarDatos();
        }

        private void IniciarDatos()
        {
            _Index = _Ind ?? 0;
            lblTitle.Text = _MyLey.NombreLey;
            lblCodigo.Text = _MyLey.CodigoLey;
            clltionMostrarHipervinculos.ItemsSource = null;
            clltionMostrarHipervinculos.ItemsSource = _Hipervinculos;
            clltionMostrarHipervinculos.Position = _Index;
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


        private void clltionMostrarHipervinculos_PositionChanged(object sender, PositionChangedEventArgs e)
        {
            _Index = e.CurrentPosition;
        }


        private async void Salir(object sender, EventArgs e)
        {
            NavigationPage a= new NavigationPage(new FirtsView());
            await Navigation.PushModalAsync(new FirtsView(), false);
            //Navigation.PushModalAsync(new FirtsView(), false);
            //Navigation.PushAsync(a, false);
        }


        private void NavegarHiperAtras(object sender, EventArgs e)
        {
            NavegarHiper(-1);
        }

        private void NavegarHiper(int v)
        {
            int result = (_Index + v);
            _Index = result;

            if (result < 0)
                _Index = 0;

            if (result > (_Hipervinculos.Count-1))
                _Index = _Hipervinculos.Count-1;


            clltionMostrarHipervinculos.Position = _Index;
        }

        private void NavegarHiperAdelante(object sender, EventArgs e)
        {
            NavegarHiper(1);
        }
    }
}