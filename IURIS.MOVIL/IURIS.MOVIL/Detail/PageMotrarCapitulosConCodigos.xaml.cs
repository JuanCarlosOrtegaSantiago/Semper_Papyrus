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
    public partial class PageMotrarCapitulosConCodigos : ContentPage
    {
        Titulo _titulo;
        public PageMotrarCapitulosConCodigos(Titulo titulo)
        {
            InitializeComponent();
            _titulo = titulo;
            DatosAInicializar();
        }

        private void DatosAInicializar()
        {
            Title = _titulo.NombreTitulo;
            clltionCapitulos.ItemsSource = null;
            clltionCapitulos.ItemsSource = _titulo.ListaCapitulos;
        }
    }
}