using IURIS.COMMON.Entidades.Ley.ClasificacionDeLey;
using IURIS.COMMON.Entidades.Ley.ComponentesDeLey;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace IURIS.MOVIL.Modelos_y_clases.DB_Local.COMMON
{
    public class MyLey
    {
        public string NombreLey { get; set; }
        public string CodigoLey { get; set; }
        public bool EsModificacion { get; set; } //importante
        public DateTime UltimaFechaDeModificacion { get; set; }
        public DateTime? FechaDeDescarga { get; set; }
        public List<ClasificacionPUsuario> Clasificaciones { get; set; }
        public List<Titulo> ListaDeTitulos { get; set; }

        
        //[TextBlob("MyClasificacionPUsuarioBlobbed")]
        //public string MyClasificacionPUsuarioBlobbed { get; set; }
        

        //[TextBlob("MyTituloBlobbed")]
        //public string MyTituloBlobbed { get; set; }


        [TextBlob("MyClasificacionBlobbed")]
        public MyClasificacion Clasificacion { get; set; }
        public string MyClasificacionBlobbed { get; set; }
    }
}
