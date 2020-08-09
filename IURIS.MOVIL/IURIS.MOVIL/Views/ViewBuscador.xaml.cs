using IURIS.COMMON.Entidades.Ley;
using IURIS.COMMON.Entidades.Ley.ComponentesDeLey;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IURIS.MOVIL.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewBuscador : ContentPage
    {
        Leyes _ley;
        public ViewBuscador(Leyes leyes)
        {
            InitializeComponent();
            _ley = leyes;

            ClltionTitulos.ItemsSource = null;
            ClltionTitulos.ItemsSource = _ley.ListaDeTitulos;
        }

        private void SearchViewDetail_TextChanged(object sender, TextChangedEventArgs e)
        {
            //DisplayAlert("", e.NewTextValue, "ok");
            BuscarTexto(e);
        }

        private void SearchViewDetail_SearchButtonPressed(object sender, EventArgs e)
        {
            //SearchViewDetail.IsVisible = false;
            //BuscarTexto(SearchViewDetail.Text);
        }

        private void BuscarTexto(TextChangedEventArgs TextChange)
        {

            //string text = ; 

            List<Titulo> titulos = new List<Titulo>();

            titulos = _ley.ListaDeTitulos.ToList().Where(e => e.NumTitulo.ToUpper().Contains(TextChange.NewTextValue.ToUpper()) == true || e.NombreTitulo.ToUpper().Contains(TextChange.NewTextValue.ToUpper()) == true).ToList();

            ClltionTitulos.ItemsSource = null;
            ClltionTitulos.ItemsSource = titulos;
            //return repositorio.Read.Where(e => e.NombreLey.ToUpper().Contains(BuscarLey.ToUpper()) == true || e.CodigoLey.ToUpper().Contains(BuscarLey.ToUpper()) == true).OrderByDescending(e => e.UltimaFechaDeModificacion).ToList();

        }
    }
}