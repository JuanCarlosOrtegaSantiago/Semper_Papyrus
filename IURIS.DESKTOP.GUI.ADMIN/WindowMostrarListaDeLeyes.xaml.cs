using IURIS.BIZ;
using IURIS.COMMON.Entidades.Ley;
using IURIS.COMMON.Entidades.Ley.ClasificacionDeLey;
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
    /// Lógica de interacción para WindowMostrarListaDeLeyes.xaml
    /// </summary>
    public partial class WindowMostrarListaDeLeyes : Window
    {
        IManejadorDeLeyes manejadorDeLeyes;

        public WindowMostrarListaDeLeyes(Clasificacion clasificacion)
        {
            InitializeComponent();
            manejadorDeLeyes = new ManejadorDeLeyes(new RepositorioGenerico<Leyes>());

            if (manejadorDeLeyes.Listar.Count <= 0)
                if (MessageBox.Show("Aun no tiene leyes agregadas", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning) == MessageBoxResult.OK)
                    this.Close();

            ListLeyes.ItemsSource = manejadorDeLeyes.BuscarEnLeyesPorClasificacion(clasificacion);
        }

        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            ListLeyes.ItemsSource = manejadorDeLeyes.BuscarEnLeyes(txtbuscar.Text);
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

        private void BtnEditarLey_Click(object sender, RoutedEventArgs e)
        {
            if (ListLeyes.SelectedItem != null)
            {
                    Leyes ley = ListLeyes.SelectedItem as Leyes;
                    WindowEditarLey windowEditarLey = new WindowEditarLey(ley);
                    this.Close();
                    windowEditarLey.Show();
            }
        }
    }
}
