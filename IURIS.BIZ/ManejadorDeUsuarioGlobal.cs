using IURIS.COMMON.Entidades.UsuarioGlobal;
using IURIS.COMMON.Interfaces;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IURIS.BIZ
{
    public class ManejadorDeUsuarioGlobal : IManejadorDeUsuarioGlobal
    {
        IRepositorio<UsuarioGlobal> repositorio;
        public ManejadorDeUsuarioGlobal(IRepositorio<UsuarioGlobal> repositorio)
        {
            this.repositorio = repositorio;
        }
        public List<UsuarioGlobal> Listar => repositorio.Read;

        public bool AGREGAR(UsuarioGlobal entidad)
        {
            return repositorio.Create(entidad);
        }

        public UsuarioGlobal BuscarPorID(ObjectId Id)
        {
            return Listar.Where(e => e.id == Id).SingleOrDefault();
        }

        public bool Eliminar(ObjectId id)
        {
            return repositorio.Delete(id);
        }

        public bool Modificar(UsuarioGlobal entidad)
        {
            return repositorio.Update(entidad);
        }
    }
}
