using IURIS.COMMON.Entidades.Ley.ComponentesDeLey;
using IURIS.COMMON.Entidades.UsuariosDeAplicacion;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IURIS.COMMON.Interfaces
{
    public interface IManejadorDeUsuarioAplicacion:IManejadorGenerico<Usuarios>
    {

        Task<Usuarios> ConsultUsuario(string IdApp, string Nombre, string A_Paterno, string A_Materno);
        //Usuarios EncontrarUsuario(string Correo, int contrasenia);
        //Usuarios EncontrarUsuarioID(int numUsuaro);
        //bool ExisteCorreo(string Email);
        //Usuarios BuscarUsuarioParaContrasenia(string Correo, string Nombre, string ApellidoPaterno, string ApellidoMaterno);
        //Usuarios NoRecuerdoMiContrasenia(string Nombre, string ApelidoPaterno, string ApellidoMaterno, string Correo);
    }
}
