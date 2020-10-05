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
    public partial class WindowOfMembresiaPlatino : PopupPage
    {
        public WindowOfMembresiaPlatino()
        {
            InitializeComponent();
            lbltitulo.Text = "Membresía platino";
            lblDescripcion.Text = "Sin comerciales\nEspacio ilimitado para leyes\nactualizaciones especiales";
        }

        private void BtnCancelar_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync(false);
        }
    }
}