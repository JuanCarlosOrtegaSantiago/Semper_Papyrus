using IURIS.COMMON.Entidades.Ley;
using IURIS.COMMON.Entidades.Ley.ComponentesDeLey;
using IURIS.COMMON.Entidades.UsuariosDeAplicacion;
using IURIS.MOVIL.Views;
using IURIS.MOVIL.Views.ViewsVentanasEmergentes;
using IURIS.MOVIL.Utils;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using IURIS.MOVIL.Modelos_y_clases;
using System.IO;
using Xamarin.Forms.Shapes;
using IURIS.COMMON.Entidades.UsuariosDeAplicacion.ComponentesDeUsuario;
using IURIS.COMMON.Interfaces;
using IURIS.DAL;
using IURIS.BIZ;
using Plugin.Clipboard;
using Acr.UserDialogs;

namespace IURIS.MOVIL.Detail
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageMotrarCapitulosConCodigos : ContentPage
    {
        readonly Titulo _titulo;
        readonly Usuarios _Usuario;
        readonly Leyes _ley;
        public bool _ArticuloSeleccionado;
        static int _NumRecultadosEncontrados = 0;
        public Capitulo _Capitulo;
        public Articulo _Articulo;
        bool isRefreshing;
        IManejadorDeUsuarioAplicacion manejadorDeUsuarioAplicacion;
        private string ColorHex;
        Ellipse Ellipse_Cargado = null;
        int numToques = 0;
        public PageMotrarCapitulosConCodigos(Titulo titulo, Usuarios usuarios, Leyes ley)
        {
            InitializeComponent();
            BindingContext = this;

            _titulo = titulo;
            _Usuario = usuarios;
            _ley = ley;

            manejadorDeUsuarioAplicacion = new ManejadorDeUsuarioAplicacion(new RepositorioGenerico<Usuarios>());

            DatosAInicializar();
            
        }

        public bool IsRefreshing
        {
            get => isRefreshing;
            set
            {
                isRefreshing = value;
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }

        public ICommand RefreshCommand
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        if (_Capitulo!=null) ActualizarDatosArticulo(_Capitulo.ListaArticulos);
                        else ActualizarDatosCapitulo(_titulo.ListaCapitulos);
                        //await Task.Delay(1500); // Only to demonstrate refresh views..
                    }
                    finally
                    {
                        IsRefreshing = false;
                    }
                });
            }
        }
        
        private void DatosAInicializar()
        {
            lblTitle.Text = _titulo.NombreTitulo;
            lblCodigo.Text = _ley.CodigoLey;

            cllctionArticulos.ItemsSource = null;
            cllctionArticulos.ItemsSource = _titulo.ListaCapitulos.FirstOrDefault().ListaArticulos;//modificar
            _Capitulo = _titulo.ListaCapitulos.FirstOrDefault();



            ActualizarDatosCapitulo(_titulo.ListaCapitulos);

        }

        private void ActualizarDatosCapitulo(List<Capitulo> listaCapitulos)
        {
            clltionCapitulos.ItemsSource = null;
            clltionCapitulos.ItemsSource = listaCapitulos;

            //cllctionArticulos.ItemsSource = null;
            //cllctionArticulos.ItemsSource = listaCapitulos.FirstOrDefault().ListaArticulos;//modificar
            //_Capitulo = listaCapitulos.FirstOrDefault();
        }

        private void ActualizarDatosArticulo(List<Articulo> listaArticulos)
        {
                cllctionArticulos.SelectedItem = null;

                cllctionArticulos.ItemsSource = null;
                cllctionArticulos.ItemsSource = listaArticulos;
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            MostrarSearch(true);
        }

        private void BuscarTexto(TextChangedEventArgs TextChange)
        {


            //Revisar el por que los resultados se muestran en formato horizontal
            if (SearchViewDetailTitle.Text != null)
            {
                List<Articulo> _ListaArticulos= new List<Articulo>();
                List<Articulo> _ListaArticulosGenerico= new List<Articulo>();

                List<Capitulo> _ListaCapitulos = new List<Capitulo>();

                foreach (var item in _titulo.ListaCapitulos)
                {
                    _ListaArticulosGenerico = item.ListaArticulos.ToList().Where(e => e.NumArticulo.ToUpper().Contains(TextChange.NewTextValue.ToUpper()) == true || e.NombreArticulo.ToUpper().Contains(TextChange.NewTextValue.ToUpper()) == true || e.Contenido.ToUpper().Contains(TextChange.NewTextValue.ToUpper()) == true).ToList();

                foreach (var ati in _ListaArticulosGenerico)
                {
                    _ListaArticulos.Add(ati);
                }
                }

                
                _ListaCapitulos = _titulo.ListaCapitulos.ToList().Where(e => e.NumCapitulo.ToUpper().Contains(TextChange.NewTextValue.ToUpper()) == true || e.NombreCapitulo.ToUpper().Contains(TextChange.NewTextValue.ToUpper()) == true).ToList();


                ActualizarDatosCapitulo(_ListaCapitulos);
                ActualizarDatosArticulo(_ListaArticulos);

                _NumRecultadosEncontrados = _ListaArticulos.Count + _ListaCapitulos.Count;

                lblNumResultados.Text = TextChange.NewTextValue == ""? "" : _NumRecultadosEncontrados.ToString();
            }

        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            MostrarSearch(false);
        }

        private void SearchViewDetailTitle_TextChanged(object sender, TextChangedEventArgs e)
        {
            BuscarTexto(e);
        }

        private void SearchViewDetailTitle_SearchButtonPressed(object sender, EventArgs e)
        {
            MostrarSearch(false);
            SearchViewDetailTitle.Text = null;
        }

        private void MostrarSearch(bool v)
        {
            SearchViewDetailTitle.IsVisible = v;
            lblNumResultados.IsVisible = v;
            lblNumResultados.Text = null;
            GridTituloEIMGBuscador.IsVisible = !v;
        }

        private async void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            var articulo = ((Image)sender).BindingContext as Articulo;
            if (articulo == null) return;

            await PopupNavigation.Instance.PushAsync(new WindowOfEmergencyCrearNota(_titulo, _Usuario, articulo, _ley, _Capitulo), false);

        }

        private async void clltionCapitulos_PositionChanged(object sender, PositionChangedEventArgs e)
        {
            var Cap = ((CarouselView)sender).CurrentItem as Capitulo;
            if (!(Cap is Capitulo)) return;

            ActualizarDatosArticulo(Cap.ListaArticulos);
            _Capitulo = Cap;

            ClassMostrarP_Cmpra _Cmpra = new ClassMostrarP_Cmpra();
            if (_Cmpra.MostrarPantalla()) await PopupNavigation.Instance.PushAsync(new WindowOfMembresiaPlatino());

        }

        private async void TapGestureRecognizer_Tapped_3(object sender, EventArgs e)
        {
            var Cap = ((ContentView)sender).BindingContext as Capitulo;

            if (!(Cap is Capitulo)) return;

            ActualizarDatosArticulo(Cap.ListaArticulos);
            _Capitulo = Cap;

            ClassMostrarP_Cmpra _Cmpra = new ClassMostrarP_Cmpra();
            if (_Cmpra.MostrarPantalla()) await PopupNavigation.Instance.PushAsync(new WindowOfMembresiaPlatino());

        }

        private async void TapGestureRecognizer_Tapped_4(object sender, EventArgs e)
        {

            var articulo = ((Image)sender).BindingContext as Articulo;
            if (articulo == null) return;
            _Articulo = articulo;
            await PopupNavigation.Instance.PushAsync(new WindowOfMenuAccion(_titulo, _Usuario, articulo, _ley, _Capitulo), false);
            expandr.IsEnabled = true;
        }

        private void ObtenerImagenesDeArticulo()
        {

            ////if (_Articulo.FotoAdjunta)
            //{

            //    //List<Fotografia> vs = _Capitulo.ListaArticulos.Where(e=> e.FotoAdjunta==true).ToList();

            //    Stream stream = new MemoryStream(byteArray);
            //    //image.Source = ImageSource.FromStream(stream);
            //    image.Source = ImageSource.FromStream(() => { return stream; });
            //    //ImageSource.FromStream(() => new MemoryStream(imageAsBytes));
            //}

        }

        private void TapGestureRecognizer_Tapped_5Fotos(object sender, EventArgs e)
        {
            var Fot = ((Image)sender).BindingContext as Fotografia;

            if (!(Fot is Fotografia)) return;

            //Stream stream = new MemoryStream(Fot.Foto);
            //image.Source = ImageSource.FromStream(stream);
            //((Image)sender).Source = ImageSource.FromStream(() => { return stream; });
            ((Image)sender).Margin= new Thickness(20,20,20,20);
            ((Image)sender).Source = ImageSource.FromStream(() => new MemoryStream(Fot.Foto));

        }

        private void ArticulosConFoto_PositionChanged(object sender, PositionChangedEventArgs e)
        {
            var Fot = ((CarouselView)sender).BindingContext as Fotografia;

            if (!(Fot is Fotografia)) return;
        }

        private void EllipceColor(object sender, EventArgs e)
        {
            if (Ellipse_Cargado != null) Ellipse_Cargado.StrokeThickness = 0;

            ColorHex = ((Ellipse)sender).Fill.ToHex();
            Ellipse_Cargado = ((Ellipse)sender);
            ((Ellipse)sender).Stroke = Color.Black;
            ((Ellipse)sender).StrokeThickness = 4;
        }

        private void ExpaderForPlus(object sender, EventArgs e)
        {
            numToques += 1;
            if (numToques == 2)
            {
                expandr.IsEnabled = false;
                numToques = 0;
                _Articulo = null;
            }
            stakColores.IsVisible = stakColores.IsVisible==false? true:false;
            UserDialogs.Instance.Toast("Selecciona el texto, copialo y elije un color\nposteriormente realiza la accion de tu agrado", TimeSpan.FromMilliseconds(2500));

        }

        private async void TapGestureRecognizer_Tapped_5(object sender, EventArgs e)
        {
            try
            {

                //if (_Articulo == null) return;
                    string txt;
                txt = await CrossClipboard.Current.GetTextAsync();
                    if (_Articulo.Contenido.ToUpper().Contains(txt)) return;

                //var cap=_Capitulo.ListaArticulos.Where(r => r.Contenido.Contains(txt)).SingleOrDefault();


                if (ColorHex == null) return;
                   if(!_Articulo.TieneColorDeTexto) _Articulo.TieneColorDeTexto=true;
                    if (_Articulo.Subrayados == null)  _Articulo.Subrayados = new List<Subrayado>();

                //if (_Articulo.Subrayados.Find(w => w.ColorTextoHex == ColorHex) != null)
                //{
                //    await DisplayAlert("Error","Ya tienes ese color por favor elije otro","ok");
                //    return;
                //}

                Subrayado subrayado;
                Subrayado subrayadoTemp;

                int Items = _Articulo.Subrayados.Count();

                if (Items < 1)
                {

                    int x = _Articulo.Contenido.IndexOf(txt);
                    int y = txt.Length;


                    subrayado = new Subrayado()
                    {
                        ColorTextoHex = ColorHex,
                        TextoContenidoAnteriror = _Articulo.Contenido.Substring(0, x),
                        TextoContenidoSeleccionado = txt,
                        TextoContenidoDespues = ""
                    };
                    subrayadoTemp = new Subrayado()
                    {

                        TextoContenidoAnteriror = _Articulo.Contenido.Substring(x + y),
                        ColorTextoHex = null,
                        TextoContenidoDespues = "",
                        TextoContenidoSeleccionado = ""
                    };
                }
                else
                {

                    Subrayado sub = _Articulo.Subrayados[Items - 1];
                    //if (_Articulo.Subrayados.Where(w => w.ColorTextoHex == ColorHex).Count() >= 1)
                    //{
                        
                    //}

                    int x = sub.TextoContenidoAnteriror.IndexOf(txt);
                    int y = txt.Length;

                    subrayado = new Subrayado()
                    {
                        ColorTextoHex = ColorHex,
                        TextoContenidoAnteriror = sub.TextoContenidoAnteriror.Substring(0, x),
                        TextoContenidoSeleccionado = txt,
                        TextoContenidoDespues = ""
                    };

                    subrayadoTemp = new Subrayado()
                    {

                        TextoContenidoAnteriror = sub.TextoContenidoAnteriror.Substring(x + y),
                        ColorTextoHex = null,
                        TextoContenidoDespues = "",
                        TextoContenidoSeleccionado = ""
                    };

                    _Articulo.Subrayados.Remove(sub);
                }

                _Articulo.Subrayados.Add(subrayado);
                _Articulo.Subrayados.Add(subrayadoTemp);

                if (manejadorDeUsuarioAplicacion.Modificar(_Usuario)) IsRefreshing = true;
                else await DisplayAlert("Error", "No se han guardado los cambios", "Ok");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "Error: " + ex.Message, "ok");
                return;
            }
        }

        private async void TapGestureRecognizer_Tapped_6(object sender, EventArgs e)
        {
            try
            {

                if (ColorHex == null)
                    return;
                if (!_Articulo.TieneColorDeTexto)
                    return;
                
                string txtABorrar= await CrossClipboard.Current.GetTextAsync();
                
                Subrayado subrayado = _Articulo.Subrayados.Where(r => r.TextoContenidoSeleccionado.ToUpper().Contains(txtABorrar.ToUpper())).First();
                if (subrayado == null)
                    return;

                int Index = _Articulo.Subrayados.IndexOf(subrayado);

                if (_Articulo.Subrayados.Where(r => r.ColorTextoHex != null).Count() == 1)
                {
                    _Articulo.TieneColorDeTexto = false;
                    _Articulo.Subrayados = null;

                }
                else
                {
                    _Articulo.Subrayados[Index + 1].TextoContenidoAnteriror = string.Format("{0}{1}{2}", _Articulo.Subrayados[Index].TextoContenidoAnteriror, _Articulo.Subrayados[Index].TextoContenidoSeleccionado, _Articulo.Subrayados[Index + 1].TextoContenidoAnteriror);
                    _Articulo.Subrayados[Index + 1].TextoContenidoDespues = "";
                    if (_Articulo.Subrayados[Index + 1].TextoContenidoSeleccionado == "" && _Articulo.Subrayados[Index + 1].ColorTextoHex == null)
                    {
                        _Articulo.Subrayados[Index + 1].TextoContenidoSeleccionado = "";
                        _Articulo.Subrayados[Index + 1].ColorTextoHex = null;

                    }
                    _Articulo.Subrayados.Remove(subrayado);
                }

                if (manejadorDeUsuarioAplicacion.Modificar(_Usuario)) 
                    IsRefreshing = true;
            }
            catch (Exception)
            {
                await DisplayAlert("Error", "No se ha podido borrar el subrayado,\npor favor intente mas tarde", "ok");
            }
        }
    }
}