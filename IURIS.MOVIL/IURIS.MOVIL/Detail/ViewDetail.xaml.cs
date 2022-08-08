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

        protected override bool OnBackButtonPressed()
        {
            if (ClltionDarEncontrados.IsVisible)
            {
                ClltionDarEncontrados.IsVisible = false;
                ClltionTitulos.IsVisible = true;
                return true;
            }
            else
            {
                System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
                return false;
            }

        }

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
            var test = CrossMTAdmob.Current.IsInterstitialLoaded().ToString();
            CrossMTAdmob.Current.ShowInterstitial();
            CrossMTAdmob.Current.LoadInterstitial("ca-app-pub-2336879831564913/4601945943");
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
            
            await Navigation.PushAsync(new PageMotrarCapitulosConCodigos(titulo, _MyLey, myAds,null,null), false);
            ClltionTitulos.SelectedItem = null;

        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            MostrarSearch(true);
        }


        private void BuscarTexto(TextChangedEventArgs TextChange)
        {
            if (SearchViewDetailTitle.Text == null || TextChange.NewTextValue=="")
            {
                ClltionDarEncontrados.IsVisible = false;
                ClltionTitulos.IsVisible = true;
                return;
            }

            List<DatosDeSubrayado> _DatosEncontrados = new List<DatosDeSubrayado>();

            foreach (var item in _MyLey.ListaDeTitulos)
            {
                if (item.NumTitulo.ToUpper().Contains(TextChange.NewTextValue.ToUpper()) || item.NombreTitulo.ToUpper().Contains(TextChange.NewTextValue.ToUpper()))
                {
                    string cadena = string.Format("{0} - {1}", item.NumTitulo, item.NombreTitulo);
                    _DatosEncontrados.Add(new DatosDeSubrayado()
                    {
                        id = item.id,
                        tipo = "Titulo",
                        Subrayados = new List<Subrayado>() { Getsubrayado(cadena, TextChange.NewTextValue) }
                    });
                }

                foreach (var con in item.ListaCapitulos)
                {
                    if (con.NombreCapitulo.ToUpper().Contains(TextChange.NewTextValue.ToUpper()) || con.NumCapitulo.ToUpper().Contains(TextChange.NewTextValue.ToUpper()))
                    {

                        string cadena = string.Format("{0} - {1}", con.NumCapitulo, con.NombreCapitulo);
                        _DatosEncontrados.Add(new DatosDeSubrayado()
                        {
                            id = con.id,
                            tipo = "Capitulo",
                            Subrayados = new List<Subrayado>() { Getsubrayado(cadena, TextChange.NewTextValue) }
                        });
                    }

                    foreach (var arti in con.ListaArticulos)
                    {
                        if (arti.NombreArticulo.ToUpper().Contains(TextChange.NewTextValue.ToUpper()) || arti.NumArticulo.ToUpper().Contains(TextChange.NewTextValue.ToUpper()) || arti.Contenido.ToUpper().Contains(TextChange.NewTextValue.ToUpper()))
                        {

                            string cadena = string.Format("{0} - {1}\n{2}", arti.NumArticulo, arti.NombreArticulo,arti.Contenido);
                            _DatosEncontrados.Add(new DatosDeSubrayado
                            {
                                id = arti.id,
                                tipo = "Articulo",
                                Subrayados = new List<Subrayado>() { Getsubrayado(cadena, TextChange.NewTextValue) },
                            });
                        }
                    }
                }
            }

            ClltionDarEncontrados.ItemsSource = _DatosEncontrados;
            ClltionDarEncontrados.IsVisible = true;
            ClltionTitulos.IsVisible = false;

            //List<Titulo> titulos = new List<Titulo>();
            //titulos =
            //   _MyLey.ListaDeTitulos.Where(
            //       e => e.NumTitulo.ToUpper().Contains(TextChange.NewTextValue.ToUpper())
            //       ||
            //       e.NombreTitulo.ToUpper().Contains(TextChange.NewTextValue.ToUpper())
            //       ).ToList();

            //ActualizarDatos(titulos);
            lblNumResultados.Text = TextChange.NewTextValue == "" ? "" : _DatosEncontrados.Count.ToString();

        }

        private Subrayado Getsubrayado(string cadena, string txt)
        {
            try
            {

                int x = cadena.ToUpper().IndexOf(txt.ToUpper());
                int y = txt.Length;

                return new Subrayado()
                {
                    ColorTextoHex = "#eef21f",
                    TextoContenidoAnteriror = cadena.Substring(0, x),
                    TextoContenidoSeleccionado = txt,
                    TextoContenidoDespues = cadena.Substring(x + y)
                };
            }
            catch (Exception)
            {

                return null;
            }
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
                await Task.Delay(2500);
                await PopupNavigation.Instance.PopAsync(false);
            }
            catch (Exception)
            {
                return;
            }

        }

        private class DatosDeSubrayado
        {
            public string id { get; set; }
            public string tipo { get; set; }
            public List<Subrayado> Subrayados { get; set; }
        }

        private async void ClltionDarEncontrados_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ClltionDarEncontrados.SelectedItem == null) return;

            var DatosSub = (DatosDeSubrayado)ClltionDarEncontrados.SelectedItem;

            if (DatosSub.tipo.Equals("Titulo")){
                var d=_MyLey.ListaDeTitulos.Find(g => g.id == DatosSub.id);
                ClltionTitulos.SelectedItem = d;
            }

            if (DatosSub.tipo.Equals("Capitulo"))
            {
                Titulo titu=null;
                Capitulo NumCap=null;
                foreach (var tit in _MyLey.ListaDeTitulos)
                {
                    titu = tit;
                    NumCap = tit.ListaCapitulos.Find(capi => capi.id.Equals(DatosSub.id));
                    break;
                }

                ClltionTitulos.SelectedItem = titu;
                await Navigation.PushAsync(new PageMotrarCapitulosConCodigos(titu, _MyLey, myAds,NumCap,null), false);

            }
            if (DatosSub.tipo.Equals("Articulo"))
            {
                Titulo titu = null;
                Capitulo NumCap = null;
                Articulo Numarticulo = null;
                foreach (var titulo in _MyLey.ListaDeTitulos)
                {
                    foreach (var capitulo in titulo.ListaCapitulos)
                    {

                        foreach (var aticulo in capitulo.ListaArticulos)
                        {
                            if (aticulo.id.Equals(DatosSub.id))
                            {
                                titu = titulo;
                                NumCap = capitulo;
                                Numarticulo = aticulo;
                                break;
                            }
                        }

                    }
                }

                ClltionTitulos.SelectedItem = titu;
                await Navigation.PushAsync(new PageMotrarCapitulosConCodigos(titu, _MyLey, myAds, NumCap, Numarticulo), false);
            }

                ClltionTitulos.SelectedItem = null;
        }
    }
}