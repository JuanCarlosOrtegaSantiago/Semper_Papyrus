using IURIS.BIZ;
using IURIS.COMMON.Constantes;
using IURIS.COMMON.Entidades.Ley;
using IURIS.COMMON.Entidades.Ley.ComponentesDeLey;
using IURIS.COMMON.Entidades.UsuariosDeAplicacion;
using IURIS.COMMON.Entidades.UsuariosDeAplicacion.ComponentesDeUsuario;
using IURIS.COMMON.Entidades.UsuariosDeAplicacion.ComponentesDeUsuario.DatosCriticos;
using IURIS.COMMON.Interfaces;
using IURIS.DAL;
using IURIS.MOVIL.Modelos_y_clases.DB_Local.COMMON;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IURIS.MOVIL.Modelos_y_clases.DB_Local
{
    public class LocalSaveUser
    {
        string _Nombre;
        string _APaterno;
        string _AMaterno;

        IManejadorDeLeyPrincipal manejadorDeLeyPrincipal;
        
        Const _Const = new Const();
        public LocalSaveUser()
        {
            
        }

        public bool SaveUser(string Nombre, string APaterno, string AMaterno)
        {
            _Nombre = Nombre;
            _APaterno = APaterno;
            _AMaterno = AMaterno;
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

        public async Task<bool> UpdateUser()
        {
            try
            {
                await App.Database.UpdateUserAsync(App.MyUser);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        private MyUser CrearUsuario()
        {
            manejadorDeLeyPrincipal = new ManejadorDeLeyPrincipal(new RepositorioGenerico<LeyPrincipal>());
            Leyes ley = (Leyes)manejadorDeLeyPrincipal.Listar.Where(w => w.CodigoLey.ToUpper() == _Const.MiLeyPrincipal.ToUpper() && w.Clasificacion== "LeyInicial").FirstOrDefault();
            ley.Clasificaciones = new List<ClasificacionPUsuario>();

            MyUser myUser = new MyUser
            {
                Apuntes = new List<Apunte>(),
                MisLeyes = new List<MyLey>(),
                ApellidoMaterno = _AMaterno,
                ApellidoPaterno = _APaterno,
                Nombre = _Nombre,
                 DatosSobreUsuario=new MyDatosSobreUsuarioParaLey(), 

            };
            myUser.DatosSobreUsuario.NumLeyesPermitidas = _Const.NumDeLeyesInicial;
            myUser.MisLeyes.Add(LeyToMyley(ley));
            myUser.MiUltimaLeyCargada = ley.CodigoLey;
            //foreach (var Ley in _User.MisLeyes)
            //{
            //    myUser.MisLeyes.Add(LeyToMyley(Ley));
            //}

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
