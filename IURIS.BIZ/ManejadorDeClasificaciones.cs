using IURIS.COMMON.Entidades.Ley.ClasificacionDeLey;
using IURIS.COMMON.Interfaces;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IURIS.BIZ
{
    public class ManejadorDeClasificaciones : IManejadorDeClasificaciones
    {
        IRepositorio<Clasificacion> repositorio;
        public ManejadorDeClasificaciones(IRepositorio<Clasificacion> repositorio)
        {
            this.repositorio = repositorio;
        }
        public List<Clasificacion> Listar => repositorio.Read;

        public bool AGREGAR(Clasificacion entidad)
        {
            return repositorio.Create(entidad);
        }

        public Clasificacion BuscarPorID(ObjectId Id)
        {
            return Listar.Where(e => e.id == Id).SingleOrDefault();
        }

        public bool Eliminar(ObjectId id)
        {
            return repositorio.Delete(id);
        }

        public bool Modificar(Clasificacion entidad)
        {
            return repositorio.Update(entidad);
        }
    }
}
