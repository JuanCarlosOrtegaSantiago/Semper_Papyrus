using IURIS.COMMON.Constantes;
using IURIS.COMMON.Entidades.UsuariosDeAplicacion;
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

        public HerramientasGenerales(Usuarios usuarios)
        {
            _User = usuarios;
        }

        public void RenovarSuscripcion()
        {
            var num = GetMonthDifference(_User.DatosSobreUsuario.FechaDeCompra);

            if (_User.DatosSobreUsuario.NumDeMeses == num)
            {
                App.masterDetail.IsPresented = false;
                App.masterDetail.Detail = new NavigationPage(new WindowOpcionDeCompra(_User));
            }
        }

        private int GetMonthDifference(DateTime startDate)
        {
            int monthsApart = 12 * (startDate.Year - DateTime.UtcNow.Year) + startDate.Month - DateTime.UtcNow.Month;
            return Math.Abs(monthsApart);
        }
    }
}
