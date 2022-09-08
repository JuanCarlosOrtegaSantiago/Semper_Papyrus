using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IURIS.MOVIL.Views.ViewsCargarLey.TabbPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabbedPageCargar_CambiarLey : TabbedPage
    {

        protected override bool OnBackButtonPressed()
        {
            Navigation.PopAsync(false);
            Navigation.PushModalAsync(new FirtsView(), false);
            return true;
        }
        public TabbedPageCargar_CambiarLey()
        {
            InitializeComponent();
        }
    }
}