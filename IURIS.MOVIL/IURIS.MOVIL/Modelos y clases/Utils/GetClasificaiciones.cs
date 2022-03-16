using IURIS.BIZ;
using IURIS.COMMON.Constantes;
using IURIS.COMMON.Entidades.Ley.ClasificacionDeLey;
using IURIS.COMMON.Entidades.UsuariosDeAplicacion;
using IURIS.COMMON.Interfaces;
using IURIS.DAL;
using IURIS.MOVIL.Modelos_y_clases.DB_Local;
using IURIS.MOVIL.Modelos_y_clases.DB_Local.COMMON;
using Matcha.BackgroundService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace IURIS.MOVIL.Modelos_y_clases.Utils
{
    public class GetClasificaiciones : IPeriodicTask
    {
        IManejadorDeClasificaciones manejadorDeClasificaciones;
        public TimeSpan Interval { get; set; }
        List<Clasificacion> _Clasificacions;

        public GetClasificaiciones(int min)
        {
            Interval = TimeSpan.FromHours(min);

        }


        public async Task<bool> StartJob()
        {
            try
            {

                // YOUR CODE HERE
                // THIS CODE WILL BE EXECUTE EVERY INTERVAL
                //manejadorDeUsuarioAplicacion = new ManejadorDeUsuarioAplicacion(new RepositorioGenerico<Usuarios>());

                //var _user = manejadorDeUsuarioAplicacion.EncontrarUsuario(App.MyUser.Correo, App.MyUser.Contrasenia);

                Const _Const = new Const();

                manejadorDeClasificaciones = new ManejadorDeClasificaciones(new RepositorioGenerico<Clasificacion>());
                _Clasificacions = manejadorDeClasificaciones.Listar;

                var milist = App.MyClasificacionDeLey.GetPeopleAsync();


                foreach (var item in _Clasificacions)
                {
                    if (!item.Nombre.Equals(_Const.NombreDeClasificaiconExcluir) && !milist.Result.Exists(x => x.Nombre.Equals(item.Nombre))) SaveMyClasificacion(item);
                    //if ( !milist.Result.Exists(x => x.Nombre.Equals(item.Nombre))) SaveMyClasificacion(item);
                }

                return true; //return false when you want to stop or trigger only once
            }
            catch (Exception)
            {

                return false;
            }
        }

        private void SaveMyClasificacion(Clasificacion item)
        {
            App.MyClasificacionDeLey.SavePersonAsync(ConvertirAMyClasificacionDeLEy(item));
        }

        private MyClasificacionDeLey ConvertirAMyClasificacionDeLEy(Clasificacion item)
        {
            MyClasificacionDeLey myClasificacion = new MyClasificacionDeLey()
            {
                Nombre = item.Nombre
            };

            return myClasificacion;
        }
    }
}