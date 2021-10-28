using IURIS.COMMON.Entidades.UsuariosDeAplicacion;
using IURIS.MOVIL.Detail;
using IURIS.MOVIL.Views;
using IURIS.MOVIL.Views.ViewsCargarLey;
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
        readonly Usuarios _User;
        public ViewMaster(Usuarios usuarios)
        {
            InitializeComponent();
            _User = usuarios;
        }

        //private async void Primera_Clicked(object sender, EventArgs e)
        //{
        //    App.masterDetail.IsPresented = false;
        //    await App.masterDetail.Detail.Navigation.PushAsync(new Page1());
        //}

        //private async void Segundo_Clicked(object sender, EventArgs e)
        //{
        //    App.masterDetail.IsPresented = false;
        //    await App.masterDetail.Detail.Navigation.PushAsync(new Page1());

        //}

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            App.masterDetail.IsPresented = false;
            App.masterDetail.Detail = new NavigationPage(new ViewsMisClasificaciones(_User,null,_User.MisLeyes.Where(w=>w.CodigoLey==_User.MiUltimaLeyCargada).SingleOrDefault()));

            //await App.masterDetail.Detail.Navigation.PushAsync(new ViewsMisClasificaciones(User));
        }

        private  void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            App.masterDetail.IsPresented = false;
            App.masterDetail.Detail = new NavigationPage(new MisApuntes(_User,null));
            //await App.masterDetail.Detail.Navigation.PushAsync(new MisApuntes());
        }

        private void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {

            //revisar para que no inicie de 0
            App.masterDetail.IsPresented = false;
            App.masterDetail.Detail =  new NavigationPage(new ViewDetail(_User));
        }

        private void TapGestureRecognizer_Tapped_3(object sender, EventArgs e)
        {
            App.masterDetail.IsPresented = false;
            App.masterDetail.Detail = new NavigationPage(new WindowDeCopmpa(_User));
        }

        private void TapGestureRecognizer_Tapped_4(object sender, EventArgs e)
        {
            App.masterDetail.IsPresented = false;
            App.masterDetail.Detail = new NavigationPage(new WindowOpcionDeCompra(_User));
        }
    }
}