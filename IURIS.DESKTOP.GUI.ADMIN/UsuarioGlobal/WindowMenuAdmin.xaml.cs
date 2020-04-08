using IURIS.COMMON.Entidades.UsuarioGlobal;
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
    /// Lógica de interacción para WindowMenuAdmin.xaml
    /// </summary>
    public partial class WindowMenuAdmin : Window
    {
        UsuarioGlobal UsuarioGlobal;
        public WindowMenuAdmin(UsuarioGlobal usuarioGlobal)
        {
            InitializeComponent();
            UsuarioGlobal = usuarioGlobal;
        }

        private void BtnSalir_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("¿Esta seguro de regresar?", "Advertencia", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
            {
            WindowOperaciones windowOperaciones = new WindowOperaciones();
            this.Close();
            windowOperaciones.Show();
            }
        }

        private void BtnUsuarios_Click(object sender, RoutedEventArgs e)
        {
            WindowEleccionDeUsuarios windowEleccionDeUsuarios= new WindowEleccionDeUsuarios(UsuarioGlobal);
            this.Close();
            windowEleccionDeUsuarios.Show();
        }

        private void BtnContraseniaGenerica_Click(object sender, RoutedEventArgs e)
        {
            WindowCambiarContrasenia windowCambiarContrasenia = new WindowCambiarContrasenia(UsuarioGlobal);
            this.Close();
            windowCambiarContrasenia.Show();
        }

        private void BtnEditarInformacion_Click(object sender, RoutedEventArgs e)
        {
            WindowEditarDatosDeAdmin windowEditarDatosDeAdmin = new WindowEditarDatosDeAdmin(UsuarioGlobal);
            this.Close();
            windowEditarDatosDeAdmin.Show();
        }
    }
}
