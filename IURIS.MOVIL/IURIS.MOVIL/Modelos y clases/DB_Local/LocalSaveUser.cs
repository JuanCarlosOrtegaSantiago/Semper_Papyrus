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

        public void Save()
        {
            MyUser myUser=CrearUsuario();
            
            App.Database.SavePersonAsync(myUser);
            App.MyUser = myUser;
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
                IdApp = _User.IdApp
            };

            foreach (var Ley in _User.MisLeyes)
            {
                MyLey myLey = new MyLey()
                {
                    Clasificacion =new MyClasificacion() { Nombre= Ley.Clasificacion.Nombre },
                     CodigoLey=Ley.CodigoLey,
                      EsModificacion=Ley.EsModificacion,
                       FechaDeDescarga=Ley.FechaDeDescarga,
                        ListaDeTitulos=Ley.ListaDeTitulos,
                         NombreLey=Ley.NombreLey,
                          UltimaFechaDeModificacion=Ley.UltimaFechaDeModificacion,
                    Clasificaciones = Ley.Clasificaciones


                };
                myUser.MisLeyes.Add(myLey);

            }
           
            return myUser;
        }

    }
}
