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
    public partial class PageArticulos : ContentPage
    {
        Capitulo _Capitulo;
        public PageArticulos(Capitulo capitulo)
        {
            InitializeComponent();
            _Capitulo = capitulo;

            DatosAIniciar();
        }

        private void DatosAIniciar()
        {
            Title = _Capitulo.NombreCapitulo;
            cllctionArticulos.ItemsSource = null;
            cllctionArticulos.ItemsSource = _Capitulo.ListaArticulos;
        }
    }
}