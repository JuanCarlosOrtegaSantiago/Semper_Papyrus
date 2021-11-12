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

        public HerramientasGenerales(Usuarios usuarios)
        {
            _User = usuarios;
            Const = new Const();
        }

        public void RenovarSuscripcion()
        {
            var num = GetMonthDifference(_User.DatosSobreUsuario.FechaDeCompra);

            if (_User.DatosSobreUsuario.NumDeMeses == num)
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
            var x = _User.DatosSobreUsuario.TipoDeCompra.Find(e => e == Const._39Mensuales || e == Const._ComprarEspacio1Ley || e == Const._49Mensuales);

            Settings.TypeDB = _User.DatosSobreUsuario.TipoDeCompra == null || x != null ? Const.TypeDBLocal : Const.TypeDBNube;
        }
    }
}
