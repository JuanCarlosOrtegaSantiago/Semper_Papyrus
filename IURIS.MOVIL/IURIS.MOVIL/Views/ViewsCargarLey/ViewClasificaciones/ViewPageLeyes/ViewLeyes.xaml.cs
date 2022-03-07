using Acr.UserDialogs;
using IURIS.BIZ;
using IURIS.COMMON.Entidades.Ley;
using IURIS.COMMON.Interfaces;
using IURIS.DAL;
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
                _Leyes = manejadorDeLeyes.Listar.Where(e => e.Clasificacion.ToUpper().Equals(Clasificacion.ToUpper())).ToList();
                clltionLeyes.ItemsSource = _Leyes;
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
    }
}