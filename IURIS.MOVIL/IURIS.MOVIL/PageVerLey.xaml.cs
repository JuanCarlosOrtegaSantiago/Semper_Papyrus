using IURIS.BIZ;
using IURIS.COMMON.Entidades.Ley;
using IURIS.COMMON.Entidades.Ley.ComponentesDeLey;
using IURIS.COMMON.Interfaces;
using IURIS.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IURIS.MOVIL
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageVerLey : ContentPage
    {
        IManejadorDeLeyes manejadorDeLeyes;

        public PageVerLey()
        {
            InitializeComponent();
            manejadorDeLeyes = new ManejadorDeLeyes(new RepositorioGenerico<Leyes>());


            Leyes leyes;
            leyes = manejadorDeLeyes.BuscarLey("MiEjemplo");
            this.Title = leyes.NombreLey;
            ListTitulos.ItemsSource = leyes.ListaDeTitulos;

        }

        private void ListTitulos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var detali = e.SelectedItem as Titulo;
            if (ListTitulos.HasUnevenRows == false)
            {
                ListTitulos.HasUnevenRows = true;
            }
            else
            {
                ListTitulos.HasUnevenRows = false;
            }
        }

        private void ListTitulos_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var detali = e.Item as Titulo;
            if (ListTitulos.HasUnevenRows == false)
            {
                ListTitulos.HasUnevenRows = true;
            }
            else
            {
                ListTitulos.HasUnevenRows = false;
            }


        }

    }
}