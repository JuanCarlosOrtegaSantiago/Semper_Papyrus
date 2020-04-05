using IURIS.COMMON.Entidades.CapaBase;
using IURIS.COMMON.Entidades.UsuarioIntermedio;
using System;
using System.Collections.Generic;
using System.Text;

namespace IURIS.COMMON.Entidades.UsuariosAdministrador
{
    public class UsuarioAdministrador:UsuarioIntermedioAdmin
    {
        public string Direccion { get; set; }
        
    }
}
