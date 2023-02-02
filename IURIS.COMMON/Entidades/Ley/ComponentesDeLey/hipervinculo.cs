using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace IURIS.COMMON.Entidades.Ley.ComponentesDeLey
{
    public class hipervinculo: INotifyPropertyChanged
    {
        public Articulo _Articulo { get; set; }
        public Capitulo _Capitulo { get; set; }
        public Titulo _Titulo { get; set; }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
