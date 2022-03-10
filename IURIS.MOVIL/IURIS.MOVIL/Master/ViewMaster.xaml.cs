using IURIS.COMMON.Entidades.UsuariosDeAplicacion;
using IURIS.MOVIL.Detail;
using IURIS.MOVIL.Modelos_y_clases.DB_Local.COMMON;
using IURIS.MOVIL.Utils;
using IURIS.MOVIL.Views;
using IURIS.MOVIL.Views.ViewsCargarLey;
using IURIS.MOVIL.Views.ViewsCargarLey.TabbPage;
using IURIS.MOVIL.Views.ViewsCargarLey.ViewClasificaciones;
using IURIS.MOVIL.Views.ViewsVentanasEmergentes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IURIS.MOVIL.Master
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewMaster : ContentPage
    {
        public ViewMaster()
        {
            InitializeComponent();
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            App.masterDetail.IsPresented = false;
            App.masterDetail.Detail = new NavigationPage(new ViewsMisClasificaciones(null,App.MyUser.MisLeyes.Where(w=>w.CodigoLey==App.MyUser.MiUltimaLeyCargada).SingleOrDefault()));
        }

        private  void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            App.masterDetail.IsPresented = false;
            App.masterDetail.Detail = new NavigationPage(new MisApuntes(null));
        }

        private void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            App.masterDetail.IsPresented = false;
            App.masterDetail.Detail =  new NavigationPage(new ViewDetail());
        }

        private void TapGestureRecognizer_Tapped_3(object sender, EventArgs e)
        {
            App.masterDetail.IsPresented = false;
            //App.masterDetail.Detail = new NavigationPage(new WindowDeCopmpa());
            App.masterDetail.Detail = new NavigationPage(new TabbedPageCargar_CambiarLey());

        }

        private void TapGestureRecognizer_Tapped_4(object sender, EventArgs e)
        {
            App.masterDetail.IsPresented = false;
            App.masterDetail.Detail = new NavigationPage(new WindowOpcionDeCompra());
        }

        private async void TapGestureRecognizer_Tapped_5(object sender, EventArgs e)
        {
            Settings.Recuerdame = false;
            Settings.Email = "";
            Settings.Contrasenia = "";
            var ultimosUsuarios = await App.ultimoUser.GetUltimoUserAsync();
            App.ultimoUser.DeleteUltimoUserAsync(ultimosUsuarios.FirstOrDefault());

            App.masterDetail.IsPresented = false;
            await Navigation.PushAsync(new PageInicioDeSesion(), false);

        }
    }
}