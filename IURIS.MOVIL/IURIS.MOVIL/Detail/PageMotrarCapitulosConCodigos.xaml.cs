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
using MarcTron.Plugin.Controls;
using Xamarin.Essentials;
using IURIS.MOVIL.Modelos_y_clases.DB_Local.COMMON;
using Xamarin.CommunityToolkit.UI.Views;
using System.Diagnostics;

namespace IURIS.MOVIL.Detail
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageMotrarCapitulosConCodigos : ContentPage
    {
        readonly Titulo _titulo;
        MyLey _MyLey;
        public bool _ArticuloSeleccionado;
        static int _NumRecultadosEncontrados = 0;
        public Capitulo _Capitulo;
        public Capitulo _CapituloFind;
        public Articulo _Articulo;
        public Articulo _ArticuloFin;
        bool isRefreshing;
        private string ColorHex;
        Ellipse Ellipse_Cargado = null;
        int numToques = 0;
        ClassAnuncio Anuncio = new ClassAnuncio();

        public PageMotrarCapitulosConCodigos(Titulo titulo, MyLey ley, MTAdView adView, Capitulo CapAIniciar,Articulo articulo)
        {
            InitializeComponent();
            _MyLey = ley;
            BindingContext = this;
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await Task.Delay(1500);
                myAds.IsVisible = true;
            });

            _titulo = titulo;
            _CapituloFind = CapAIniciar;
            _ArticuloFin = articulo;
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

            Anuncio.MostrarAnuncioPantallaParaTitulos();

            ActualizarDatosCapitulo(_titulo.ListaCapitulos);
            lblTitle.Text = _titulo.NombreTitulo;
            lblCodigo.Text = _MyLey.CodigoLey;
            cllctionArticulos.ItemsSource = null;

            if (_CapituloFind != null)
            {

                cllctionArticulos.ItemsSource = _titulo.ListaCapitulos.Find(s => s.id.Equals(_CapituloFind.id)).ListaArticulos;
                clltionCapitulos.Position = _titulo.ListaCapitulos.IndexOf(_CapituloFind);
                if (_ArticuloFin != null)
                {
                    List<Articulo> ar = new List<Articulo>() { _ArticuloFin };
                    cllctionArticulos.ItemsSource = ar;
                    cllctionArticulos.ScrollTo(_ArticuloFin, position: ScrollToPosition.Start);
                }
            }
            else
            {

                cllctionArticulos.ItemsSource = _titulo.ListaCapitulos.FirstOrDefault().ListaArticulos;//modificar
                _Capitulo = _titulo.ListaCapitulos.FirstOrDefault();
            }

        }

        private void ActualizarDatosCapitulo(List<Capitulo> listaCapitulos)
        {
            clltionCapitulos.ItemsSource = null;
            clltionCapitulos.ItemsSource = listaCapitulos;
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

            await PopupNavigation.Instance.PushAsync(new WindowOfEmergencyCrearNota(_titulo, articulo, _MyLey, _Capitulo), false);

        }

        private void clltionCapitulos_PositionChanged(object sender, PositionChangedEventArgs e)
        {
            var Cap = ((CarouselView)sender).CurrentItem as Capitulo;
            if (!(Cap is Capitulo)) return;

            ActualizarDatosArticulo(Cap.ListaArticulos);
            _Capitulo = Cap;

            Anuncio.MostrarAnuncioPantallaParaTitulos();

        }

        private void TapGestureRecognizer_Tapped_3(object sender, EventArgs e)
        {
            var Cap = ((ContentView)sender).BindingContext as Capitulo;

            if (!(Cap is Capitulo)) return;

            ActualizarDatosArticulo(Cap.ListaArticulos);
            _Capitulo = Cap;

            Anuncio.MostrarAnuncioPantalla();
        }

        private async void TapGestureRecognizer_Tapped_4(object sender, EventArgs e)
        {
            var articulo = ((Image)sender).BindingContext as Articulo;
            if (articulo == null) return;
            
            _Articulo = articulo;
            await PopupNavigation.Instance.PushAsync(new WindowOfMenuAccion(_titulo, articulo, _MyLey, _Capitulo), false);
            expandr.IsEnabled = true;
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
            if (Ellipse_Cargado != null)
            {
                Ellipse_Cargado.StrokeThickness = 0;
                Ellipse_Cargado.Stroke = Brush.Transparent;
            }
            ColorHex = ((SolidColorBrush)(((Ellipse)sender).Fill)).Color.ToHex();
            Ellipse_Cargado = ((Ellipse)sender);
            ((Ellipse)sender).Stroke = Brush.Black;
            ((Ellipse)sender).StrokeThickness = 4;
        }

        private async void ExpaderForPlus(object sender, EventArgs e)
        {
            numToques += 1;
            if (numToques == 2)
            {
                expandr.IsEnabled = false;
                numToques = 0;
                _Articulo = null;
            }
            stakColores.IsVisible = stakColores.IsVisible==false? true:false;
            try
            {
                if (!expandr.IsExpanded)
                {

                await PopupNavigation.Instance.PushAsync(new WindowAlert("Selecciona tu color favorito y guardalo/eliminalo"), false);
                await Task.Delay(4000);
                await PopupNavigation.Instance.PopAsync(false);
                }
            }
            catch (Exception ex)
            {
                return;
            }

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
                if (_MyLey.ClasificacionesPorColores == null) 
                    _MyLey.ClasificacionesPorColores= new List<ClasificacionPorColor>();
                 
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

                ClasificacionPorColor porColor = new ClasificacionPorColor
                {
                    ColorHexDeClasificado = ColorHex,
                    _Articulo = this._Articulo,
                    Clasificados = subrayado
                };

                _MyLey.ClasificacionesPorColores.Add(porColor);

                if (await App.Database.UpdateUserAsync(App.MyUser))
                {
                    IsRefreshing = true;
                    return;
                }
                else
                {
                    try
                    {
                        await PopupNavigation.Instance.PushAsync(new WindowAlert("Error", "No se han guardado los cambios", "Ok"), false);
                        await Task.Delay(3000);
                        await PopupNavigation.Instance.PopAsync(false);
                    }
                    catch (Exception)
                    {

                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                //await DisplayAlert("Error", "Error: " + ex.Message, "ok");
                try
                {
                    await PopupNavigation.Instance.PushAsync(new WindowAlert("Error",ex.Message,"Ok"), false);
                    await Task.Delay(5000);
                    await PopupNavigation.Instance.PopAsync(false);
                }
                catch (Exception)
                {

                    return;
                }
                return;
            }
            finally
            {
                expandr.IsExpanded = false;
                stakColores.IsVisible = false;
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

                if (await App.Database.UpdateUserAsync(App.MyUser)) 
                    IsRefreshing = true;
            }
            catch (Exception)
            {
                //await DisplayAlert("Error", "No se ha podido borrar el subrayado,\npor favor intente mas tarde", "ok");
                try
                {
                    await PopupNavigation.Instance.PushAsync(new WindowAlert("Error", "No se ha podido borrar el subrayado,\npor favor intente mas tarde", "Ok"), false);
                    await Task.Delay(5000);
                    await PopupNavigation.Instance.PopAsync(false);
                }
                catch (Exception)
                {

                    return;
                }
            }
            finally
            {
                expandr.IsExpanded = false;
                stakColores.IsVisible = false;
            }
        }

        private async void TapGestureRecognizer_Tapped_7(object sender, EventArgs e)
        {
            try
            {
                await PopupNavigation.Instance.PushAsync(new WindowAlert(_titulo.NombreTitulo), false);
                await Task.Delay(2500);
                await PopupNavigation.Instance.PopAsync(false);
            }
            catch (Exception)
            {
                return;
            }
        }

        private void CrearHipervinculo(object sender, EventArgs e)
        {
            Hipertex.IsVisible = true;
            expan.IsExpanded = false;
        }

        private void MostrarHipervinculo(object sender, EventArgs e)
        {

        }

        private async void DropGestureRecognizer_DragOver(object sender, DragEventArgs e)
        {
            try
            {
                var articulo = (sender as Element).BindingContext as Articulo;
                
                
            }
            catch (Exception)
            {
                await PopupNavigation.Instance.PushAsync(new WindowAlert("Bien", "sasasa", "Ok"), false);

            }
        }
    }
}