using IURIS.COMMON.Entidades.Ley;
using IURIS.COMMON.Entidades.Ley.ComponentesDeLey;
using IURIS.COMMON.Entidades.UsuariosDeAplicacion;
using IURIS.MOVIL.Modelos_y_clases;
using IURIS.MOVIL.Utils;
using IURIS.MOVIL.Views;
using IURIS.MOVIL.Views.ViewsVentanasEmergentes;
using MarcTron.Plugin;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IURIS.MOVIL.Detail
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewDetail : ContentPage
    {


        public Usuarios _Usuario;
        Leyes _Ley;
        public ViewDetail(Usuarios usuarios)
        {
            InitializeComponent();

            MainThread.BeginInvokeOnMainThread(async () => {
                await Task.Delay(5000);
                myAds.IsVisible = true;
            });
                var test = CrossMTAdmob.Current.IsInterstitialLoaded().ToString();
                CrossMTAdmob.Current.ShowInterstitial();
                CrossMTAdmob.Current.LoadInterstitial("ca-app-pub-3940256099942544/1033173712");
            _Usuario = usuarios;
            DatosAInicializar();
        }

        private void DatosAInicializar()
        {
            _Ley = _Usuario.MisLeyes.Where(e => e.CodigoLey == _Usuario.MiUltimaLeyCargada).SingleOrDefault();
            lblTitle.Text = _Ley.NombreLey;
            lblCodigo.Text = _Ley.CodigoLey;
            ActualizarDatos(_Ley.ListaDeTitulos);

            ClltionTitulos.SelectedItem = null;
        }

        private void ActualizarDatos(List<Titulo> _MiLista)
        {
            ClltionTitulos.ItemsSource = null;
            ClltionTitulos.ItemsSource = _MiLista;
        }

        private void MostrarSearch(bool v)
        {
            SearchViewDetailTitle.IsVisible = v;
;            tituloLey.IsVisible = !v;
            IMGBuscador.IsVisible = !v;
            lblNumResultados.IsVisible = v;
            lblNumResultados.Text = null;
        }

        private async void ClltionTitulos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            Titulo titulo = ClltionTitulos.SelectedItem as Titulo;
            if (titulo == null) return;

            MostrarSearch(false);
            await Navigation.PushAsync(new PageMotrarCapitulosConCodigos(titulo, _Usuario, _Ley, myAds), false);
            ClltionTitulos.SelectedItem = null;

            ClassMostrarP_Cmpra _Cmpra = new ClassMostrarP_Cmpra();

            if (_Cmpra.MostrarPantalla()) await PopupNavigation.Instance.PushAsync(new WindowOfMembresiaPlatino());
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            MostrarSearch(true);
        }


        private void BuscarTexto(TextChangedEventArgs TextChange)
        {
            if (SearchViewDetailTitle.Text == null) return;

            List<Titulo> titulos = new List<Titulo>();

             titulos= _Ley.ListaDeTitulos.ToList().Where(e => e.NumTitulo.ToUpper().Contains(TextChange.NewTextValue.ToUpper()) == true || e.NombreTitulo.ToUpper().Contains(TextChange.NewTextValue.ToUpper()) == true).ToList();

            ActualizarDatos(titulos);
            lblNumResultados.Text = TextChange.NewTextValue == "" ? "" : titulos.Count.ToString();

        }


        private void SearchViewDetailTitle_TextChanged(object sender, TextChangedEventArgs e)
        {
            BuscarTexto(e);
        }

        private void SearchViewDetailTitle_SearchButtonPressed(object sender, EventArgs e)
        {
            MostrarSearch(false);
            SearchViewDetailTitle.Text = null;
        }
    }
}