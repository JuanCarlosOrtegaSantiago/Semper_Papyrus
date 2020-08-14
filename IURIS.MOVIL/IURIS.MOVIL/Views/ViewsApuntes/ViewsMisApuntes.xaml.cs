using IURIS.COMMON.Entidades.UsuariosDeAplicacion;
using IURIS.COMMON.Entidades.UsuariosDeAplicacion.ComponentesDeUsuario;
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
        Usuarios _User;
        
        public ViewsMisApuntes(Usuarios usuarios)
        {
            InitializeComponent();

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
            if (apunte != null)
            {
                App.masterDetail.IsPresented = false;
                //MisApuntes misApuntes = new MisApuntes(_User, apunte);
                App.masterDetail.Detail = new NavigationPage(new MisApuntes(_User, apunte));
            }
        }
    }
}