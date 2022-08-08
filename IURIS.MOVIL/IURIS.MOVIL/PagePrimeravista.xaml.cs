using Acr.UserDialogs;
using IURIS.BIZ;
using IURIS.COMMON.Entidades.UsuariosDeAplicacion;
using IURIS.COMMON.Entidades.UsuariosDeAplicacion.ComponentesDeUsuario.DatosCriticos;
using IURIS.COMMON.Interfaces;
using IURIS.DAL;
using IURIS.MOVIL.Modelos_y_clases.DB_Local.COMMON;
using IURIS.MOVIL.Views.ViewsVentanasEmergentes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IURIS.MOVIL
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PagePrimeravista : ContentPage
    {

        IManejadorDeUsuarioAplicacion manejadorDeUsuarioAplicacion;
        public PagePrimeravista()
        {
            InitializeComponent();
            DatosAValidar();
        }


        public static bool HayConexion()
        {
            try
            {
                string huesped = $"8.8.8.8";

                return new Ping().Send(huesped).Status == IPStatus.Success;
            }
            catch
            {
                return false;
            }
        }


        private async void DatosAValidar()
        {
                    if (!HayConexion())
                    {
                
                await Navigation.PopAsync(false);
                await Navigation.PushAsync(new PageVistaSinInternet("Revisa tu conexión\na internet"), false);
                    return;
                    }
                UserDialogs.Instance.ShowLoading("Validando\npor favor espere.", MaskType.None);
                await Task.Delay(500);

                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await Task.Delay(1000);




                    
            try
            {

                        manejadorDeUsuarioAplicacion = new ManejadorDeUsuarioAplicacion(new RepositorioGenerico<Usuarios>());

                        var usuarios = await App.Database.GetPeopleAsync();
                        App.MyUser = usuarios.FirstOrDefault();

                        if (App.MyUser != null)
                        {

                            if (App.MyUser.IdApp == 0)
                            {
                                try
                                {
                                    Usuarios user = new Usuarios()
                                    {
                                        ApellidoMaterno = App.MyUser.ApellidoMaterno,
                                        ApellidoPaterno = App.MyUser.ApellidoPaterno,
                                        DatosSobreUsuario = new DatosSobreUsuarioParaLey() { NumLeyesPermitidas = App.MyUser.DatosSobreUsuario.NumLeyesPermitidas },
                                        Nombre = App.MyUser.Nombre,
                                        IdApp = manejadorDeUsuarioAplicacion.Listar.Count + 1
                                    };
                                    if (manejadorDeUsuarioAplicacion.AGREGAR(user))
                                    {
                                        App.MyUser.IdApp = user.IdApp;
                                        await App.Database.UpdateUserAsync(App.MyUser);
                                    }
                                }
                                catch (Exception)
                                {


                                }
                            }

                            Usuarios User = await manejadorDeUsuarioAplicacion.ConsultUsuario(App.MyUser.IdApp.ToString(), App.MyUser.Nombre, App.MyUser.ApellidoPaterno, App.MyUser.ApellidoMaterno);

                            if (User.DatosSobreUsuario.NumLeyesPermitidas != App.MyUser.DatosSobreUsuario.NumLeyesPermitidas)
                            {
                                App.MyUser.DatosSobreUsuario.NumLeyesPermitidas = User.DatosSobreUsuario.NumLeyesPermitidas;
                                await App.Database.UpdateUserAsync(App.MyUser);
                            }


                            await Navigation.PushModalAsync(new FirtsView(), false);
                        }
                        else { lblEntrar.IsVisible = true; }

                        UserDialogs.Instance.HideLoading();
                        await Navigation.PopAsync(false);
            }
            catch (Exception)
            {
                        UserDialogs.Instance.HideLoading();
                        
                        await Navigation.PushAsync(new PageVistaSinInternet("Ocurrio un error\nintente mas tarde"), false);
            }
                    
                });

        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageNuevoUser(), false);
        }
    }
}