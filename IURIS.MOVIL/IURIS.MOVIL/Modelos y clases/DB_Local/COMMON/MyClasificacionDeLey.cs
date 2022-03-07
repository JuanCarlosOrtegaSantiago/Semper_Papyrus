using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace IURIS.MOVIL.Modelos_y_clases.DB_Local.COMMON
{
    public class MyClasificacionDeLey
    {

        public MyClasificacionDeLey()
        {
            Id = Guid.NewGuid().ToString();
        }
        [PrimaryKey]
        public string Id { get; set; }

        public string Nombre { get; set; }

        public override string ToString()
        {
            return string.Format("{0}", Nombre);
        }
    }
}
