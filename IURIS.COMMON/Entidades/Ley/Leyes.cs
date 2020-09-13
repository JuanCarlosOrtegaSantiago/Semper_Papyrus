using IURIS.COMMON.Entidades.CapaBase;
using IURIS.COMMON.Entidades.Ley.ClasificacionDeLey;
using IURIS.COMMON.Entidades.Ley.ComponentesDeLey;
using System;
using System.Collections.Generic;
using System.Text;

namespace IURIS.COMMON.Entidades.Ley
{
    public class Leyes:BaseDTO
    {
        public string NombreLey { get; set; }
        public string CodigoLey { get; set; }
        public string CodigoDeVenta { get; set; }
        public bool EsModificacion { get; set; }
        public DateTime UltimaFechaDeModificacion { get; set; }
        public List<Titulo> ListaDeTitulos { get; set; }
        public int numDescargas { get; set; }
        public Clasificacion Clasificacion { get; set; }
    }
}
