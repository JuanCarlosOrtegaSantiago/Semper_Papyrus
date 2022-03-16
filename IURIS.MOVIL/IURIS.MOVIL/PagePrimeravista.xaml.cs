using Acr.UserDialogs;
using IURIS.MOVIL.Modelos_y_clases.DB_Local.COMMON;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IURIS.MOVIL
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PagePrimeravista : ContentPage
    {
        public PagePrimeravista()
        {
            InitializeComponent();
            DatosAValidar();
        }

        private async void DatosAValidar()
        {
            UserDialogs.Instance.ShowLoading("Validando\npor favor espere.", MaskType.None);
            await Task.Delay(500);

            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await Task.Delay(1000);

                var usuarios = await App.Database.GetPeopleAsync();
                App.MyUser = usuarios.FirstOrDefault();

                if (App.MyUser != null) await Navigation.PushModalAsync(new FirtsView(), false);
                else lblEntrar.IsVisible = true;

                UserDialogs.Instance.HideLoading();
                await Navigation.PopAsync(false);
            });

        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageNuevoUser(), false);
        }
    }
}