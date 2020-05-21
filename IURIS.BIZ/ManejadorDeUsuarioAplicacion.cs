using IURIS.COMMON.Entidades.UsuariosDeAplicacion;
using IURIS.COMMON.Interfaces;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IURIS.BIZ
{
    public class ManejadorDeUsuarioAplicacion : IManejadorDeUsuarioAplicacion
    {
        IRepositorio<Usuarios> repositorio;
        public ManejadorDeUsuarioAplicacion(IRepositorio<Usuarios> repositorio)
        {
            this.repositorio = repositorio;
        }
        public List<Usuarios> Listar => repositorio.Read;

        public bool AGREGAR(Usuarios entidad)
        {
            return repositorio.Create(entidad);
        }

        public Usuarios BuscarPorID(ObjectId Id)
        {
            return Listar.Where(e => e.id == Id).SingleOrDefault();
        }

        public bool Eliminar(ObjectId id)
        {
            return repositorio.Delete(id);
        }

        public bool EncontrarUsuario(string Correo, int contrasenia)
        {
            return Listar.Where(e => e.Correo == Correo && e.Contrasenia == contrasenia).Count() == 1? true:false ;
        }

        public bool ExisteCorreo(string Correo)
        {
            return Listar.Where(e => e.Correo == Correo).Count() >= 1 ? true : false;
        }

        //public bool ExisteNombre(string Nombre, string ApellidoPaterno, string apellidoMaterno)
        //{
        //    return Listar.Where(e => e.Nombre.ToUpper() == Nombre.ToUpper() && e.ApellidoPaterno.ToUpper() == ApellidoPaterno.ToUpper() && e.ApellidoMaterno.ToUpper() == apellidoMaterno.ToUpper()).Count() >= 1 ? true : false;
        //}

        public bool Modificar(Usuarios entidad)
        {
            return repositorio.Update(entidad);
        }
    }
}
