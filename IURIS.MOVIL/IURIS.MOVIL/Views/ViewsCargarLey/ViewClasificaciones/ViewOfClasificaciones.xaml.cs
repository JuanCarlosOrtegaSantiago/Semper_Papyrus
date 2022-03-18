using Acr.UserDialogs;
using IURIS.BIZ;
using IURIS.COMMON.Entidades.Ley.ClasificacionDeLey;
using IURIS.COMMON.Interfaces;
using IURIS.DAL;
using IURIS.MOVIL.Modelos_y_clases.DB_Local.COMMON;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IURIS.MOVIL.Views.ViewsCargarLey.ViewClasificaciones
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewOfClasificaciones : ContentPage
    {
        IManejadorDeClasificaciones manejadorDeClasificaciones;
        public ViewOfClasificaciones()
        {
            UserDialogs.Instance.ShowLoading("Obteniendo datos...", MaskType.None);
            InitializeComponent();
            Task.Delay(2000);
            clltionClasificaciones.ItemsSource = App.MyClasificacionDeLey.GetPeopleAsync().Result;
            UserDialogs.Instance.HideLoading();

        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            SearchText(EntryCodigo.Text);
        }

        private void SearchText(string text)
        {
            clltionClasificaciones.ItemsSource = App.MyClasificacionDeLey.GetPeopleAsync().Result.Where(e=>e.Nombre.ToUpper().Contains(text.ToUpper()));
        }

        private void EntryCodigo_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchText(e.NewTextValue);
        }

        private async void clltionClasificaciones_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var x= clltionClasificaciones.SelectedItem as MyClasificacionDeLey;
            await Navigation.PushAsync(new ViewLeyes.ViewLeyes(x.Nombre), false);
            //clltionClasificaciones.SelectedItem = null;
        }
    }
}