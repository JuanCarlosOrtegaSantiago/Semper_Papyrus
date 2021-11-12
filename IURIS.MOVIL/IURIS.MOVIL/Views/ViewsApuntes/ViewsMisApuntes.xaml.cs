using IURIS.BIZ;
using IURIS.COMMON.Entidades.UsuariosDeAplicacion;
using IURIS.COMMON.Entidades.UsuariosDeAplicacion.ComponentesDeUsuario;
using IURIS.COMMON.Interfaces;
using IURIS.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IURIS.MOVIL.Views.ViewsApuntes
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewsMisApuntes : ContentPage
    {
        public ViewsMisApuntes()
        {
            InitializeComponent();
            DatosAInicializar();
        }

        private void DatosAInicializar()
        {
            LlenadosDeCampos();
        }

        private void LlenadosDeCampos()
        {
            clltionApuntes.ItemsSource = null;
            clltionApuntes.ItemsSource = App.MyUser.Apuntes;
        }

        private void clltionApuntes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Apunte apunte = (Apunte)clltionApuntes.SelectedItem;
            if (apunte == null) return;

                App.masterDetail.IsPresented = false;
                App.masterDetail.Detail = new NavigationPage(new MisApuntes(apunte));
        }

        private async void SwipeItemView_Invoked(object sender, EventArgs e)
        {
            var MiApunte = ((SwipeItemView)sender).BindingContext as Apunte;

            if (MiApunte == null) return;

            App.MyUser.Apuntes.Remove(MiApunte);

            if (!await App.Database.UpdateUserAsync(App.MyUser)) return;

            await DisplayAlert("", "Se elimino el apunte", "Ok");
            LlenadosDeCampos();
        }
    }
}