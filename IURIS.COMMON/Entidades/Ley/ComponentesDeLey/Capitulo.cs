using System;
using System.Collections.Generic;
using System.Text;

namespace IURIS.COMMON.Entidades.Ley.ComponentesDeLey
{
    public class Capitulo
    {
        public string NombreCapitulo { get; set; }
        public string NumCapitulo { get; set; }
        public List<Articulo> ListaArticulos { get; set; }
    }
}
