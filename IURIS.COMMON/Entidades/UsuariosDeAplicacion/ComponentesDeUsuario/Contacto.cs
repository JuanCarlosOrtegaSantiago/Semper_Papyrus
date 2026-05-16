using System;

namespace IURIS.COMMON.Entidades.UsuariosDeAplicacion.ComponentesDeUsuario
{
    public class Contacto
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Correo { get; set; }
        public int IdAppContacto { get; set; } // ID de la app del contacto
        public DateTime FechaAgregado { get; set; }
        public string NombreCompleto => $"{Nombre} {ApellidoPaterno} {ApellidoMaterno}";

        public Contacto()
        {
            Id = Guid.NewGuid().ToString();
            FechaAgregado = DateTime.Now;
        }
    }
}
