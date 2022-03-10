using IURIS.COMMON.Entidades.Ley;
using IURIS.COMMON.Entidades.Ley.ClasificacionDeLey;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IURIS.COMMON.Interfaces
{
    public interface IManejadorDeLeyes:IManejadorGenerico<Leyes>
    {
        Leyes BuscarPorCodigo(string codigo);
        //Leyes BuscarLey(string NombreDeLEy);
        List<Leyes> BuscarEnLeyes(string BuscarLey);
        List<Leyes> BuscarEnLeyesPorClasificacion(Clasificacion clasificacion);
        List<Leyes> MostrarLeyes { get; }
        Leyes Consult(string key);
        Task<List<Leyes>> Consults(string key);
        Task<List<Leyes>> ConsultsDesktop(string key);

    }
}
