using IURIS.COMMON.Entidades.Ley.ComponentesDeLey;
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
    public partial class PageCapitulos : ContentPage
    {
        Titulo _Titulo;
        public PageCapitulos(Titulo titulo)
        {
            InitializeComponent();
            _Titulo = titulo;

            DatosAIniciar();
        }

        private void DatosAIniciar()
        {
            Title = _Titulo.NombreTitulo;

            ClltionCapitulos.ItemsSource = null;
            ClltionCapitulos.ItemsSource = _Titulo.ListaCapitulos;

        }

        private void ClltionCapitulos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Capitulo capitulo= ClltionCapitulos.SelectedItem as Capitulo;
            Navigation.PushAsync(new PageArticulos(capitulo));
        }
    }
}