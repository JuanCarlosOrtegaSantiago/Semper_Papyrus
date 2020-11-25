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
        readonly Usuarios _User;
        IManejadorDeUsuarioAplicacion manejadorDeUsuarioAplicacion;
        
        public ViewsMisApuntes(Usuarios usuarios)
        {
            InitializeComponent();
            manejadorDeUsuarioAplicacion = new ManejadorDeUsuarioAplicacion(new RepositorioGenerico<Usuarios>());
            _User = usuarios;

            DatosAInicializar();
        }

        private void DatosAInicializar()
        {
            LlenadosDeCampos();
        }

        private void LlenadosDeCampos()
        {
            clltionApuntes.ItemsSource = null;
            clltionApuntes.ItemsSource = _User.Apuntes;
        }

        private void clltionApuntes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Apunte apunte = (Apunte)clltionApuntes.SelectedItem;
            if (apunte == null) return;

                App.masterDetail.IsPresented = false;
                App.masterDetail.Detail = new NavigationPage(new MisApuntes(_User, apunte));
        }

        private void SwipeItemView_Invoked(object sender, EventArgs e)
        {
            var MiApunte = ((SwipeItemView)sender).BindingContext as Apunte;

            if (MiApunte == null) return;

            _User.Apuntes.Remove(MiApunte);

            if (!manejadorDeUsuarioAplicacion.Modificar(_User)) return;

            DisplayAlert("", "Se elimino el apunte", "Ok");
            LlenadosDeCampos();
        }
    }
}