using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace IURIS.COMMON.Entidades.CapaBase
{
    public abstract class BaseDTO
    {
        public ObjectId id { get; set; }
    }
}
