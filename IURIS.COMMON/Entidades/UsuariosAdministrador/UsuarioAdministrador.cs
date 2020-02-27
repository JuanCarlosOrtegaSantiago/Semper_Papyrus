using IURIS.COMMON.Entidades.CapaBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace IURIS.COMMON.Entidades.UsuariosAdministrador
{
    public class UsuarioAdministrador:BaseDTO
    {
        public string NombreCompleto { get; set; }
        public string Direccion { get; set; }
        public string Correo { get; set; }
        public string Contrasenia { get; set; }
    }
}
