using IURIS.COMMON.Entidades.Ley;
using IURIS.COMMON.Entidades.Ley.ComponentesDeLey;
using IURIS.COMMON.Entidades.UsuariosDeAplicacion;
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
        Leyes Ley;
        public ViewDetail(Usuarios usuarios)
        {
            InitializeComponent();
            this.usuario = usuarios;
            this.BindingContext = this;

            Ley = usuario.MisLeyes.Where(e => e.CodigoLey == "cnpp1").SingleOrDefault();
            Title = Ley.NombreLey;
            ClltionTitulos.ItemsSource = Ley.ListaDeTitulos;
            ClltionTitulos.SelectedItem = null;

            //ListTitulos.ItemsSource = Ley.ListaDeTitulos;
        }

        public ObservableCollection<Album> MyImagenes { get; set; }

        private void ClltionTitulos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Titulo titulo = ClltionTitulos.SelectedItem as Titulo;

            Navigation.PushAsync(new PageMotrarCapitulosConCodigos(titulo));
            //Navigation.PushAsync(new PageCapitulos(titulo));
        }

        public class Album
        {
            public string Image { get; set; }
            public string Description { get; set; }
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