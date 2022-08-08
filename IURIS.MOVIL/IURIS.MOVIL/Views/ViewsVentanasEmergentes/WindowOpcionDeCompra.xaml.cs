using Acr.UserDialogs;
using IURIS.BIZ;
using IURIS.COMMON.Entidades.UsuariosDeAplicacion;
using IURIS.COMMON.Entidades.UsuariosDeAplicacion.ComponentesDeUsuario.DatosCriticos;
using IURIS.COMMON.Interfaces;
using IURIS.DAL;
using IURIS.MOVIL.Modelos_y_clases.DB_Local;
using IURIS.MOVIL.Views.ViewPayPal;
using PayPal.Forms;
using PayPal.Forms.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IURIS.MOVIL.Views.ViewsVentanasEmergentes
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WindowOpcionDeCompra : ContentPage
    {
        //IManejadorDeUsuarioAplicacion manejadorDeUsuarioAplicacion;

        enum TipoDeCompra
        {
            _39Mensuales,
            _49Mensuales,
            _479Anuales,
            _ComprarEspacio1Ley
        }
        TipoDeCompra MiCompra;

        //Usuarios _User;
        public WindowOpcionDeCompra()
        {
            InitializeComponent();
            //MainThread.BeginInvokeOnMainThread(async () =>
            //{
            //    await Task.Delay(1000);
            //    manejadorDeUsuarioAplicacion = new ManejadorDeUsuarioAplicacion(new RepositorioGenerico<Usuarios>(true));
            //    _User = manejadorDeUsuarioAplicacion.EncontrarUsuarioID(App.MyUser.IdApp);
            //});
            //UserDialogs.Instance.ShowLoading("Validando datos\nEspere...", MaskType.Gradient);
            //while (_User != null) ;

            UserDialogs.Instance.HideLoading();
        }

        private void _Btn39Mensuales_Clicked(object sender, EventArgs e)
        {
            MiCompra = TipoDeCompra._39Mensuales;
            SendPagePayPal(39);
        }


        private void _Btn49Mensuales_Clicked(object sender, EventArgs e)
        {
            MiCompra = TipoDeCompra._49Mensuales;
            SendPagePayPal(49);
        }

        private void _Btn479Anuales_Clicked(object sender, EventArgs e)
        {
            MiCompra = TipoDeCompra._479Anuales;
            SendPagePayPal(479);
        }

        private void _Btn19PorLey_Clicked(object sender, EventArgs e)
        {
            MiCompra = TipoDeCompra._ComprarEspacio1Ley;
            SendPagePayPal(19);
        }

        private async void SendPagePayPal(int Monto)
        {



            //await Navigation.PopAsync();
            //await Navigation.PushAsync(new PayPalPage(Monto), false);
            try
            {
                var result = await CrossPayPalManager.Current.Buy(new PayPalItem("Compra en IURIS", new Decimal(Monto), "MXN"),
                    new Decimal(0));
                if (result.Status == PayPalStatus.Cancelled)
                {
                    Debug.WriteLine("Cancelled");
                }
                else if (result.Status == PayPalStatus.Error)
                {
                    Debug.WriteLine(result.ErrorMessage);
                }
                else if (result.Status == PayPalStatus.Successful)
                {

                    //App.MyUser.DatosSobreUsuario.FechaDeCompra = DateTime.UtcNow;
                    //if (App.MyUser.DatosSobreUsuario.TipoDeCompra == null) App.MyUser.DatosSobreUsuario.TipoDeCompra = new List<string>();


                    if (MiCompra == TipoDeCompra._ComprarEspacio1Ley)
                    {

                        App.MyUser.DatosSobreUsuario.NumLeyesPermitidas = App.MyUser.DatosSobreUsuario.NumLeyesPermitidas == default ? 5 : App.MyUser.DatosSobreUsuario.NumLeyesPermitidas + 1;

                    }
                    else
                    {

                        if (MiCompra == TipoDeCompra._39Mensuales)
                        {
                            //if (!App.MyUser.DatosSobreUsuario.TipoDeCompra.Contains(MiCompra.ToString())) App.MyUser.DatosSobreUsuario.NumLeyesPermitidas += 1;
                            //App.MyUser.DatosSobreUsuario.NumDeMeses = 1;
                        }
                        if (MiCompra == TipoDeCompra._49Mensuales)
                        {
                            //App.MyUser.DatosSobreUsuario.TipoDeCompra = new List<string>();
                            //App.MyUser.DatosSobreUsuario.NumLeyesPermitidas =default;
                            //App.MyUser.DatosSobreUsuario.NumDeMeses = 1;

                        }
                        if (MiCompra == TipoDeCompra._479Anuales)
                        {
                            //App.MyUser.DatosSobreUsuario.TipoDeCompra = new List<string>();
                            //App.MyUser.DatosSobreUsuario.NumLeyesPermitidas=default;
                            //App.MyUser.DatosSobreUsuario.NumDeMeses = 12;

                        }

                        
                    }
                   
                    //if (App.MyUser.DatosSobreUsuario.TipoDeCompra.Where(e => e.ToString() == MiCompra.ToString()).Count() == 0) App.MyUser.DatosSobreUsuario.TipoDeCompra.Add(MiCompra.ToString());

                    LocalSaveUser saveUser = new LocalSaveUser();
                    await saveUser.UpdateUser();

                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}