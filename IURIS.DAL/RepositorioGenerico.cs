using IURIS.COMMON.Entidades.CapaBase;
using IURIS.COMMON.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace IURIS.DAL
{
    public class RepositorioGenerico<T> : IRepositorio<T> where T : BaseDTO
    {
        public List<T> Read => throw new NotImplementedException();

        public bool Create(T Entidad)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(T EntidadModificada)
        {
            throw new NotImplementedException();
        }
    }
}
