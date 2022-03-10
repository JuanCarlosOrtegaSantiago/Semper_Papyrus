using Acr.UserDialogs;
using IURIS.BIZ;
using IURIS.COMMON.Constantes;
using IURIS.COMMON.Entidades.Ley;
using IURIS.COMMON.Entidades.Ley.ComponentesDeLey;
using IURIS.COMMON.Entidades.UsuariosDeAplicacion;
using IURIS.COMMON.Interfaces;
using IURIS.DAL;
using IURIS.MOVIL.Modelos_y_clases.DB_Local;
using IURIS.MOVIL.Utils;
using IURIS.MOVIL.Views.ViewsVentanasEmergentes;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace IURIS.MOVIL.Modelos_y_clases.Tools
{
    public class HerramientasGenerales
    {
        Usuarios _User;
        Const Const;
        IManejadorDeLeyes manejadorDeLeyes;

        public HerramientasGenerales()
        {
            Const = new Const();
            manejadorDeLeyes = new ManejadorDeLeyes(new RepositorioGenerico<Leyes>());
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


        public async Task<string> AgragarLEyAsync(string Codigo)
        {
            bool ExisteLey = false;
            Leyes _LeyComprada = null;
            string _1LeyMas = null;

            if (App.MyUser.DatosSobreUsuario.TipoDeCompra != null) _1LeyMas = App.MyUser.DatosSobreUsuario.TipoDeCompra.Find(w => w == Const._ComprarEspacio1Ley || w == Const._39Mensuales);

            if (_1LeyMas != null && App.MyUser.MisLeyes.Count >= App.MyUser.DatosSobreUsuario.NumLeyesPermitidas)
            {
                UserDialogs.Instance.HideLoading();
                await PopupNavigation.Instance.PushAsync(new WindowOfComprarEspacio());
                return "Error|Necesitas mas espacio|OK";
            }


            if (string.IsNullOrEmpty(Codigo)) return "Advertencia|Introduce un codigo valido|OK";

            _LeyComprada = manejadorDeLeyes.Consult(Codigo);

            if (_LeyComprada == null)
            {
                UserDialogs.Instance.HideLoading();
                return  "Error|Codigo incorrecto\nIntenta de nuevo|OK";
                
            }

            
            if (App.MyUser.MisLeyes.Exists(r => r.CodigoLey == _LeyComprada.CodigoLey))
            {
                UserDialogs.Instance.HideLoading();
                return "Advertencia|El codigo ingresado\nCorresponde a una ley que ya \nse encuentra en tu lista|OK";
            }

            UserDialogs.Instance.HideLoading();
            UserDialogs.Instance.ShowLoading("Agregando ley a tu lista", MaskType.Gradient);
            await Task.Delay(1000);
            _LeyComprada.FechaDeDescarga = DateTime.UtcNow.ToLocalTime();

            LocalSaveUser localSaveUser = new LocalSaveUser();

            App.MyUser.MisLeyes.Add(localSaveUser.LeyToMyley(_LeyComprada));
            App.MyUser.MisLeyes.Where(w => w.CodigoLey == _LeyComprada.CodigoLey).SingleOrDefault().Clasificaciones = new List<ClasificacionPUsuario>();

            if (await App.Database.UpdateUserAsync(App.MyUser))
            {
                //_LeyComprada.numDescargas += 1;
                //manejadorDeLeyes.Modificar(_LeyComprada);
                UserDialogs.Instance.HideLoading();
                return "OK|OK|OK";
            }
            else
            {
                UserDialogs.Instance.HideLoading();
                return "Error|Ocurrio un error\nIntente mas tarde|OK";
            }
        }
    }
}
