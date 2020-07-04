using IURIS.COMMON.Entidades.UsuariosDeAplicacion;
using IURIS.MOVIL.Detail;
using IURIS.MOVIL.Views;
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
        Usuarios User;
        public ViewMaster(Usuarios usuarios)
        {
            InitializeComponent();
            User = usuarios;
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
            App.masterDetail.Detail = new NavigationPage(new ViewsMisClasificaciones(User));

            //await App.masterDetail.Detail.Navigation.PushAsync(new ViewsMisClasificaciones(User));
        }

        private  void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            App.masterDetail.IsPresented = false;
            App.masterDetail.Detail = new NavigationPage(new MisApuntes());
            //await App.masterDetail.Detail.Navigation.PushAsync(new MisApuntes());
        }

        private void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {

            //revisar para que no inicie de 0
            App.masterDetail.IsPresented = false;
            App.masterDetail.Detail =  new NavigationPage(new ViewDetail(User));
        }
    }
}