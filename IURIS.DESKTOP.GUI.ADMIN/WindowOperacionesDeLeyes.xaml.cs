using IURIS.BIZ;
using IURIS.COMMON.Entidades.Ley;
using IURIS.COMMON.Entidades.Ley.ClasificacionDeLey;
using IURIS.COMMON.Entidades.Ley.ComponentesDeLey;
using IURIS.COMMON.Interfaces;
using IURIS.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace IURIS.DESKTOP.GUI.ADMIN
{
    /// <summary>
    /// Lógica de interacción para WindowOperacionesDeLeyes.xaml
    /// </summary>
    public partial class WindowOperacionesDeLeyes : Window
    {
        //public WindowOperacionesDeLeyes(bool EsNuevaLey)
        List<Articulo> articulos;
        List<Capitulo> capitulos;
        List<Titulo> titulos;
        Leyes ley;
        bool CodigoExistente=false;
        IManejadorDeLeyes manejadorDeLeyes;
        IManejadorDeClasificaciones manejadorDeClasificaciones;
        static TimeoutException ExSinInternet = new TimeoutException();
        public bool HayInternet = true;


        public WindowOperacionesDeLeyes()
        {
            InitializeComponent();

            try
            {
                manejadorDeLeyes = new ManejadorDeLeyes(new RepositorioGenerico<Leyes>());
                manejadorDeClasificaciones = new ManejadorDeClasificaciones(new RepositorioGenerico<Clasificacion>());

            }
            catch (Exception ex)
            {

                if (ex.HResult == ExSinInternet.HResult)
                    NoHayInternet();
            }

            DatosAInicializar();
        }

        private void NoHayInternet()
        {
            MessageBox.Show("Revisa tu conexion a internet", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void DatosAInicializar()
        {
            EstadoDeCajas(false);

            CmbxClasificacion.ItemsSource = null;
            CmbxClasificacion.ItemsSource = manejadorDeClasificaciones.Listar;
        }

        private void EstadoDeCajas(bool v)
        {
            LimpiarCajas();
            //Abilitar cajas
            RtcTxtContenido.IsEnabled = v;
            txtCodigo.IsEnabled = v;
            txtNombreArticulo.IsEnabled = v;
            txtNombreCapitulo.IsEnabled = v;
            TxtNombreDeLey.IsEnabled = v;
            txtNombreTitulo.IsEnabled = v;
            txtNumArticulo.IsEnabled = v;
            txtNumCapitulo.IsEnabled = v;
            txtNumTitulo.IsEnabled = v;
            CmbxClasificacion.IsEnabled = v;
            //Habilitar botones
            BtnAgregarArticulo.IsEnabled = v;
            BtnAgregarCapitulo.IsEnabled = v;
            BtnAgregarTitulo.IsEnabled = v;
            BtnBuscarCodgio.IsEnabled = v;
            BtnRegresarAMenuDeOperaciones.IsEnabled = !v;
            BtnNuevaLey.IsEnabled = !v;
            BtnCancelar.IsEnabled = v;
            BtnSubirLey.IsEnabled = v;


        }

        private void LimpiarCajas()
        {
            //Limpiar cajas
            LimpiarCajaDeContenido();
            txtCodigo.Clear();
            txtNombreArticulo.Clear();
            txtNombreCapitulo.Clear();
            TxtNombreDeLey.Clear();
            txtNombreTitulo.Clear();
            txtNumArticulo.Clear();
            txtNumCapitulo.Clear();
            txtNumTitulo.Clear();
            CmbxClasificacion.SelectedItem = null;
        }

        private void BtnCambiarDeAlturaMinimizar_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
                BtnCambiarDeAlturaMaximisar.Visibility = Visibility.Visible;
                BtnCambiarDeAlturaMinimizar.Visibility = Visibility.Collapsed;
            }
            
        }

        private void BtnCambiarDeAlturaMaximisar_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
            {
                this.WindowState = WindowState.Maximized;
                BtnCambiarDeAlturaMaximisar.Visibility = Visibility.Collapsed;
                BtnCambiarDeAlturaMinimizar.Visibility = Visibility.Visible;
            }
        }

        private void BtnRegresarAMenuDeOperaciones_Click(object sender, RoutedEventArgs e)
        {

            if (MessageBox.Show("¿Realmente decea salir?", "Advertencia", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No) == MessageBoxResult.Yes)
            {
                WindowOperaciones windowOperaciones = new WindowOperaciones();
                this.Close();
                windowOperaciones.Show();

            }


        }

        private void BtnBuscarCodgio_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (manejadorDeLeyes.BuscarPorCodigo(txtCodigo.Text))
                {
                    WrpLblCodigoAsociado.Visibility = Visibility.Visible;
                    CodigoExistente = true;
                }

            }
            catch (Exception ex)
            {

                if (ex.HResult == ExSinInternet.HResult)
                    NoHayInternet();

            }
        }

        private void BtnNuevaLey_Click(object sender, RoutedEventArgs e)
        {
            EstadoDeCajas(true);
            articulos = new List<Articulo>();
            capitulos = new List<Capitulo>();
            titulos = new List<Titulo>();
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            EstadoDeCajas(false);
        }

        private string Contenido()
        {
            //string richText;
            return new TextRange(RtcTxtContenido.Document.ContentStart, RtcTxtContenido.Document.ContentEnd).Text;
        }

        private void LimpiarCajaDeContenido()
        {
            TextRange textRange = new TextRange(RtcTxtContenido.Document.ContentStart, RtcTxtContenido.Document.ContentEnd);
            textRange.Text = "";
        }

        private void BtnAgregarArticulo_Click(object sender, RoutedEventArgs e)
        {

            if (!string.IsNullOrWhiteSpace(txtNumArticulo.Text) && !string.IsNullOrWhiteSpace(txtNombreArticulo.Text) && Contenido() != "")
            {
                Articulo articulo = new Articulo()
                {
                    Contenido = Contenido(),
                    NombreArticulo = txtNombreArticulo.Text,
                    NumArticulo = txtNumArticulo.Text

                };
                if (MessageBox.Show("¿La informacion es correcta?", "", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
                {

                    articulos.Add(articulo);
                    LimpiarCajas();
                    if (MessageBox.Show("¿Deseas agregar otro articulo?", "", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes) == MessageBoxResult.No)
                    {
                        WrpArticulo.IsEnabled = false;
                        RtcTxtContenido.IsEnabled = false;
                    }
                }

            }
            else
            {
                MessageBox.Show("Faltan datos por llenar", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BtnAgregarCapitulo_Click(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(txtNumCapitulo.Text) && !string.IsNullOrWhiteSpace(txtNombreCapitulo.Text) && articulos != null)
            {
                Capitulo capitulo = new Capitulo()
                {
                    NombreCapitulo = txtNombreCapitulo.Text,
                    NumCapitulo = txtNumCapitulo.Text,
                    ListaArticulos = articulos
                };
                if (MessageBox.Show("¿La informacion es correcta?", "", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
                {
                    articulos = new List<Articulo>();
                    capitulos.Add(capitulo);
                    LimpiarCajas();
                    if (MessageBox.Show("¿Deseas agregar otro capitulo?", "", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes) == MessageBoxResult.No)
                    {
                        WrpArticulo.IsEnabled = false;
                        RtcTxtContenido.IsEnabled = false;
                        WrpCapitulo.IsEnabled = false;
                    }
                    else
                    {
                        WrpArticulo.IsEnabled = true;
                        RtcTxtContenido.IsEnabled = true;
                        WrpCapitulo.IsEnabled = true;
                    }
                }
            }
            else
            {
                if (articulos == null)
                    MessageBox.Show("No tienes articulos agregados", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                else
                    MessageBox.Show("Faltan datos por llenar", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BtnAgregarTitulo_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtNombreTitulo.Text) && !string.IsNullOrWhiteSpace(txtNumTitulo.Text) && capitulos != null)
            {
                Titulo titulo = new Titulo()
                {
                    NombreTitulo = txtNombreTitulo.Text,
                    NumTitulo = txtNumTitulo.Text,
                    ListaCapitulos = capitulos
                };
                if (MessageBox.Show("¿La informacion es correcta?", "", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
                {
                    capitulos = new List<Capitulo>();
                    titulos.Add(titulo);
                    LimpiarCajas();
                    if (MessageBox.Show("¿Deseas agregar otro Titulo?", "", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes) == MessageBoxResult.No)
                    {
                        WrpArticulo.IsEnabled = false;
                        RtcTxtContenido.IsEnabled = false;
                        WrpCapitulo.IsEnabled = false;
                        WrpTitulo.IsEnabled = false;
                    }
                    else
                    {
                        WrpArticulo.IsEnabled = true;
                        RtcTxtContenido.IsEnabled = true;
                        WrpCapitulo.IsEnabled = true;
                        WrpTitulo.IsEnabled = true;
                    }
                }
            }
            else
            {
                if (capitulos == null)
                    MessageBox.Show("No tienes capitulos agregados", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                else
                    MessageBox.Show("Faltan datos por llenar", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BtnSubirLey_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtCodigo.Text) && !string.IsNullOrWhiteSpace(TxtNombreDeLey.Text) && titulos != null && CmbxClasificacion.SelectedItem!=null)
            {

                if (MessageBox.Show("Esta seguro de subir la informacion", "", MessageBoxButton.YesNo, MessageBoxImage.Exclamation, MessageBoxResult.No) == MessageBoxResult.Yes)
                {
                    if (!CodigoExistente)
                    {
                        ley = new Leyes()
                        {
                            CodigoLey = txtCodigo.Text,
                            ListaDeTitulos = titulos,
                            NombreLey = TxtNombreDeLey.Text,
                            numDescargas = 0,
                            UltimaFechaDeModificacion = DateTime.Now.Date,
                            EsModificacion = false,
                            Clasificacion = (Clasificacion)CmbxClasificacion.SelectedItem
                        };
                        try
                        {
                            if (manejadorDeLeyes.AGREGAR(ley))
                            {
                                MessageBox.Show("La ley ha subida con exito", "Correcto", MessageBoxButton.OK, MessageBoxImage.Information);
                                EstadoDeCajas(false);
                            }

                        }
                        catch (Exception ex)
                        {

                            if (ex.HResult == ExSinInternet.HResult)
                                NoHayInternet();
                        }
                    }
                    else
                    {
                        MessageBox.Show("El codigo escrito ya existe", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }

                }
            }
            else
            {
                if (titulos == null)
                    MessageBox.Show("No tienes titulos agregados", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                else
                    MessageBox.Show("Faltan datos por llenar", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
