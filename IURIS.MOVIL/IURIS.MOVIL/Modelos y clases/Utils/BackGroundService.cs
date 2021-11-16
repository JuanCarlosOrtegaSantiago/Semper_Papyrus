using IURIS.BIZ;
using IURIS.COMMON.Entidades.UsuariosDeAplicacion;
using IURIS.COMMON.Interfaces;
using IURIS.DAL;
using Matcha.BackgroundService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace IURIS.MOVIL.Modelos_y_clases.Utils
{
    public class BackGroundService : IPeriodicTask
    {
        public TimeSpan Interval { get; set; }


        IManejadorDeUsuarioAplicacion manejadorDeUsuarioAplicacion;

        public BackGroundService(int seconds)
        {
            Interval = TimeSpan.FromSeconds(seconds);

        }


        public async Task<bool> StartJob()
        {
            try
            {

                // YOUR CODE HERE
                // THIS CODE WILL BE EXECUTE EVERY INTERVAL
                manejadorDeUsuarioAplicacion = new ManejadorDeUsuarioAplicacion(new RepositorioGenerico<Usuarios>());

                var _user = manejadorDeUsuarioAplicacion.EncontrarUsuario(App.MyUser.Correo, App.MyUser.Contrasenia);

                return true; //return false when you want to stop or trigger only once
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<bool> UserModifiqued()
        {
            try
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await Task.Delay(500);
                    manejadorDeUsuarioAplicacion = new ManejadorDeUsuarioAplicacion(new RepositorioGenerico<Usuarios>());

                    var _user = manejadorDeUsuarioAplicacion.EncontrarUsuario(App.MyUser.Correo, App.MyUser.Contrasenia);
                    _user.ApellidoPaterno = "Ortega";
                    manejadorDeUsuarioAplicacion.Modificar(_user);

                });
                return true; //return false when you want to stop or trigger only once
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<bool> ModificarUsuario()
        {
            try
            {
                manejadorDeUsuarioAplicacion = new ManejadorDeUsuarioAplicacion(new RepositorioGenerico<Usuarios>());

                var _user = manejadorDeUsuarioAplicacion.EncontrarUsuario(App.MyUser.Correo, App.MyUser.Contrasenia);

                _user.ApellidoMaterno = "Santiago";
                manejadorDeUsuarioAplicacion.Modificar(_user);

                return true; //return false when you want to stop or trigger only once
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}