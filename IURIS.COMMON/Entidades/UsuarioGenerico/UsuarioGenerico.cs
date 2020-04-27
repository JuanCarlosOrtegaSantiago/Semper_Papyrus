using IURIS.COMMON.Entidades.BaseUser;
using IURIS.COMMON.Entidades.UsuarioIntermedio;
using System;
using System.Collections.Generic;
using System.Text;

namespace IURIS.COMMON.Entidades.UsuarioGenerico
{
    public class UsuarioGenerico:BaseUsuarios
    {
        public string Direccion { get; set; }
        public string NombreCompleto { get; set; }
        public override string ToString()
        {
            return string.Format("- {0} -", NombreCompleto);
        }

    }
}
