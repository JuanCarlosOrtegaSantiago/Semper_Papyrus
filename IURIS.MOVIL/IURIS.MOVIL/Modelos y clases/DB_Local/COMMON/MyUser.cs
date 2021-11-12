using IURIS.COMMON.Entidades.Ley;
using IURIS.COMMON.Entidades.UsuariosDeAplicacion;
using IURIS.COMMON.Entidades.UsuariosDeAplicacion.ComponentesDeUsuario;
using IURIS.COMMON.Entidades.UsuariosDeAplicacion.ComponentesDeUsuario.DatosCriticos;
using IURIS.MOVIL.Modelos_y_clases.DB_Local.COMMON;
using MongoDB.Bson;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace IURIS.MOVIL.Modelos_y_clases.DB_Local
{
    public class MyUser
    {
        public MyUser()
        {
            Id = Guid.NewGuid().ToString();
        }
        [PrimaryKey]
        public string Id { get; set; }
        public string MiUltimaLeyCargada { get; set; }
        public string IdUser { get; set; }
        public int IdApp { get; set; }
        public string Correo { get; set; }
        public int Contrasenia { get; set; }


        [TextBlob("MysLeyesBlobbed")]
        public List<MyLey> MisLeyes { get; set; }
        public string MysLeyesBlobbed { get; set; }


        [TextBlob("ApunttesBlobbed")]
        public List<Apunte> Apuntes { get; set; }
        public string ApunttesBlobbed { get; set; }

    }
}
