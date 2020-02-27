using IURIS.COMMON.Entidades.UsuariosAdministrador;
using System;
using System.Collections.Generic;
using System.Text;

namespace IURIS.COMMON.Interfaces
{
    public interface IManejadorDeUsuarioAdministrador:IManejadorGenerico<UsuarioAdministrador>
    {
        UsuarioAdministrador BuscarCorreo(string Correo);
        UsuarioAdministrador BuscarContrasenia(string Contrasenia);
    }
}
