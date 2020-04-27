using IURIS.BIZ;
using IURIS.COMMON.Entidades.Ley;
using IURIS.COMMON.Interfaces;
using IURIS.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IURIS.APP.GUI
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
            leyes = manejadorDeLeyes.BuscarLey("vrsdasax");
            this.Title = leyes.NombreLey;
            ListTitulos.ItemsSource = leyes.ListaDeTitulos;

        }
    }
}