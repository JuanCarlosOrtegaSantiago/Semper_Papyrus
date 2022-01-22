using IURIS.COMMON.Entidades.Ley;
using System;
using System.Collections.Generic;
using System.Text;

namespace IURIS.COMMON.Interfaces
{
    public interface IManejadorDeLeyPrincipal : IManejadorGenerico<LeyPrincipal>
    {
        LeyPrincipal BuscarPorCodigoYClasificacion(string codigo, string nombreDeClasificacion);
    }
}
