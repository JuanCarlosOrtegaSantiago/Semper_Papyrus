using IURIS.COMMON.Entidades.Ley;
using IURIS.COMMON.Entidades.Ley.ComponentesDeLey;
using IURIS.COMMON.Entidades.UsuariosDeAplicacion;
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
    public partial class ViewDetail : ContentPage
    {


        public Usuarios usuario;
        Leyes Ley;
        public ViewDetail(Usuarios usuarios)
        {
            InitializeComponent();
            this.usuario = usuarios;

            Ley = usuario.MisLeyes.Where(e => e.CodigoLey == "12345ds").SingleOrDefault();
            Title = Ley.NombreLey;
            //ListTitulos.ItemsSource = Ley.ListaDeTitulos;
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