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
    /// Lógica de interacción para WindowUsuarios.xaml
    /// </summary>
    public partial class WindowUsuarios : Window
    {
        public WindowUsuarios()
        {
            InitializeComponent();
            Editable(false);
            cmbxUsuarios.Visibility = Visibility.Collapsed;
        }

        private void Editable(bool v)
        {
            txtContrasenia.IsEnabled = v;
            txtCorreo.IsEnabled = v;
            txtDireccion.IsEnabled = v;
            TxtNombre.IsEnabled = v;
            TxtRepetirContrasenia.IsEnabled = v;
            BtnEditar.IsEnabled = !v;
            BtnEliminar.IsEnabled = !v;
            BtnGuardar.IsEnabled = v;
            BtnCancelar.IsEnabled = v;
            BtnNuevo.IsEnabled = !v;
            BtnRegresar.IsEnabled = !v;
        }

        private void BtnRegresar_Click(object sender, RoutedEventArgs e)
        {
            WindowOperaciones windowOperaciones = new WindowOperaciones();
            this.Close();
            windowOperaciones.Show();
        }

        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
            Editable(true);

            cmbxUsuarios.Visibility = Visibility.Visible;
        }

        private void BtnNuevo_Click(object sender, RoutedEventArgs e)
        {
            Editable(true);

        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            Editable(true);

        }
    }
}
