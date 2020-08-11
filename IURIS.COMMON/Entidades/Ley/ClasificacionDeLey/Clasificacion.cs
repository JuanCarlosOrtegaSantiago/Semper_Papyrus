using IURIS.COMMON.Entidades.CapaBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace IURIS.COMMON.Entidades.Ley.ClasificacionDeLey
{
    public class Clasificacion:BaseDTO
    {
        public string Nombre { get; set; }

        public override string ToString()
        {
            return string.Format("{0}", Nombre);
        }
    }
}
