using IURIS.COMMON.Entidades.UsuarioGenerico;
using IURIS.COMMON.Interfaces;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IURIS.BIZ
{
    public class ManejadorDeUsuarioGenerico : IManejadorDeUsuarioGenerico
    {
        IRepositorio<UsuarioGenerico> repositorio;
        public ManejadorDeUsuarioGenerico(IRepositorio<UsuarioGenerico> repositorio)
        {
            this.repositorio = repositorio;
        }
        public List<UsuarioGenerico> Listar => repositorio.Read;

        public bool AGREGAR(UsuarioGenerico Entidad)
        {
            return repositorio.Create(Entidad);
        }

        public UsuarioGenerico BuscarPorID(ObjectId Id)
        {
            return Listar.Where(e => e.id == Id).SingleOrDefault();
        }

        public bool Eliminar(ObjectId id)
        {
            return repositorio.Delete(id);
        }

        public bool Modificar(UsuarioGenerico entidad)
        {
            return repositorio.Update(entidad);
        }
    }
}
