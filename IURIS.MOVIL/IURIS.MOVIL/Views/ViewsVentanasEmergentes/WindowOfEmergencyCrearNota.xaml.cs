using IURIS.BIZ;
using IURIS.COMMON.Entidades.Ley;
using IURIS.COMMON.Entidades.Ley.ComponentesDeLey;
using IURIS.COMMON.Entidades.UsuariosDeAplicacion;
using IURIS.COMMON.Interfaces;
using IURIS.DAL;
using IURIS.MOVIL.Detail;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IURIS.MOVIL.Views.ViewsVentanasEmergentes
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WindowOfEmergencyCrearNota : PopupPage
    {
        readonly IManejadorDeUsuarioAplicacion manejadorDeUsuarioAplicacion;
        readonly Titulo _Titulo;
        readonly Usuarios _User;
        readonly Articulo _Articulo;
        readonly Leyes _Ley;
        readonly Capitulo _Capitulo;

        public WindowOfEmergencyCrearNota(Titulo titulo, Usuarios usuarios, Articulo articulo,Leyes ley, Capitulo capitulo)
        {
            InitializeComponent();
            _Articulo = articulo;
            _Titulo = titulo;
            _User = usuarios;
            _Ley = ley;
            _Capitulo = capitulo;

            manejadorDeUsuarioAplicacion = new ManejadorDeUsuarioAplicacion(new RepositorioGenerico<Usuarios>());

            DatosAInicializar();

        }

        private void DatosAInicializar()
        {
            if (!_Articulo.NotaAdjunta) return;

                EntryNombreApunte.Text = _Articulo.TextoDeNota;
                MostarBotonEliminar(true);
        }

        private void MostarBotonEliminar(bool v)
        {
            BtnCancelar.IsVisible = !v;
            BtnEliminar.IsVisible = v;
        }

        private void BtnCancelar_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync(false);
        }

        private void BtnGuardar_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(EntryNombreApunte.Text)) return;

            //List<Titulo> titulos = (List<Titulo>)_Ley.ListaDeTitulos;
            //_titulo = (Titulo)titulos.Find(i => i.NumTitulo == _Titulo.NumTitulo);

            //List<Capitulo> capitulos = _titulo.ListaCapitulos as List<Capitulo>;
            //_capitulo = capitulos.Find(i => i.NumCapitulo == _Capitulo.NumCapitulo) as Capitulo;

            //List<Articulo> articulos = _capitulo.ListaArticulos as List<Articulo>;

            //_articulo = articulos.Find(i => i.NumArticulo == _Articulo.NumArticulo) as Articulo;

            if (!EntryNombreApunte.Text.Equals(_Articulo.TextoDeNota))
            {

                _Articulo.NotaAdjunta = true;
                _Articulo.TextoDeNota = EntryNombreApunte.Text;

                //int NumIndex= _User.MisLeyes.IndexOf(_LeyCopia);
                //_User.MisLeyes.RemoveAt(NumIndex);
                //_User.MisLeyes.Insert(NumIndex,_Ley);
                if (!manejadorDeUsuarioAplicacion.Modificar(_User)) return;
            }


            PopupNavigation.Instance.PopAsync(false);

        }

        private void BtnEliminar_Clicked(object sender, EventArgs e)
        {
            _Articulo.NotaAdjunta = false;
            _Articulo.TextoDeNota = null;

            if (!manejadorDeUsuarioAplicacion.Modificar(_User)) return;

            PopupNavigation.Instance.PopAsync(false);

        }
    }
}