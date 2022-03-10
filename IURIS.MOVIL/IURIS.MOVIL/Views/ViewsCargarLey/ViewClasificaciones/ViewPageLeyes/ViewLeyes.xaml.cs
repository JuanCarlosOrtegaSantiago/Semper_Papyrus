using Acr.UserDialogs;
using IURIS.BIZ;
using IURIS.COMMON.Entidades.Ley;
using IURIS.COMMON.Interfaces;
using IURIS.DAL;
using IURIS.MOVIL.Modelos_y_clases.Tools;
using IURIS.MOVIL.Views.ViewsCargarLey.TabbPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IURIS.MOVIL.Views.ViewsCargarLey.ViewClasificaciones.ViewLeyes
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewLeyes : ContentPage
    {
        IManejadorDeLeyes manejadorDeLeyes;
        List<Leyes> _Leyes;
        public ViewLeyes(string Clasificacion)
        {
            UserDialogs.Instance.ShowLoading("Descargando leyes\nde nuestro servidor", MaskType.None);
            Task.Delay(500);
            InitializeComponent();

            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await Task.Delay(1000);
                manejadorDeLeyes = new ManejadorDeLeyes(new RepositorioGenerico<Leyes>());
                _Leyes = await manejadorDeLeyes.Consults(Clasificacion);
                clltionLeyes.ItemsSource = _Leyes;
                // clltionLeyes.ItemsSource = _Leyes;
                UserDialogs.Instance.HideLoading();
            });
        }

        private void EntryCodigo_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchText(e.NewTextValue);
        }

        private void SearchText(string text)
        {
            clltionLeyes.ItemsSource = _Leyes.Where(e=>e.NombreLey.ToUpper().Contains(text.ToUpper()));
        }

        private void EntryCodigo_Completed(object sender, EventArgs e)
        {
            SearchText(EntryCodigo.Text);
        }

        private async void clltionLeyes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Buscando ley", MaskType.Gradient);
                await Task.Delay(1000);


                HerramientasGenerales herramientasGenerales = new HerramientasGenerales();
                string x = await herramientasGenerales.AgragarLEyAsync(((Leyes)clltionLeyes.SelectedItem).CodigoLey);
                string[] subs = x.Split('|');

                if (subs[0] != "OK")
                {
                    await DisplayAlert(subs[0], subs[1], subs[2]);
                }
                App.masterDetail.IsPresented = false;
                //App.masterDetail.Detail = new NavigationPage(new WindowDeCopmpa());
                App.masterDetail.Detail = new NavigationPage(new TabbedPageCargar_CambiarLey());



            }
            catch (Exception ex)
            {
                UserDialogs.Instance.HideLoading();
            }
            
        }
    }
}