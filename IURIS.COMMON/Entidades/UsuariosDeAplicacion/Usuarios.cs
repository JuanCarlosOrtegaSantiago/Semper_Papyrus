using IURIS.COMMON.Entidades.CapaBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace IURIS.COMMON.Entidades.UsuariosDeAplicacion
{
    public class Usuarios:BaseDTO
    {
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Correo { get; set; }
        public int Contrasenia { get; set; }
    }
}
