using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace IURIS.COMMON.Entidades.Ley.ComponentesDeLey
{
    public class Articulo: INotifyPropertyChanged
    {
        public string NombreArticulo { get; set; }
        public string id { get; set; }
        public string NumArticulo { get; set; }
        public string Contenido { get; set; }
        public bool NotaAdjunta { get; set; }
        public string TextoDeNota { get; set; }
        public bool FotoAdjunta { get; set; }
        public List<Fotografia> Fotografia { get; set; }
        public List<Subrayado> Subrayados { get; set; }
        public bool TieneColorDeTexto { get; set; }
        public string ColorTextoHex { get; set; }
        public string TextoContenidoAnteriror { get; set; }
        public string TextoContenidoSeleccionado { get; set; }
        public string TextoContenidoDespues { get; set; }
        public bool TieneHipervinculo { get; set; }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
