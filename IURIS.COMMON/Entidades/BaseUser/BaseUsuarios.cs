using IURIS.COMMON.Entidades.CapaBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace IURIS.COMMON.Entidades.BaseUser
{
    public abstract class BaseUsuarios:BaseDTO
    {
        public string Correo { get; set; }
    }
}
