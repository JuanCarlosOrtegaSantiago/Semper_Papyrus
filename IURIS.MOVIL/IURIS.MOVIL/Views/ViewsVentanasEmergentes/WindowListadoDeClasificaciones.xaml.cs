using IURIS.COMMON.Entidades.Ley.ComponentesDeLey;
using IURIS.MOVIL.Modelos_y_clases.DB_Local.COMMON;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IURIS.MOVIL.Views.ViewsVentanasEmergentes
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WindowListadoDeClasificaciones : PopupPage
    {
        MyLey _MyLey;
        Articulo _Articulo;
        public WindowListadoDeClasificaciones(MyLey Myley, Articulo articulo)
        {
            InitializeComponent();
            _MyLey = Myley;
            _Articulo = articulo;
            clltionClasificaciones.ItemsSource = App.MyUser.MisLeyes.Find(d=>d.CodigoLey==_MyLey.CodigoLey).Clasificaciones;
        }

        private async void clltionClasificaciones_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var classi=clltionClasificaciones.SelectedItem as ClasificacionPUsuario;
            if (classi != null)
            {
                
                try
                {
                    _MyLey.Clasificaciones.Find(clasi => clasi.Nombre.Equals(classi.Nombre)).MisArticulos.Add(_Articulo);
                    await PopupNavigation.Instance.PopAllAsync(false);

                    string mensaje = await App.Database.UpdateUserAsync(App.MyUser) ? "Artículo agregado"  :  "Intente más tarde" ;


                    await PopupNavigation.Instance.PushAsync(new WindowAlert(mensaje), false);
                    await Task.Delay(2000);
                    await PopupNavigation.Instance.PopAsync(false);
                    return;
                }
                catch (Exception)
                {
                    await PopupNavigation.Instance.PopAllAsync(false);
                    return;

                }
            }
        }

        private async void LblCancelar_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAllAsync(false);
        }

        private async void LblAceptar_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAllAsync(false);
            await PopupNavigation.Instance.PushAsync(new WindowOfEmergencyNuevaClasificacion(_MyLey, _Articulo));
        }
    }
}