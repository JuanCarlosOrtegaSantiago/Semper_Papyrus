using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace IURIS.MOVIL.Modelos_y_clases.DB_Local.COMMON
{
    public class UltimoUser
    {

        public UltimoUser()
        {
            Id = Guid.NewGuid().ToString();
        }
        [PrimaryKey]
        public string Id { get; set; }
        public string IdUser { get; set; }
    }
}
