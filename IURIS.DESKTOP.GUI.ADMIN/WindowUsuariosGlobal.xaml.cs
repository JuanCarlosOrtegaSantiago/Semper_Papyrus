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
    /// Lógica de interacción para WindowUsuariosGlobal.xaml
    /// </summary>
    public partial class WindowUsuariosGlobal : Window
    {
        IManejadorDeUsuarioGlobal manejadorDeUsuarioGloblal;
        UsuarioGlobal UsuarioGlobal;

        public WindowUsuariosGlobal(UsuarioGlobal usuarioGlobal)
        {
            InitializeComponent();
            UsuarioGlobal = usuarioGlobal;
            manejadorDeUsuarioGloblal = new ManejadorDeUsuarioGlobal(new RepositorioGenerico<UsuarioGlobal>());


            CamposHabilitados(false);
            LimpiarCampos();
            CargarDatos();
        }

        private void CargarDatos()
        {
            DTGUsuariosGlobal.ItemsSource = null;
            DTGUsuariosGlobal.ItemsSource = manejadorDeUsuarioGloblal.Listar;
        }

        private void LimpiarCampos()
        {
            txtContrasenia.Clear();
            txtCorreo.Clear();
            TxtNombre.Clear();
            TxtRepetirContrasenia.Clear();

        }

        private void CamposHabilitados(bool v)
        {
            txtContrasenia.IsEnabled = v;
            txtCorreo.IsEnabled = v;
            TxtNombre.IsEnabled = v;
            TxtRepetirContrasenia.IsEnabled = v;

            BtnCancelar.IsEnabled = v;
            BtnEliminar.IsEnabled = !v;
            BtnGuardar.IsEnabled = v;
            BtnNuevo.IsEnabled = !v;
            BtnRegresar.IsEnabled = !v;
        }

        private void BtnNuevo_Click(object sender, RoutedEventArgs e)
        {
            CamposHabilitados(true);
            LimpiarCampos();
            WrpAgregarNuevo.Visibility = Visibility.Visible;
            DTGUsuariosGlobal.Visibility = Visibility.Collapsed;
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            CamposHabilitados(false);
            WrpAgregarNuevo.Visibility = Visibility.Collapsed;
            DTGUsuariosGlobal.Visibility = Visibility.Visible;
        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            WrpAgregarNuevo.Visibility = Visibility.Collapsed;
            DTGUsuariosGlobal.Visibility = Visibility.Visible;

            UsuarioGlobal usuario = DTGUsuariosGlobal.SelectedItem as UsuarioGlobal;
            if (usuario != null)
            {

            if(MessageBox.Show("Realmente decea eliminar a \n" + usuario.NombreCompleto, "", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No) == MessageBoxResult.Yes)
            {
                if (manejadorDeUsuarioGloblal.Eliminar(usuario.id))
                {
                    MessageBox.Show("El usuario se elimino correctamente", "", MessageBoxButton.OK, MessageBoxImage.Information);
                    CargarDatos();
                    CamposHabilitados(false);
                    WrpAgregarNuevo.Visibility = Visibility.Collapsed;
                    DTGUsuariosGlobal.Visibility = Visibility.Visible;
                }
            }
            }
            else
            {
                    MessageBox.Show("No has seleccionado ningun elemento", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                
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

        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(txtContrasenia.Text) && !string.IsNullOrWhiteSpace(txtCorreo.Text) && !string.IsNullOrWhiteSpace(TxtNombre.Text) && !string.IsNullOrWhiteSpace(TxtRepetirContrasenia.Text))
            {
                if (txtContrasenia.Text == TxtRepetirContrasenia.Text)
                {
                    UsuarioGlobal usuario = new UsuarioGlobal()
                    {
                        Contrasenia = txtContrasenia.Text,
                        Correo = txtCorreo.Text,
                        NombreCompleto = TxtNombre.Text
                    };
                    if (manejadorDeUsuarioGloblal.AGREGAR(usuario))
                    {
                        MessageBox.Show("El usuario se agrego correctamente", "", MessageBoxButton.OK, MessageBoxImage.Information);
                        CargarDatos();
                        CamposHabilitados(false);
                        WrpAgregarNuevo.Visibility = Visibility.Collapsed;
                        DTGUsuariosGlobal.Visibility = Visibility.Visible;
                    }
                    else
                    {

                        MessageBox.Show("No se pudo agregar el nuevo usuario", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {

                        MessageBox.Show("Las contraseñas no cionsiden\nPorfavor verifique de nuevo", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Faltan datos por llenar", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
