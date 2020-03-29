using IURIS.COMMON.Entidades.CapaBase;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace IURIS.COMMON.Interfaces
{
    public interface IManejadorGenerico<T> where T:BaseDTO
    {
        bool AGREGAR(T entidad);
        List<T> Listar { get; }
        //Al crear la ase de datos en mongo db cambiar el identificador para ObjectId
        bool Eliminar(ObjectId id);
        bool Modificar(T entidad);
        //Al crear la ase de datos en mongo db cambiar el identificador para ObjectId
        T BuscarPorID(ObjectId Id);

    }
}
