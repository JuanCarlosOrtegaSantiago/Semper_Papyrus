using IURIS.COMMON.Entidades.ContraseniaDeAccesoUnico;
using IURIS.COMMON.Interfaces;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
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
        public List<ContraseniaUnica> Read => repositorio.Read;

        public bool Create(ContraseniaUnica Entidad)
        {
            return repositorio.Create(Entidad);
        }

        public bool Delete(ObjectId id)
        {
            return repositorio.Delete(id);
        }

        public bool Update(ContraseniaUnica EntidadModificada)
        {
            return repositorio.Update(EntidadModificada);
        }
    }
}
