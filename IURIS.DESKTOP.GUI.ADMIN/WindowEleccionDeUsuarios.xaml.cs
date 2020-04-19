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
    /// Lógica de interacción para WindowEleccionDeUsuarios.xaml
    /// </summary>
    public partial class WindowEleccionDeUsuarios : Window
    {
        UsuarioGlobal UsuarioGlobal;
        public WindowEleccionDeUsuarios(UsuarioGlobal usuarioGlobal)
        {
            InitializeComponent();
            UsuarioGlobal = usuarioGlobal;
        }

        private void BtnAceptar_Click(object sender, RoutedEventArgs e)
        {
            if (RdBtnUsuarioAdmin.IsChecked == false && RdBtnUsuarioGlobal.IsChecked == false)
            {
                MessageBox.Show("Aun no has seleccionado ningun tipo de usuario", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (RdBtnUsuarioGlobal.IsChecked == true)
                {
                    WindowUsuariosGlobal windowUsuariosGlobal = new WindowUsuariosGlobal(UsuarioGlobal);
                    this.Close();
                    windowUsuariosGlobal.Show();
                }
                else
                {
                    WindowUsuarios windowUsuarios = new WindowUsuarios(UsuarioGlobal);
                    this.Close();
                    windowUsuarios.Show();
                }
            }
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
    }
}
