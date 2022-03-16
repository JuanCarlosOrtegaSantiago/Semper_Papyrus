using Acr.UserDialogs;
using IURIS.COMMON.Entidades.Ley;
using IURIS.COMMON.Entidades.Ley.ComponentesDeLey;
using IURIS.COMMON.Entidades.UsuariosDeAplicacion;
using IURIS.MOVIL.Modelos_y_clases;
using IURIS.MOVIL.Modelos_y_clases.DB_Local;
using IURIS.MOVIL.Modelos_y_clases.DB_Local.COMMON;
using IURIS.MOVIL.Modelos_y_clases.Tools;
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

        ClassAnuncio Anuncio = new ClassAnuncio();
        MyLey _MyLey;

        public ViewDetail()
        {
            InitializeComponent();
            DatosAInicializar();

            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await Task.Delay(5000);
                myAds.IsVisible = true;
            });

            Anuncio.MostrarAnuncioPantalla();

            //if (_User != null)
            //{
            //    HerramientasGenerales herramientasGenerales = new HerramientasGenerales(_User);
            //    herramientasGenerales.RenovarSuscripcion();
            //}

            var test = CrossMTAdmob.Current.IsInterstitialLoaded().ToString();
            CrossMTAdmob.Current.ShowInterstitial();
            CrossMTAdmob.Current.LoadInterstitial("ca-app-pub-3940256099942544/1033173712");
        }

        private void DatosAInicializar()
        {
            _MyLey = App.MyUser.MisLeyes.Where(e => e.CodigoLey == App.MyUser.MiUltimaLeyCargada).SingleOrDefault();
            
            lblTitle.Text = _MyLey.NombreLey;
            lblCodigo.Text = _MyLey.CodigoLey;
            ActualizarDatos(_MyLey.ListaDeTitulos);

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
;           tituloLey.IsVisible = !v;
            IMGBuscador.IsVisible = !v;
            lblNumResultados.IsVisible = v;
            lblNumResultados.Text = null;
        }

        private async void ClltionTitulos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            Titulo titulo = ClltionTitulos.SelectedItem as Titulo;
            if (titulo == null || titulo.ListaCapitulos == null || titulo.ListaCapitulos.Count == 0)
            {
                ClltionTitulos.SelectedItem = null;
                return;
            }
            MostrarSearch(false);
            
            await Navigation.PushAsync(new PageMotrarCapitulosConCodigos(titulo, _MyLey, myAds), false);
            ClltionTitulos.SelectedItem = null;

        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            MostrarSearch(true);
        }


        private void BuscarTexto(TextChangedEventArgs TextChange)
        {
            if (SearchViewDetailTitle.Text == null) return;

            List<Titulo> titulos = new List<Titulo>();
             titulos =
                _MyLey.ListaDeTitulos.ToList().Where(
                    e => e.NumTitulo.ToUpper().Contains(TextChange.NewTextValue.ToUpper()) == true 
                    || 
                    e.NombreTitulo.ToUpper().Contains(TextChange.NewTextValue.ToUpper()) == true)
                .ToList(); ;

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

        private  async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            try
            {
                await PopupNavigation.Instance.PushAsync(new WindowAlert(_MyLey.NombreLey), false);
                await Task.Delay(3000);
                await PopupNavigation.Instance.PopAsync(false);
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}