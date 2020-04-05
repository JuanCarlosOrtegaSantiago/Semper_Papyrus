using IURIS.COMMON.Entidades.BaseUser;
using System;
using System.Collections.Generic;
using System.Text;

namespace IURIS.COMMON.Entidades.UsuarioIntermedio
{
    public abstract class UsuarioIntermedioAdmin:BaseUsuarios
    {
        public string NombreCompleto { get; set; }
        public string Contrasenia { get; set; }

        public override string ToString()
        {
            return string.Format("- {0} -", NombreCompleto);
        }
    }
}
