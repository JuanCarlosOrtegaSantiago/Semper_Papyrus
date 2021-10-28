using System;
using System.Collections.Generic;
using System.Text;

namespace IURIS.COMMON.Entidades.UsuariosDeAplicacion.ComponentesDeUsuario.DatosCriticos
{
    public class DatosSobreUsuarioParaLey
    {
        public int NumLeyesPermitidas { get; set; }
        public DateTime FechaDeCompra { get; set; }
        public int NumDeMeses { get; set; }
        public List<string> TipoDeCompra { get; set; }
    }
}
