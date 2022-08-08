using Acr.UserDialogs;
using IURIS.MOVIL.Modelos_y_clases.DB_Local;
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
    public partial class PageNuevoUser : ContentPage
    {
        public PageNuevoUser()
        {
            InitializeComponent();
        }

        private async void btnOK_Clicked(object sender, EventArgs e)
        {

            UserDialogs.Instance.ShowLoading("Creando\npor favor espere.", MaskType.None);
            await Task.Delay(500);

            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await Task.Delay(1000);

                if (string.IsNullOrWhiteSpace(EntryNombre.Text) && string.IsNullOrWhiteSpace(EntryApellidoPaterno.Text) && string.IsNullOrWhiteSpace(EntryApellidoMaterno.Text)) return;

                LocalSaveUser localSaveUser = new LocalSaveUser();
                if (localSaveUser.SaveUser(EntryNombre.Text, EntryApellidoPaterno.Text, EntryApellidoMaterno.Text))
                {
                   await Navigation.PushAsync(new FirtsView(), false);
                    UserDialogs.Instance.HideLoading();
                }
                else
                {
                    await DisplayAlert("","Error no se ha podido creare el usuario, intenta mas tarde","Aceptar");
                    await Navigation.PopAsync();
                }
            });

        }
    }
}