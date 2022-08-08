using IURIS.COMMON.Entidades.CapaBase;
using IURIS.COMMON.Entidades.Ley;
using IURIS.COMMON.Entidades.UsuariosDeAplicacion;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IURIS.COMMON.Interfaces
{
    public interface IRepositorio<T> where T:BaseDTO
    {
        bool Create(T Entidad);
        List<T> Read { get; }
        bool Update(T EntidadModificada);
        //Cuando se conecte a mongo cambiar el tipo de identidicador para ObjectId
        bool Delete(ObjectId id);
        Leyes Consult(string key);
        Task<List<Leyes>> Consults(string key);
        Task<List<Leyes>> ConsultsDesktop(string key);
        Task<Usuarios> ConsultaUser(string IdApp, string Nombre, string A_Paterno, string A_Materno);
    }
}
