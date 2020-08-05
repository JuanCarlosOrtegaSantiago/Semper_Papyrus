using IURIS.COMMON.Entidades.Ley.ComponentesDeLey;
using IURIS.COMMON.Entidades.UsuariosDeAplicacion;
using IURIS.MOVIL.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IURIS.MOVIL.Detail
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageMotrarCapitulosConCodigos : ContentPage
    {
        Titulo _titulo;
        Usuarios _Usuario;
        public bool _ArticuloSeleccionado;
        public PageMotrarCapitulosConCodigos(Titulo titulo, Usuarios usuarios)
        {
            InitializeComponent();
            
            _titulo = titulo;
            _Usuario = usuarios;

            DatosAInicializar();
        }


        private void DatosAInicializar()
        {
            

            lblTitle.Text = _titulo.NombreTitulo;

            ActualizarDatosCapitulo(_titulo.ListaCapitulos);

        }

        private void ActualizarDatosCapitulo(List<Capitulo> listaCapitulos)
        {
            clltionCapitulos.SelectedItem = null;

            clltionCapitulos.ItemsSource = null;
            clltionCapitulos.ItemsSource = listaCapitulos;
        }

        private void clltionCapitulos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Capitulo capitulo = clltionCapitulos.SelectedItem as Capitulo;
            if ( capitulo != null)
            {
                ActualizarDatosArticulo(capitulo.ListaArticulos);
            }
        }

        private void ActualizarDatosArticulo(List<Articulo> listaArticulos)
        {
                cllctionArticulos.SelectedItem = null;

                cllctionArticulos.ItemsSource = null;
                cllctionArticulos.ItemsSource = listaArticulos;
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            //Navigation.PushAsync(new ViewBuscador(Ley), true);
            MostrarSearch(true);

        }


        private void BuscarTexto(TextChangedEventArgs TextChange)
        {
            if (SearchViewDetailTitle.Text != null)
            {
                List<Articulo> _ListaArticulos= new List<Articulo>();

                List<Capitulo> _ListaCapitulos = new List<Capitulo>();

                _ListaCapitulos = _titulo.ListaCapitulos.ToList().Where(e => e.NumCapitulo.ToUpper().Contains(TextChange.NewTextValue.ToUpper()) == true || e.NombreCapitulo.ToUpper().Contains(TextChange.NewTextValue.ToUpper()) == true).ToList();

                foreach (var item in _titulo.ListaCapitulos)
                {
                    _ListaArticulos = item.ListaArticulos.ToList().Where(e => e.NumArticulo.ToUpper().Contains(TextChange.NewTextValue.ToUpper()) == true || e.NombreArticulo.ToUpper().Contains(TextChange.NewTextValue.ToUpper()) == true || e.Contenido.ToUpper().Contains(TextChange.NewTextValue.ToUpper()) == true).ToList();
                }

                ActualizarDatosCapitulo(_ListaCapitulos);
                ActualizarDatosArticulo(_ListaArticulos);
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
            GridTituloEIMGBuscador.IsVisible = !v;

            //SearchViewDetail.IsVisible = v;
        }

        private void cllctionArticulos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cllctionArticulos.SelectedItem != null)
            {

            }
            //expanderGeneric.
            //Articulo articulo = cllctionArticulos.SelectedItem as Articulo;
            //if (articulo != null)
            //{
            //    var x=_titulo.ListaCapitulos.Where(e=>e.)
            //    clltionCapitulos.SelectedItem = _titulo.ListaCapitulos.Skip(1).FirstOrDefault();
            //        //Monkeys.Skip(3).FirstOrDefault(); ;
            //}

        }

        private async void LblClasificacionPersonalizada(object sender, EventArgs e)
        {
            if (_ArticuloSeleccionado)
            {
            Articulo articulo = cllctionArticulos.SelectedItem as Articulo;
                await Navigation.PushAsync(new ViewsMisClasificaciones(_Usuario,articulo));
            }
        }

        private void EsArticuloSeleccionado(object sender, EventArgs e)
        {
            Articulo articulo = cllctionArticulos.SelectedItem as Articulo;
            if (articulo != null)
                _ArticuloSeleccionado = true;
            else
                return;
        }
    }
}