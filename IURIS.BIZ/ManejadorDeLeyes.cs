using IURIS.COMMON.Entidades.Ley;
using IURIS.COMMON.Entidades.Ley.ClasificacionDeLey;
using IURIS.COMMON.Interfaces;
using IURIS.DAL;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public List<Leyes> MostrarLeyes => repositorio.Read.OrderByDescending(e => e.UltimaFechaDeModificacion).ToList();

        public bool AGREGAR(Leyes entidad)
        {
            return repositorio.Create(entidad);
        }

        public List<Leyes> BuscarEnLeyes(string BuscarLey)
        {
            return repositorio.Read.Where(e => e.NombreLey.ToUpper().Contains(BuscarLey.ToUpper()) == true || e.CodigoLey.ToUpper().Contains(BuscarLey.ToUpper()) == true).OrderByDescending(e => e.UltimaFechaDeModificacion).ToList();
        }

        public List<Leyes> BuscarEnLeyesPorClasificacion(Clasificacion clasificacion)
        {
            return repositorio.Read.Where(w => w.Clasificacion == clasificacion.Nombre).ToList();
        }

        public Leyes BuscarPorCodigo(string codigo)
        {
            return Listar.Where(e => e.CodigoLey.ToUpperInvariant().Equals(codigo)).SingleOrDefault();
        }

        public Leyes BuscarPorID(ObjectId Id)
        {
            return Listar.Where(e => e.id == Id).SingleOrDefault();
        }

        public Leyes Consult(string key)
        {
            return repositorio.Consult(key);
        }

        public Task<List<Leyes>> Consults(string key)
        {
            return repositorio.Consults(key);
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
