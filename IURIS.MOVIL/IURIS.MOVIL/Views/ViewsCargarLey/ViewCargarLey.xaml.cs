using IURIS.BIZ;
using IURIS.COMMON.Entidades.Ley;
using IURIS.COMMON.Entidades.Ley.ComponentesDeLey;
using IURIS.COMMON.Entidades.UsuariosDeAplicacion;
using IURIS.COMMON.Interfaces;
using IURIS.DAL;
using IURIS.MOVIL.Detail;
using IURIS.MOVIL.Utils;
using IURIS.MOVIL.Views.ViewsVentanasEmergentes;
using Rg.Plugins.Popup.Services;
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
        int tocadas = 0;
        Usuarios _User;
        Leyes _LeyeComprada;
        Leyes _LeyEncache=null;
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
            clltionLeyes.ItemsSource = _User.MisLeyes.OrderBy(e=>e.UltimaFechaDeModificacion);
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

            bool ExisteLey = false;

            if (_User.MisLeyes.Count < 4)
            {

                _LeyeComprada = manejadorDeLeyes.BuscarPorCodigo(EntryCodigo.Text);

                if (_LeyeComprada != null)
                {

                    foreach (var Ley in _User.MisLeyes)
                        if (_LeyeComprada.id == Ley.id)
                            ExisteLey = true;

                    if (!ExisteLey)
                    {

                        _User.MisLeyes.Add(_LeyeComprada);

                        if (manejadorDeUsuarioAplicacion.Modificar(_User))
                        {
                            _LeyeComprada.numDescargas += 1;
                            manejadorDeLeyes.Modificar(_LeyeComprada);
                            CargarDatos();
                        }
                        else
                            await DisplayAlert("", "Ocurrio un error\nIntente mas tarde", "OK");
                    }
                    else
                    {
                        await DisplayAlert("", "El codigo ingresado\nCorresponde a una ley que ya \nse encuentra en tu coleccion", "OK");
                    }
                }
                else
                {
                    await DisplayAlert("Error", "Codigo incorrecto\nIntenta de nuevo", "OK");
                }
            }
            else
            {
                await PopupNavigation.Instance.PushAsync(new WindowOfComprarEspacio());
            }

            Limpiardatos();
            //Leyes leyCopia=null;
            //bool Encontrado = false;

            //if (_User.MisLeyes.Count <= 4)
            //{
            //    CodigoVenta codigoVenta = new CodigoVenta();

            //    foreach (var Ley in manejadorDeLeyes.Listar)
            //    {
            //        foreach (var Codigo in Ley.CodigosDeVentas)
            //        {
            //            if (Codigo.CodigoDeVenta == EntryCodigo.Text)
            //            {
            //                _LeyeComprada = Ley;
            //                leyCopia = Ley;
            //                codigoVenta = Codigo;
            //                Encontrado = true;
            //                break;

            //            }
            //        }

            //        if (Encontrado)
            //            break;
            //    }

            //    if (_LeyeComprada == null)
            //    {
            //        return;
            //    }

            //    _LeyeComprada.CodigosDeVentas = null;

            //    _User.MisLeyes.Add(_LeyeComprada);
            //    if (manejadorDeUsuarioAplicacion.Modificar(_User))
            //    {
            //        leyCopia.numDescargas += 1;
            //        leyCopia.CodigosDeVentas.Remove(codigoVenta);
            //        manejadorDeLeyes.Modificar(leyCopia);
            //        CargarDatos();
            //        Limpiardatos();
            //    }
            //    else
            //    {
            //        await DisplayAlert("", "Ocurrio un error\nIntente mas tarde", "OK");
            //    }

            //}
            //else
            //{

            //}
            //}
            //else
            //{
            //    
            //    
            //}
        }

        private void Limpiardatos()
        {
            EntryCodigo.Text = null;
        }

        private async void clltionLeyes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (clltionLeyes.SelectedItem != null)
            {
                if ((Leyes)clltionLeyes.SelectedItem != _LeyEncache)
                {
                    tocadas = 0;
                    _LeyEncache = (Leyes)clltionLeyes.SelectedItem;
                    clltionLeyes.SelectedItem = null;
                }

                tocadas++;
                if (tocadas == 1)
                {
                    IMGBorrar.IsVisible = true;
                }

                if (tocadas == 2)
                {
                    //string tex = Settings.CodigoDeLeyCargada;
                    //Settings.CodigoDeLeyCargada.Replace(tex, ((Leyes)clltionLeyes.SelectedItem).CodigoLey);
                    _User.MiUltimaLeyCargada = ((Leyes)clltionLeyes.SelectedItem).CodigoLey;
                    if (manejadorDeUsuarioAplicacion.Modificar(_User))
                    {

                App.masterDetail.IsPresented = false;
                App.masterDetail.Detail = new NavigationPage(new ViewDetail(_User));
                    }
                    else
                    {
                        await DisplayAlert("error", "No se ha podido cargar la ley\n por favor intente mas tarde", "OK");
                    }

                }

            }
        }

        private void MenuItem_Clicked(object sender, EventArgs e)
        {
            //if (clltionLeyes.SelectedItem != null)
            //{
            //if (!(DisplayAlert("Borrar", "Realmente desea borrar la ley:\n" + _LeyEncache.NombreLey, "Si", "No").IsCompleted))
                _User.MisLeyes.Remove(_LeyEncache);
            if (manejadorDeUsuarioAplicacion.Modificar(_User))
            {
                DisplayAlert("", "Se borro la ley de tu colección", "Ok");
                CargarDatos();
            }
            //}
        }

        //private void clltionLeyes_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        //{
        //    if (clltionLeyes.SelectedItem != null)
        //    {
        //        Settings.CodigoDeLeyCargada = ((Leyes)clltionLeyes.SelectedItem).CodigoLey;
        //        App.masterDetail.IsPresented = false;
        //        App.masterDetail.Detail = new NavigationPage(new ViewDetail(_User));
        //    }
        //}
    }
}