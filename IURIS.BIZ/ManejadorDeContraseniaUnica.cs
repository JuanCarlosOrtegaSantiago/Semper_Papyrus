using IURIS.COMMON.Entidades.ContraseniaDeAccesoUnico;
using IURIS.COMMON.Interfaces;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IURIS.BIZ
{
    public class ManejadorDeContraseniaUnica : IManejadorDeContraseniaUnica
    {
        IRepositorio<ContraseniaUnica> repositorio;
        public ManejadorDeContraseniaUnica(IRepositorio<ContraseniaUnica> repositorio)
        {
            this.repositorio = repositorio;
        }
        public List<ContraseniaUnica> Listar => repositorio.Read;

        public bool AGREGAR(ContraseniaUnica Entidad)
        {
            return repositorio.Create(Entidad);
        }

        public ContraseniaUnica BuscarPorID(ObjectId Id)
        {
            return Listar.Where(e => e.id == Id).SingleOrDefault();
        }

        public bool Eliminar(ObjectId id)
        {
            return repositorio.Delete(id);
        }

        public bool Modificar(ContraseniaUnica EntidadModificada)
        {
            return repositorio.Update(EntidadModificada);
        }
    }
}
