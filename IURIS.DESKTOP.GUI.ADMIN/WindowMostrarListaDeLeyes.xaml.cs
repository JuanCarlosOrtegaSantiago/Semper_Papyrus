using IURIS.BIZ;
using IURIS.COMMON.Entidades.Ley;
using IURIS.COMMON.Entidades.Ley.ClasificacionDeLey;
using IURIS.COMMON.Interfaces;
using IURIS.DAL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
//[System.Diagnostics.DebuggerBrowsable(DebuggerBrowsableState.Never)]
        IManejadorDeLeyes manejadorDeLeyes;
        List<Leyes> LeyesDeClasificacion;
        public WindowMostrarListaDeLeyes(Clasificacion clasificacion)
        {
            InitializeComponent();
            try
            {

            Background = (Brush)new BrushConverter().ConvertFrom(App.color);
            manejadorDeLeyes = new ManejadorDeLeyes(new RepositorioGenerico<Leyes>());

            List<Leyes> leyes = manejadorDeLeyes.Listar;

            if (leyes.Count <= 0)
                if (MessageBox.Show("Aun no tiene leyes agregadas", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning) == MessageBoxResult.OK)
                    this.Close();

            LeyesDeClasificacion = leyes.Where(w => w.Clasificacion == clasificacion.Nombre).ToList();
            ListLeyes.ItemsSource =  LeyesDeClasificacion;
            }
            catch (Exception ex)
            {

                //if (ex.HResult == ExSinInternet.HResult)
                //{
                //    HayInternet = false;
                //    return;
                //}
                MessageBox.Show(ex.Message, "Error");
            }

        }

        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            //ListLeyes.ItemsSource = manejadorDeLeyes.BuscarEnLeyes(txtbuscar.Text);
            ListLeyes.ItemsSource = LeyesDeClasificacion.Where(r => r.NombreLey.ToUpper().Contains(txtbuscar.Text.ToUpper()) == true || r.CodigoLey.ToUpper().Contains(txtbuscar.Text.ToUpper()) == true).OrderByDescending(w => w.UltimaFechaDeModificacion).ToList();
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
            if (ListLeyes.SelectedItem == null) return;

            WindowEditarLey windowEditarLey = new WindowEditarLey(ListLeyes.SelectedItem as Leyes);
            this.Close();
            windowEditarLey.Show();
        }
    }
}
