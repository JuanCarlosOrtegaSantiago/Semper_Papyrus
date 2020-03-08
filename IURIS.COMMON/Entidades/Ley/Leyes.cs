using IURIS.COMMON.Entidades.CapaBase;
using IURIS.COMMON.Entidades.Ley.ComponentesDeLey;
using System;
using System.Collections.Generic;
using System.Text;

namespace IURIS.COMMON.Entidades.Ley
{
    public class Leyes:BaseDTO
    {
        public string NombreLey { get; set; }
        public int CodigoLey { get; set; }
        public bool EsModificacion { get; set; }
        public DateTime UltimaFechaDeModificacion { get; set; }
        public List<Titulo> ListaDeTitulos { get; set; }
    }
}
