using IURIS.COMMON.Entidades.UsuariosDeAplicacion;
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

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            App.masterDetail.IsPresented = false;
            await App.masterDetail.Detail.Navigation.PushAsync(new ViewsMisClasificaciones(User));
        }

        private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            App.masterDetail.IsPresented = false;
            await App.masterDetail.Detail.Navigation.PushAsync(new MisApuntes());
        }
    }
}