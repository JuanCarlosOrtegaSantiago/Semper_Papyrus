using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace IURIS.APP.GUI.Model
{
    public class MasterMenuItem
    {
        public string Titulo { get; set; }
        public string IconSource { get; set; }
        public Color color { get; set; }
        public Type targetTipe { get; set; }

        public MasterMenuItem( string title, string IconSource, Color color, Type type)
        {
            this.Titulo = title;
            this.IconSource = IconSource;
            this.color = color;
            this.targetTipe = type;
        }
    }
}
