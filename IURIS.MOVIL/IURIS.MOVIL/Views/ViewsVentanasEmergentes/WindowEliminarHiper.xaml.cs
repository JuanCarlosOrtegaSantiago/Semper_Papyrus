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
    public partial class WindowEliminarHiper : PopupPage
    {

        Articulo _Art;
        MyLey _MyLey;

        public WindowEliminarHiper(Articulo art, MyLey myLey)
        {
            InitializeComponent();
            _Art = art;
            _MyLey = myLey;
        }

        private async void LblCancelar_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync(false);
        }

        private async void LblAceptar_Clicked(object sender, EventArgs e)
        {
            try
            {
                _Art.TieneHipervinculo = false;
                var hip=_MyLey.Hipervinculos.Where(r => r._Articulo.NumArticulo.Equals(_Art.NumArticulo)).SingleOrDefault();
                _MyLey.Hipervinculos.Remove(hip);

                await App.Database.UpdateUserAsync(App.MyUser);
                await PopupNavigation.Instance.PopAsync(false);
            }
            catch (Exception)
            {
                await PopupNavigation.Instance.PopAsync(false);
            }
        }
    }
}