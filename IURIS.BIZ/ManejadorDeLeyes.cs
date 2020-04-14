using IURIS.COMMON.Entidades.Ley;
using IURIS.COMMON.Interfaces;
using IURIS.DAL;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IURIS.BIZ
{
    public class ManejadorDeLeyes : IManejadorDeLeyes
    {
        IRepositorio<Leyes> repositorio;
        public ManejadorDeLeyes(IRepositorio<Leyes> repositorio)
        {
            this.repositorio = repositorio;
        }
        public List<Leyes> Listar => repositorio.Read;

        public bool AGREGAR(Leyes entidad)
        {
            return repositorio.Create(entidad);
        }

        public bool BuscarPorCodigo(string codigo)
        {
            return Listar.Where(e => e.CodigoLey == codigo).Count() >= 1 ? true : false;
        }

        public Leyes BuscarPorID(ObjectId Id)
        {
            return Listar.Where(e => e.id == Id).SingleOrDefault();
        }

        public bool Eliminar(ObjectId id)
        {
            return repositorio.Delete(id);
        }

        public bool Modificar(Leyes entidad)
        {
            return repositorio.Update(entidad);
        }
    }
}
