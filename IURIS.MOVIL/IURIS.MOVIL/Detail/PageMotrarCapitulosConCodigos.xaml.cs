using IURIS.COMMON.Entidades.Ley;
using IURIS.COMMON.Entidades.Ley.ComponentesDeLey;
using IURIS.COMMON.Entidades.UsuariosDeAplicacion;
using IURIS.MOVIL.Views;
using IURIS.MOVIL.Views.ViewsVentanasEmergentes;
using IURIS.MOVIL.Utils;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using IURIS.MOVIL.Modelos_y_clases;

namespace IURIS.MOVIL.Detail
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageMotrarCapitulosConCodigos : ContentPage
    {
        readonly Titulo _titulo;
        readonly Usuarios _Usuario;
        readonly Leyes _ley;
        public bool _ArticuloSeleccionado;
        static int _NumRecultadosEncontrados = 0;
        public Capitulo _Capitulo;
        public Articulo _Articulo;
        bool isRefreshing;


        public PageMotrarCapitulosConCodigos(Titulo titulo, Usuarios usuarios, Leyes ley)
        {
            InitializeComponent();
            BindingContext = this;

            _titulo = titulo;
            _Usuario = usuarios;
            _ley = ley;

            DatosAInicializar();
        }

        public bool IsRefreshing
        {
            get => isRefreshing;
            set
            {
                isRefreshing = value;
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }

        public ICommand RefreshCommand
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        if (_Capitulo!=null) ActualizarDatosArticulo(_Capitulo.ListaArticulos);
                        else ActualizarDatosCapitulo(_titulo.ListaCapitulos);
                        //await Task.Delay(1500); // Only to demonstrate refresh views..
                    }
                    finally
                    {
                        IsRefreshing = false;
                    }
                });
            }
        }
        private void DatosAInicializar()
        {
            lblTitle.Text = _titulo.NombreTitulo;
            lblCodigo.Text = _ley.CodigoLey;

            ActualizarDatosCapitulo(_titulo.ListaCapitulos);

        }

        private void ActualizarDatosCapitulo(List<Capitulo> listaCapitulos)
        {
            clltionCapitulos.ItemsSource = null;
            clltionCapitulos.ItemsSource = listaCapitulos;

            cllctionArticulos.ItemsSource = null;
            cllctionArticulos.ItemsSource = listaCapitulos.FirstOrDefault().ListaArticulos;
            _Capitulo = listaCapitulos.FirstOrDefault();
        }

        private void ActualizarDatosArticulo(List<Articulo> listaArticulos)
        {
                cllctionArticulos.SelectedItem = null;

                cllctionArticulos.ItemsSource = null;
                cllctionArticulos.ItemsSource = listaArticulos;
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            MostrarSearch(true);
        }


        private void BuscarTexto(TextChangedEventArgs TextChange)
        {


            //Revisar el por que los resultados se muestran en formato horizontal
            if (SearchViewDetailTitle.Text != null)
            {
                List<Articulo> _ListaArticulos= new List<Articulo>();
                List<Articulo> _ListaArticulosGenerico= new List<Articulo>();

                List<Capitulo> _ListaCapitulos = new List<Capitulo>();

                foreach (var item in _titulo.ListaCapitulos)
                {
                    _ListaArticulosGenerico = item.ListaArticulos.ToList().Where(e => e.NumArticulo.ToUpper().Contains(TextChange.NewTextValue.ToUpper()) == true || e.NombreArticulo.ToUpper().Contains(TextChange.NewTextValue.ToUpper()) == true || e.Contenido.ToUpper().Contains(TextChange.NewTextValue.ToUpper()) == true).ToList();

                foreach (var ati in _ListaArticulosGenerico)
                {
                    _ListaArticulos.Add(ati);
                }
                }

                
                _ListaCapitulos = _titulo.ListaCapitulos.ToList().Where(e => e.NumCapitulo.ToUpper().Contains(TextChange.NewTextValue.ToUpper()) == true || e.NombreCapitulo.ToUpper().Contains(TextChange.NewTextValue.ToUpper()) == true).ToList();


                ActualizarDatosCapitulo(_ListaCapitulos);
                ActualizarDatosArticulo(_ListaArticulos);

                _NumRecultadosEncontrados = _ListaArticulos.Count + _ListaCapitulos.Count;

                lblNumResultados.Text = TextChange.NewTextValue == ""? "" : _NumRecultadosEncontrados.ToString();
            }

        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            MostrarSearch(false);
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

        private void MostrarSearch(bool v)
        {
            SearchViewDetailTitle.IsVisible = v;
            lblNumResultados.IsVisible = v;
            lblNumResultados.Text = null;
            GridTituloEIMGBuscador.IsVisible = !v;
        }

        private async void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            var articulo = ((Image)sender).BindingContext as Articulo;
            if (articulo == null) return;

            await PopupNavigation.Instance.PushAsync(new WindowOfEmergencyCrearNota(_titulo, _Usuario, articulo, _ley, _Capitulo), false);

        }

        private async void clltionCapitulos_PositionChanged(object sender, PositionChangedEventArgs e)
        {
            var Cap = ((CarouselView)sender).CurrentItem as Capitulo;
            if (!(Cap is Capitulo)) return;

            ActualizarDatosArticulo(Cap.ListaArticulos);
            _Capitulo = Cap;

            ClassMostrarP_Cmpra _Cmpra = new ClassMostrarP_Cmpra();
            if (_Cmpra.MostrarPantalla()) await PopupNavigation.Instance.PushAsync(new WindowOfMembresiaPlatino());

        }

        private async void TapGestureRecognizer_Tapped_3(object sender, EventArgs e)
        {
            var Cap = ((ContentView)sender).BindingContext as Capitulo;

            if (!(Cap is Capitulo)) return;

            ActualizarDatosArticulo(Cap.ListaArticulos);
            _Capitulo = Cap;

            ClassMostrarP_Cmpra _Cmpra = new ClassMostrarP_Cmpra();
            if (_Cmpra.MostrarPantalla()) await PopupNavigation.Instance.PushAsync(new WindowOfMembresiaPlatino());

        }

        private async void TapGestureRecognizer_Tapped_4(object sender, EventArgs e)
        {

            var articulo = ((Image)sender).BindingContext as Articulo;
            if (articulo == null) return;

            await PopupNavigation.Instance.PushAsync(new WindowOfMenuAccion(_titulo, _Usuario, articulo, _ley, _Capitulo), false);
            
        }
    }
}