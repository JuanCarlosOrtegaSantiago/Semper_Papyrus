using IURIS.BIZ;
using IURIS.COMMON.Entidades.UsuariosDeAplicacion;
using IURIS.COMMON.Entidades.UsuariosDeAplicacion.ComponentesDeUsuario.DatosCriticos;
using IURIS.COMMON.Interfaces;
using IURIS.DAL;
using IURIS.MOVIL.Views.ViewPayPal;
using PayPal.Forms;
using PayPal.Forms.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IURIS.MOVIL.Views.ViewsVentanasEmergentes
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WindowOpcionDeCompra : ContentPage
    {
        IManejadorDeUsuarioAplicacion manejadorDeUsuarioAplicacion;

        enum TipoDeCompra
        {
            _39Mensuales,
            _49Mensuales,
            _479Anuales,
            _ComprarEspacio1Ley
        }
        TipoDeCompra MiCompra;

        Usuarios _User;
        public WindowOpcionDeCompra(Usuarios user)
        {
            InitializeComponent();

            manejadorDeUsuarioAplicacion = new ManejadorDeUsuarioAplicacion(new RepositorioGenerico<Usuarios>());

            _User = user;
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
                    if (_User.DatosSobreUsuario == null)
                        _User.DatosSobreUsuario = new DatosSobreUsuarioParaLey();

                    _User.DatosSobreUsuario.FechaDeCompra = DateTime.UtcNow;
                    if(_User.DatosSobreUsuario.TipoDeCompra.Where(e=> e.ToString()== MiCompra.ToString()).Count() == 0)
                    {
                    _User.DatosSobreUsuario.TipoDeCompra.Add(MiCompra.ToString());

                    }


                    if (MiCompra == TipoDeCompra._ComprarEspacio1Ley)
                    {
                        if (_User.DatosSobreUsuario.NumLeyesPermitidas == default)
                        {
                            if (_User.DatosSobreUsuario.NumLeyesPermitidas < 3) 
                                _User.DatosSobreUsuario.NumLeyesPermitidas = 5;
                        }
                        else
                        {
                            _User.DatosSobreUsuario.NumLeyesPermitidas += 1;
                        }
                    }
                    else
                    {

                        if (MiCompra == TipoDeCompra._39Mensuales)
                        {
                            _User.DatosSobreUsuario.NumLeyesPermitidas += 1;
                            _User.DatosSobreUsuario.NumDeMeses = 1;
                        }
                        if (MiCompra == TipoDeCompra._49Mensuales)
                        {
                            _User.DatosSobreUsuario.NumLeyesPermitidas =default;
                            _User.DatosSobreUsuario.NumDeMeses = 1;

                        }
                        if (MiCompra == TipoDeCompra._479Anuales)
                        {
                            _User.DatosSobreUsuario.NumLeyesPermitidas=default;
                            _User.DatosSobreUsuario.NumDeMeses = 12;

                        }

                        
                    }
                    while (!manejadorDeUsuarioAplicacion.Modificar(_User));

                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}