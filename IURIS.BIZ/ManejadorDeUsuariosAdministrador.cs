using IURIS.COMMON.Entidades.UsuariosAdministrador;
using IURIS.COMMON.Interfaces;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IURIS.BIZ
{
    public class ManejadorDeUsuariosAdministrador : IManejadorDeUsuarioAdministrador
    {
        IRepositorio<UsuarioAdministrador> repositorio;
        public ManejadorDeUsuariosAdministrador(IRepositorio<UsuarioAdministrador> repositorio)
        {
            this.repositorio = repositorio;
        }
        public List<UsuarioAdministrador> Listar => repositorio.Read;

        public bool AGREGAR(UsuarioAdministrador entidad)
        {
            return repositorio.Create(entidad);
        }

        public UsuarioAdministrador BuscarContrasenia(string Contrasenia)
        {
            return Listar.Where(e => e.Contrasenia == Contrasenia).SingleOrDefault();
        }

        public UsuarioAdministrador BuscarCorreo(string Correo)
        {
            return Listar.Where(e => e.Correo == Correo).SingleOrDefault();
        }

        public UsuarioAdministrador BuscarPorID(ObjectId Id)
        {
            return Listar.Where(e => e.id == Id).SingleOrDefault();
        }

        public bool Eliminar(ObjectId id)
        {
            return repositorio.Delete(id);
        }

        public bool Modificar(UsuarioAdministrador entidad)
        {
            return repositorio.Update(entidad);
        }
    }
}
