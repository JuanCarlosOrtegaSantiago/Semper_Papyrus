using IURIS.COMMON.Constantes;
using IURIS.COMMON.Entidades.Ley;
using IURIS.COMMON.Interfaces;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IURIS.BIZ
{
    public class ManejadorDeLeyPrincipal : IManejadorDeLeyPrincipal
    {
        IRepositorio<LeyPrincipal> _repositorio;
        Const _const;
        public ManejadorDeLeyPrincipal(IRepositorio<LeyPrincipal> repositorio)
        {
            this._repositorio = repositorio;
        }

        public List<LeyPrincipal> Listar => _repositorio.Read;

        public bool AGREGAR(LeyPrincipal entidad)
        {
            return _repositorio.Create(entidad);
        }

        public LeyPrincipal BuscarPorCodigoYClasificacion(string codigo, string nombreDeClasificacion)
        {
            return Listar.Where(w => w.CodigoLey.ToUpper() == _const.MiLeyPrincipal.ToUpper() && w.Clasificacion == "LeyInicial").SingleOrDefault();
        }

        public LeyPrincipal BuscarPorID(ObjectId Id)
        {
            return Listar.Where(e => e.id == Id).SingleOrDefault();
        }

        public bool Eliminar(ObjectId id)
        {
            return _repositorio.Delete(id);
        }

        public bool Modificar(LeyPrincipal entidad)
        {
            return _repositorio.Update(entidad);
        }
    }
}
