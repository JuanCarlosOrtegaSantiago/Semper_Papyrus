using IURIS.COMMON.Entidades.Ley.ComponentesDeLey;
using System;
using System.Collections.Generic;
using System.Text;

namespace IURIS.COMMON.Entidades.UsuariosDeAplicacion.ComponentesDeUsuario
{
    public class Clasificacion
    {
        public string Nombre { get; set; }
        public List<Articulo> MisArticulos{ get; set; }
    }
}
