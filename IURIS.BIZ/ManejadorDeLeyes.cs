using IURIS.COMMON.Entidades.Ley;
using IURIS.COMMON.Entidades.Ley.ClasificacionDeLey;
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
            return repositorio.Read.Where(e => e.Clasificacion == clasificacion).ToList();
        }

        public Leyes BuscarLey(string NombreDeLEy)
        {
        
            return Listar.Where(e => e.NombreLey == NombreDeLEy).SingleOrDefault();
            //return Listar.Where(e => e.NombreLey == NombreDeLEy).ToList().OrderBy(e => e.NombreLey).Single();
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
