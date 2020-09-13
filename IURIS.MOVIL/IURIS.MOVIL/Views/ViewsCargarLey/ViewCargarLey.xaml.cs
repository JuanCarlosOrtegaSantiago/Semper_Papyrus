using IURIS.BIZ;
using IURIS.COMMON.Entidades.Ley;
using IURIS.COMMON.Entidades.UsuariosDeAplicacion;
using IURIS.COMMON.Interfaces;
using IURIS.DAL;
using IURIS.MOVIL.Detail;
using IURIS.MOVIL.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IURIS.MOVIL.Views.ViewsCargarLey
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewCargarLey : ContentPage
    {
        IManejadorDeLeyes manejadorDeLeyes;
        IManejadorDeUsuarioAplicacion manejadorDeUsuarioAplicacion;

        Usuarios _User;
        Leyes _LeyeComprada;
        public ViewCargarLey(Usuarios usuarios)
        {
            InitializeComponent();
            _User = usuarios;

            CargarDatos();
            manejadorDeLeyes = new ManejadorDeLeyes(new RepositorioGenerico<Leyes>());
            manejadorDeUsuarioAplicacion = new ManejadorDeUsuarioAplicacion(new RepositorioGenerico<Usuarios>());
        }

        private void CargarDatos()
        {
            clltionLeyes.ItemsSource = null;
            clltionLeyes.ItemsSource = _User.MisLeyes;
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (manejadorDeLeyes.BuscarPorCodigo(EntryCodigo.Text))
            {
                _LeyeComprada = manejadorDeLeyes.MostrarLeyes.Where(x => x.CodigoLey == EntryCodigo.Text).SingleOrDefault();
                _User.MisLeyes.Add(_LeyeComprada);
                if (manejadorDeUsuarioAplicacion.Modificar(_User))
                {
                _LeyeComprada.numDescargas += 1;
                    manejadorDeLeyes.Modificar(_LeyeComprada);
                    CargarDatos();
                    Limpiardatos();
                }
                else
                {
                    await DisplayAlert("", "Ocurrio un error\nIntente mas tarde", "OK");
                }
            }
            else
            {
                await DisplayAlert("Error", "Codigo incorrecto\nIntenta de nuevo", "OK");
            }
        }

        private void Limpiardatos()
        {
            EntryCodigo.Text = null;
        }

        private void clltionLeyes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (clltionLeyes.SelectedItem != null)
            {
                Settings.CodigoDeLeyCargada = ((Leyes)clltionLeyes.SelectedItem).CodigoLey;
                App.masterDetail.IsPresented = false;
                App.masterDetail.Detail = new NavigationPage(new ViewDetail(_User));
            }
        }
    }
}