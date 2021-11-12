using IURIS.COMMON.Entidades.Ley;
using IURIS.COMMON.Entidades.UsuariosDeAplicacion;
using IURIS.MOVIL.Modelos_y_clases.DB_Local.COMMON;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IURIS.MOVIL.Modelos_y_clases.DB_Local
{
    public class LocalSaveUser
    {

        Usuarios _User;
        public LocalSaveUser(Usuarios usuarios)
        {
            _User = usuarios;
        }

        public bool Save()
        {
            try
            {
                MyUser myUser = CrearUsuario();

                App.Database.SavePersonAsync(myUser);
                App.MyUser = myUser;
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> ExisteUsuario()
        {
            var data = await App.Database.GetPeopleAsync();

            return data.Find(e=>e.IdApp==_User.IdApp)!=null;
        }

        private MyUser CrearUsuario()
        {

            MyUser myUser = new MyUser
            {
                Apuntes = _User.Apuntes,
                MiUltimaLeyCargada = _User.MiUltimaLeyCargada,
                IdUser = _User.id.ToString(),
                MisLeyes = new List<MyLey>(),
                IdApp = _User.IdApp,
                Contrasenia = _User.Contrasenia,
                Correo = _User.Correo
            };

            foreach (var Ley in _User.MisLeyes)
            {
                myUser.MisLeyes.Add(LeyToMyley(Ley));
            }
           
            return myUser;
        }

        public MyLey LeyToMyley(Leyes Ley)
        {
            try
            {

                MyLey myLey = new MyLey()
                {
                    Clasificacion = Ley.Clasificacion,
                    CodigoLey = Ley.CodigoLey,
                    EsModificacion = Ley.EsModificacion,
                    FechaDeDescarga = Ley.FechaDeDescarga,
                    ListaDeTitulos = Ley.ListaDeTitulos,
                    NombreLey = Ley.NombreLey,
                    UltimaFechaDeModificacion = Ley.UltimaFechaDeModificacion,
                    Clasificaciones = Ley.Clasificaciones
                };

                return myLey;
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}
