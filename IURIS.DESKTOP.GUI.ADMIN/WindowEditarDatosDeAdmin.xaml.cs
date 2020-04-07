using IURIS.BIZ;
using IURIS.COMMON.Entidades.UsuarioGlobal;
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
    /// Lógica de interacción para WindowEditarDatosDeAdmin.xaml
    /// </summary>
    public partial class WindowEditarDatosDeAdmin : Window
    {
        IManejadorDeUsuarioGlobal manejadorDeUsuarioGloblal;
        UsuarioGlobal UsuarioGlobal;

        public WindowEditarDatosDeAdmin(UsuarioGlobal usuarioGlobal)
        {
            InitializeComponent();
            UsuarioGlobal = usuarioGlobal;
            manejadorDeUsuarioGloblal = new ManejadorDeUsuarioGlobal(new RepositorioGenerico<UsuarioGlobal>());
            DatosAInicializar();
        }

        private void DatosAInicializar()
        {

            txtContrasenia.Text = UsuarioGlobal.Contrasenia;
            txtCorreo.Text = UsuarioGlobal.Correo;
            TxtNombre.Text = UsuarioGlobal.NombreCompleto;
            EsEditable(false);
        }

        private void EsEditable(bool v)
        {
            txtContrasenia.IsEnabled=v;
            txtCorreo.IsEnabled = v;
            TxtNombre.IsEnabled = v;
            BtnEditar.IsEnabled = !v;
            BtnCancelar.IsEnabled = v;
            BtnGuardar.IsEnabled = v;
            BtnRegresar.IsEnabled = !v;
        }

        private void BtnRegresar_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("¿Esta seguro de regresar?", "Advertencia", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
            {
                WindowMenuAdmin windowMenuAdmin = new WindowMenuAdmin(UsuarioGlobal);
                this.Close();
                windowMenuAdmin.Show();
            }
        }

        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
            EsEditable(true);
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            DatosAInicializar();
        }

        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            UsuarioGlobal.Contrasenia = txtContrasenia.Text;
            UsuarioGlobal.Correo = txtCorreo.Text;
            UsuarioGlobal.NombreCompleto = TxtNombre.Text;

            if (manejadorDeUsuarioGloblal.Modificar(UsuarioGlobal))
            {
                MessageBox.Show("Tus datos han sido actualizados", "Informe", MessageBoxButton.OK, MessageBoxImage.Information);
                DatosAInicializar();
            }
            else
            {
                MessageBox.Show("Error al guardar tus datos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
