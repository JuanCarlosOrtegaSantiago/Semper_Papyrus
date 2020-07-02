using IURIS.COMMON.Entidades.BaseUser;
using IURIS.COMMON.Entidades.CapaBase;
using IURIS.COMMON.Entidades.Ley;
using IURIS.COMMON.Entidades.UsuariosDeAplicacion.ComponentesDeUsuario;
using System;
using System.Collections.Generic;
using System.Text;

namespace IURIS.COMMON.Entidades.UsuariosDeAplicacion
{
    public class Usuarios : BaseUsuarios
    {
        public int IdApp { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public int Contrasenia { get; set; }
        public List<Leyes> MisLeyes { get; set; }
        public List<Clasificacion> Clasificaciones { get; set; }
    }
}
