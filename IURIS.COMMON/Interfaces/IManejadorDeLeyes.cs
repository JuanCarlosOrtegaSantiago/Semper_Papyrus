using IURIS.COMMON.Entidades.Ley;
using IURIS.COMMON.Entidades.Ley.ClasificacionDeLey;
using System;
using System.Collections.Generic;
using System.Text;

namespace IURIS.COMMON.Interfaces
{
    public interface IManejadorDeLeyes:IManejadorGenerico<Leyes>
    {
        bool BuscarPorCodigo(string codigo);
        Leyes BuscarLey(string NombreDeLEy);
        List<Leyes> BuscarEnLeyes(string BuscarLey);
        List<Leyes> BuscarEnLeyesPorClasificacion(Clasificacion clasificacion);
        List<Leyes> MostrarLeyes { get; }


    }
}
