using IURIS.BIZ;
using IURIS.COMMON.Entidades.Ley;
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

        //Leyes ley;
        IManejadorDeLeyes manejadorDeLeyes;

        

        public WindowOperacionesDeLeyes()
        {
            InitializeComponent();
            manejadorDeLeyes = new ManejadorDeLeyes(new RepositorioGenerico<Leyes>());
            LblOperacionARealizar.Content = "Nueva ley // editar ley";
            EstadoDeCajas(false);

        }

        private void EstadoDeCajas(bool v)
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
            //Hanilitar botones
            BtnAgregarArticulo.IsEnabled = v;
            BtnAgregarCapitulo.IsEnabled = v;
            BtnAgregarTitulo.IsEnabled = v;
            BtnBuscarCodgio.IsEnabled = v;
            BtnRegresarAMenuDeOperaciones.IsEnabled = !v;
            BtnNuevaLey.IsEnabled = !v;
            BtnCancelar.IsEnabled = v;
            BtnSubirLey.IsEnabled = v;


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
            WrpLblCodigoAsociado.Visibility = Visibility.Visible;
        }

        private void BtnNuevaLey_Click(object sender, RoutedEventArgs e)
        {
            EstadoDeCajas(true);
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
    }
}
