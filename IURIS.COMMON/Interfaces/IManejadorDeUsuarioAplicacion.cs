using IURIS.COMMON.Entidades.UsuariosDeAplicacion;
using System;
using System.Collections.Generic;
using System.Text;

namespace IURIS.COMMON.Interfaces
{
    public interface IManejadorDeUsuarioAplicacion:IManejadorGenerico<Usuarios>
    {
        bool EncontrarUsuario(string Correo, int contrasenia);
        bool ExisteCorreo(string Email);
    }
}
