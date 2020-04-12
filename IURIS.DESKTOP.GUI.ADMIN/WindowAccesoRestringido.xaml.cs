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
    /// Lógica de interacción para WindowAccesoRestringido.xaml
    /// </summary>
    public partial class windowAccesoRestringido : Window
    {

        public bool Entro;
        IManejadorDeUsuarioGlobal manejadorDeUsuarioGloblal;

        public windowAccesoRestringido()
        {
            InitializeComponent();
            manejadorDeUsuarioGloblal = new ManejadorDeUsuarioGlobal(new RepositorioGenerico<UsuarioGlobal>());
            CosasAInicializar();
        }

        private void CosasAInicializar()
        {
            CmbxUsuarioAdmin.ItemsSource = null;
            CmbxUsuarioAdmin.ItemsSource = manejadorDeUsuarioGloblal.Listar;
        }

        private void BtnSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnEntrar_Click(object sender, RoutedEventArgs e)
        {
            AccionEntrar();
           
        }

        void AccionEntrar()
        {
            if (CmbxUsuarioAdmin.SelectedItem == null)
            {
                MessageBox.Show("Administrador no seleccionado", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {

                UsuarioGlobal usuarioGlobal = CmbxUsuarioAdmin.SelectedItem as UsuarioGlobal;

                if (usuarioGlobal.Contrasenia == PswrDeUsuarioAdmin.Password)
                {
                    Entro = true;
                    WindowMenuAdmin windowMenuAdmin = new WindowMenuAdmin(usuarioGlobal);
                    this.Close();
                    windowMenuAdmin.Show();
                }
                else
                {
                    Entro = false;
                    LblErrorDeContraseniaAdmin.Visibility = Visibility.Visible;
                }
            }
        }

        private void PswrDeUsuario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter)
            {
                LblErrorDeContraseniaAdmin.Visibility = Visibility.Collapsed;

            }
            if (e.Key == Key.Enter)
            {
                AccionEntrar();

            }
        }
        
    }
}
