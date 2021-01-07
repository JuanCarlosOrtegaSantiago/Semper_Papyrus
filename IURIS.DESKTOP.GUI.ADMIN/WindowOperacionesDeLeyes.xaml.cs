using IURIS.BIZ;
using IURIS.COMMON.Entidades.Ley;
using IURIS.COMMON.Entidades.Ley.ClasificacionDeLey;
using IURIS.COMMON.Entidades.Ley.ComponentesDeLey;
using IURIS.COMMON.Interfaces;
using IURIS.DAL;
using MongoDB.Bson;
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
        readonly IManejadorDeLeyes manejadorDeLeyes;
        readonly IManejadorDeClasificaciones manejadorDeClasificaciones;
        public bool HayInternet = true;

        public WindowOperacionesDeLeyes()
        {
            InitializeComponent();

            //try
            //{
            //    manejadorDeLeyes = new ManejadorDeLeyes(new RepositorioGenerico<Leyes>());
            //    manejadorDeClasificaciones = new ManejadorDeClasificaciones(new RepositorioGenerico<Clasificacion>());
                DatosAInicializar();

            //}
            //catch (TimeoutException)
            //{

            //        MensajeDeExcepcion("Revisa tu conexion a internet");
            //}
            //catch (Exception ex)
            //{
            //    MensajeDeExcepcion(ex.Message);
            //}

        }

        private void MensajeDeExcepcion(string Contenido)
        {
            MessageBox.Show(Contenido, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void DatosAInicializar()
        {
            EstadoDeCajas(false);
           // CargarDatosAlCombo();
        }

        private void CargarDatosAlCombo()
        {
            CmbxClasificacion.ItemsSource = null;

            if (manejadorDeClasificaciones.Listar.Count() <= 0)
            {
                List<string> vs = new List<string>
                {
                    "Agregar Nueva Clasificación"
                };
                CmbxClasificacion.ItemsSource = vs;
            }
            else
            {
                CmbxClasificacion.ItemsSource = manejadorDeClasificaciones.Listar;
            }
        }

        private void EstadoDeCajas(bool v)
        {
            //Habilitar cajas
            RtcTxtContenido.IsEnabled = v;
            txtCodigo.IsEnabled = v;
            txtNombreArticulo.IsEnabled = v;
            txtNombreCapitulo.IsEnabled = v;
            TxtNombreDeLey.IsEnabled = v;//
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

            LimpiarCajas();

        }

        private void LimpiarCajas()
        {
            LimpiarCajaDeContenido(RtcTxtContenido);
            LimpiarCajaDeContenido(txtNombreArticulo);
            LimpiarCajaDeContenido(txtNombreCapitulo);
            LimpiarCajaDeContenido(txtNombreTitulo);
            txtCodigo.Clear();
            TxtNombreDeLey.Clear();
            txtNumArticulo.Clear();
            txtNumCapitulo.Clear();
            txtNumTitulo.Clear();
            CmbxClasificacion.SelectedItem = null;
            WrpLblCodigoAsociado.Visibility = Visibility.Collapsed;
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
                if (manejadorDeLeyes.BuscarPorCodigo(txtCodigo.Text.ToUpper())!=null)
                {
                    WrpLblCodigoAsociado.Visibility = Visibility.Visible;
                }
                else
                {

                    WrpLblCodigoAsociado.Visibility = Visibility.Collapsed;
                    CodigoExistente = false;
                }

            }
            catch (TimeoutException)
            {

                MensajeDeExcepcion("Revisa tu conexion a internet");
            }
            catch (Exception ex)
            {
                MensajeDeExcepcion(ex.Message);
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

        private string Contenido(RichTextBox caja)
        {
            return new TextRange(caja.Document.ContentStart, caja.Document.ContentEnd).Text;
        }

        private void LimpiarCajaDeContenido(RichTextBox caja)
        {
            _ = new TextRange(caja.Document.ContentStart, caja.Document.ContentEnd)
            {
                Text = ""
            };
        }

        private void BtnAgregarArticulo_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtNumArticulo.Text))
            {
                MessageBox.Show("Faltan datos por llenar", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            
         
            if (MessageBox.Show("¿La informacion es correcta?", "", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.No) return;

            Articulo articulo = new Articulo()
            {
                Contenido = Contenido(RtcTxtContenido),
                NombreArticulo = Contenido(txtNombreArticulo),
                NumArticulo = txtNumArticulo.Text,
                id = Guid.NewGuid().ToString()

            };

            articulos.Add(articulo);
            LimpiarCajas();
            
            if (MessageBox.Show("¿Deseas agregar otro articulo?", "", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes) == MessageBoxResult.No)
            {
                WrpArticulo.IsEnabled = false;
                RtcTxtContenido.IsEnabled = false;
            }

        }

        private void BtnAgregarCapitulo_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNumCapitulo.Text))
            {
                MessageBox.Show("Faltan datos por llenar", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            //string codigo = string.Format("{0}", Guid.NewGuid().ToString());
            if (MessageBox.Show("¿La informacion es correcta?", "", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.No) return;

            Capitulo capitulo = new Capitulo()
            {
                NombreCapitulo = Contenido(txtNombreCapitulo),
                NumCapitulo = txtNumCapitulo.Text,
                ListaArticulos = articulos,
                id = Guid.NewGuid().ToString()
            };
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

        private void BtnAgregarTitulo_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNumTitulo.Text))
            {
                MessageBox.Show("Faltan datos por llenar", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            //string codigo = string.Format("{0}",);

         
            if (MessageBox.Show("¿La informacion es correcta?", "", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.No) return;
            
            Titulo titulo = new Titulo()
            {
                NombreTitulo = Contenido(txtNombreTitulo),
                NumTitulo = txtNumTitulo.Text,
                ListaCapitulos = capitulos,
                id = Guid.NewGuid().ToString()
            };
            
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
                            UltimaFechaDeModificacion = DateTime.Today.Date,
                            EsModificacion = false,
                            Clasificacion = (Clasificacion)CmbxClasificacion.SelectedItem,
                            //CodigosDeVentas = new List<CodigoVenta>()
                        };
                        try
                        {
                            if (manejadorDeLeyes.AGREGAR(ley))
                            {
                                MessageBox.Show("La ley ha subida con exito", "Correcto", MessageBoxButton.OK, MessageBoxImage.Information);
                                EstadoDeCajas(false);
                            }

                        }
                        catch (TimeoutException)
                        {

                            MensajeDeExcepcion("Revisa tu conexion a internet");
                        }
                        catch (Exception ex)
                        {
                            MensajeDeExcepcion(ex.Message);
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

        private void CmbxClasificacion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CmbxClasificacion.SelectedItem != null)
            {
                if (CmbxClasificacion.SelectedItem.ToString() == "Agregar Nueva Clasificación")
                {
                    WindowClasificacion clasificacion = new WindowClasificacion
                    {
                        NoHayClasificacion_Agregar = true
                    };
                    clasificacion.ShowDialog();
                    if (clasificacion.SeAgregoUnaClasificacion)
                    {
                        CargarDatosAlCombo();
                    }
                }

            }
        }
    }
}
