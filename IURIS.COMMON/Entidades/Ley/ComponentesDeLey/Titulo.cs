using System;
using System.Collections.Generic;
using System.Text;

namespace IURIS.COMMON.Entidades.Ley.ComponentesDeLey
{
    public class Titulo
    {
        public string NombreTitulo { get; set; }
        public string NumTitulo { get; set; }
        public List<Capitulo> ListaCapitulos { get; set; }
    }
}
