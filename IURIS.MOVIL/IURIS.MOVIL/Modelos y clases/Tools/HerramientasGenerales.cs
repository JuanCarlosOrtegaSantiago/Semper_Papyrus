using IURIS.COMMON.Constantes;
using IURIS.COMMON.Entidades.UsuariosDeAplicacion;
using IURIS.MOVIL.Utils;
using IURIS.MOVIL.Views.ViewsVentanasEmergentes;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace IURIS.MOVIL.Modelos_y_clases.Tools
{
    public class HerramientasGenerales
    {
        Usuarios _User;
        Const Const;

        public HerramientasGenerales()
        {
            Const = new Const();
        }

        public HerramientasGenerales(Usuarios usuarios)
        {
            _User = usuarios;
            Const = new Const();
        }

        public void RenovarSuscripcion()
        {
            var num = GetMonthDifference(App.MyUser.DatosSobreUsuario.FechaDeCompra);

            if (App.MyUser.DatosSobreUsuario.NumDeMeses == num)
            {
                App.masterDetail.IsPresented = false;
                App.masterDetail.Detail = new NavigationPage(new WindowOpcionDeCompra());
            }
        }

        private int GetMonthDifference(DateTime startDate)
        {
            int monthsApart = 12 * (startDate.Year - DateTime.UtcNow.Year) + startDate.Month - DateTime.UtcNow.Month;
            return Math.Abs(monthsApart);
        }
    
        public void TipoDeAlmacenamiento()
        {
            var x = App.MyUser.DatosSobreUsuario.TipoDeCompra.Find(e => e == Const._39Mensuales || e == Const._ComprarEspacio1Ley || e == Const._49Mensuales);

            Settings.TypeDB = App.MyUser.DatosSobreUsuario.TipoDeCompra == null || x != null ? Const.TypeDBLocal : Const.TypeDBNube;
        }
    }
}
