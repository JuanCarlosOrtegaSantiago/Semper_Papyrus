using IURIS.COMMON.Entidades.Ley;
using IURIS.COMMON.Entidades.Ley.ComponentesDeLey;
using IURIS.COMMON.Entidades.UsuariosDeAplicacion;
using IURIS.MOVIL.Utils;
using IURIS.MOVIL.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IURIS.MOVIL.Detail
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewDetail : ContentPage
    {


        public Usuarios usuario;
        Leyes _Ley;
        public ViewDetail(Usuarios usuarios)
        {
            InitializeComponent();
            this.usuario = usuarios;
            this.BindingContext = _Ley;

            
            DatosAInicializar();
            //MostrarSearch(false);
        }

        private void DatosAInicializar()
        {
            //_Ley = usuario.MisLeyes.Where(e => e.CodigoLey == "cnpp1").SingleOrDefault();
            _Ley = usuario.MisLeyes.Where(e => e.CodigoLey == Settings.CodigoDeLeyCargada).SingleOrDefault();
            lblTitle.Text = _Ley.NombreLey;

            ActualizarDatos(_Ley.ListaDeTitulos);

            ClltionTitulos.SelectedItem = null;
        }

        private void ActualizarDatos(List<Titulo> _MiLista)
        {
            ClltionTitulos.SelectedItem = null;

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
            //SearchViewDetail.IsVisible = v;
        }

        private async void ClltionTitulos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            Titulo titulo = ClltionTitulos.SelectedItem as Titulo;
            if (titulo != null) { 

            MostrarSearch(false);
            await Navigation.PushAsync(new PageMotrarCapitulosConCodigos(titulo,usuario,_Ley),false);
            }
            //Navigation.PushAsync(new PageCapitulos(titulo));
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

            List<Titulo> titulos = new List<Titulo>();

             titulos= _Ley.ListaDeTitulos.ToList().Where(e => e.NumTitulo.ToUpper().Contains(TextChange.NewTextValue.ToUpper()) == true || e.NombreTitulo.ToUpper().Contains(TextChange.NewTextValue.ToUpper()) == true).ToList();

            ActualizarDatos(titulos);
            lblNumResultados.Text = TextChange.NewTextValue == "" ? "" : titulos.Count.ToString();
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

        //private void ListTitulos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        //{
        //    var detali = e.SelectedItem as Titulo;
        //    if (ListTitulos.HasUnevenRows == false)
        //    {
        //        ListTitulos.HasUnevenRows = true;
        //    }
        //    else
        //    {
        //        ListTitulos.HasUnevenRows = false;
        //    }
        //}

        //private void ListTitulos_ItemTapped(object sender, ItemTappedEventArgs e)
        //{
        //    var detali = e.Item as Titulo;
        //    if (ListTitulos.HasUnevenRows == false)
        //    {
        //        ListTitulos.HasUnevenRows = true;
        //    }
        //    else
        //    {
        //        ListTitulos.HasUnevenRows = false;
        //    }


        //}
    }
}