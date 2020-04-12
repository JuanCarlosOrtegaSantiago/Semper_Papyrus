using IURIS.BIZ;
using IURIS.COMMON.Entidades.UsuarioGlobal;
using IURIS.COMMON.Entidades.UsuariosAdministrador;
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
    /// Lógica de interacción para WindowUsuarios.xaml
    /// </summary>
    public partial class WindowUsuarios : Window
    {
        enum AccionGuardar
        {
            Nuevo,
            Editado
        }
        AccionGuardar AccionDeGuardarUsuarioAdministrador;

        enum AccionSeleccionComboBox
        {
            Eliminar,
            Editar
        }
        AccionSeleccionComboBox SeleccionComboBox;

        IManejadorDeUsuarioAdministrador manejadorDeUsuarioAdministrador;
        UsuarioGlobal UsuarioGlobal;

        public WindowUsuarios(UsuarioGlobal usuarioGlobal)
        {
            InitializeComponent();
            UsuarioGlobal = usuarioGlobal;
            manejadorDeUsuarioAdministrador = new ManejadorDeUsuariosAdministrador(new RepositorioGenerico<UsuarioAdministrador>());
            EstadoInicial();
        }

        private void LimpiarCampos()
        {
            txtContrasenia.Clear();
            txtCorreo.Clear();
            txtDireccion.Clear();
            TxtNombre.Clear();
            TxtRepetirContrasenia.Clear();
        }

        private void TextosSonEditables(bool Y)
        {
            txtContrasenia.IsEnabled = Y;
            txtCorreo.IsEnabled = Y;
            txtDireccion.IsEnabled = Y;
            TxtNombre.IsEnabled = Y;
            TxtRepetirContrasenia.IsEnabled = Y;
        }

        private void BotonesSonEditables(bool X)
        {
            BtnEditar.IsEnabled = !X;
            BtnEliminar.IsEnabled = !X;
            BtnGuardar.IsEnabled = X;
            BtnCancelar.IsEnabled = X;
            BtnNuevo.IsEnabled = !X;
            BtnRegresar.IsEnabled = !X;
        }

        private void ElementosHabilitados(bool v)
        {
            BotonesSonEditables(v);
            TextosSonEditables(v);
        }

        private void CargarDarosEnComboBox()
        {
            cmbxUsuarios.ItemsSource = null;
            cmbxUsuarios.ItemsSource = manejadorDeUsuarioAdministrador.Listar;
        }

        private void EstadoInicial()
        {
            LimpiarCampos();
            ElementosHabilitados(false);
            ChkRevision.Visibility = Visibility.Visible;
            wraCmbx.Visibility = Visibility.Collapsed;
        }

        private void BtnRegresar_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("¿Esta seguro de regresar?", "Advertencia", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
            {
                WindowMenuAdmin windowMenuAdmin= new WindowMenuAdmin(UsuarioGlobal);
                this.Close();
                windowMenuAdmin.Show();
            }
        }

        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
            if (manejadorDeUsuarioAdministrador.Listar.Count <= 0)
            {
                MessageBox.Show("No puedes editar ningun usuario \n ya que no tienes agregado a ninguno", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                SeleccionComboBox = AccionSeleccionComboBox.Editar;
                ElementosHabilitados(true);
                AccionDeGuardarUsuarioAdministrador = AccionGuardar.Editado;
                wraCmbx.Visibility = Visibility.Visible;
                CargarDarosEnComboBox();

                
            }
        }

        private void BtnNuevo_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCampos();
            ElementosHabilitados(true);
            AccionDeGuardarUsuarioAdministrador = AccionGuardar.Nuevo;
        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (manejadorDeUsuarioAdministrador.Listar.Count <= 0)
            {
                MessageBox.Show("No puedes eliminar ningun usuario \n ya que no tienes agregado a ninguno", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                wraCmbx.Visibility = Visibility.Visible;
                ChkRevision.Visibility = Visibility.Collapsed;
                SeleccionComboBox = AccionSeleccionComboBox.Eliminar;
                CargarDarosEnComboBox();
                TextosSonEditables(false);
                BotonesSonEditables(true);
            }
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCampos();
            EstadoInicial();
        }

        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (!string.IsNullOrWhiteSpace(txtContrasenia.Text) && !string.IsNullOrWhiteSpace(txtCorreo.Text) && !string.IsNullOrWhiteSpace(txtDireccion.Text) && !string.IsNullOrWhiteSpace(TxtNombre.Text) && !string.IsNullOrWhiteSpace(TxtRepetirContrasenia.Text))
                {
                    if (AccionDeGuardarUsuarioAdministrador == AccionGuardar.Nuevo)
                    {
                        if (txtContrasenia.Text == TxtRepetirContrasenia.Text)
                        {
                            UsuarioAdministrador usuarioAdministrador = new UsuarioAdministrador()
                            {
                                Contrasenia = txtContrasenia.Text,
                                Correo = txtCorreo.Text,
                                Direccion = txtDireccion.Text,
                                NombreCompleto = TxtNombre.Text,
                            };
                            if (MessageBox.Show(string.Format("¿Realmente decea agregar el usuario: {0}?", TxtNombre.Text), "Operación", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes) == MessageBoxResult.Yes)
                            {

                                if (manejadorDeUsuarioAdministrador.AGREGAR(usuarioAdministrador))
                                
                                    MessageBox.Show("Usuario agregado correctamente", "Operación", MessageBoxButton.OK, MessageBoxImage.Information);

                                else

                                    MessageBox.Show("Error al agregar el usuario", "Operación", MessageBoxButton.OK, MessageBoxImage.Hand);
                                
                            }

                            ElementosHabilitados(false);
                            EstadoInicial();
                        }
                        else
                        {
                            MessageBox.Show("Las contraseñas no son iguales\nPor favor verificar", "Error", MessageBoxButton.OK, MessageBoxImage.Stop);

                        }


                    }
                    else
                    {
                        if (txtContrasenia.Text == TxtRepetirContrasenia.Text)
                        {
                            UsuarioAdministrador usuarioAdministradorEditado = cmbxUsuarios.SelectedItem as UsuarioAdministrador;

                            if (MessageBox.Show(string.Format("¿Realmente guardar los cambios \nal usuario: {0}?", usuarioAdministradorEditado.NombreCompleto), "Operación", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes) == MessageBoxResult.Yes)
                            {
                                usuarioAdministradorEditado.Contrasenia = txtContrasenia.Text;
                                usuarioAdministradorEditado.Correo = txtCorreo.Text;
                                usuarioAdministradorEditado.Direccion = txtDireccion.Text;
                                usuarioAdministradorEditado.NombreCompleto = TxtNombre.Text;

                                if (manejadorDeUsuarioAdministrador.Modificar(usuarioAdministradorEditado))

                                    MessageBox.Show("Usuario modificado correctamente", "Operación", MessageBoxButton.OK, MessageBoxImage.Information);

                                else

                                    MessageBox.Show("Error al modificar el usuario", "Operación", MessageBoxButton.OK, MessageBoxImage.Hand);

                            }
                            ElementosHabilitados(false);
                            EstadoInicial();
                        }
                        else
                        {
                            MessageBox.Show("Las contraseñas no son iguales\nPor favor verificar", "Error", MessageBoxButton.OK, MessageBoxImage.Stop);

                        }

                    }
                }
                else
                {
                    MessageBox.Show("Faltan datos Por llenar", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }

        private void CmbxUsuarios_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UsuarioAdministrador usuarioAdministrador = cmbxUsuarios.SelectedItem as UsuarioAdministrador;

            if (usuarioAdministrador != null)
            {

                if (SeleccionComboBox == AccionSeleccionComboBox.Editar)
                {
                    txtContrasenia.Text = usuarioAdministrador.Contrasenia;
                    txtCorreo.Text = usuarioAdministrador.Correo;
                    txtDireccion.Text = usuarioAdministrador.Direccion;
                    TxtNombre.Text = usuarioAdministrador.NombreCompleto;
                    TxtRepetirContrasenia.Text = usuarioAdministrador.Contrasenia;
                    
                    
                }
                else
                {

                    if (MessageBox.Show("Esta seguro de eliminar al usuario\n" + usuarioAdministrador.NombreCompleto, "Operación", MessageBoxButton.YesNo, MessageBoxImage.Information, MessageBoxResult.No) == MessageBoxResult.Yes)

                        try
                        {
                            if (manejadorDeUsuarioAdministrador.Eliminar(usuarioAdministrador.id))

                                MessageBox.Show("El usuario fue eliminado exitosamemte", "Operación", MessageBoxButton.OK, MessageBoxImage.Information);

                            else

                                MessageBox.Show("El usuario No pudo ser eliminado", "Operación", MessageBoxButton.OK, MessageBoxImage.Exclamation);

                        }
                        catch (Exception ex)
                        {

                            MessageBox.Show("Error: " + ex.Message, "Operación", MessageBoxButton.OK, MessageBoxImage.Error);
                        }

                    EstadoInicial();
                }

            }
        }

        private void ChkRevision_Click(object sender, RoutedEventArgs e)
        {
            if (ChkRevision.IsChecked == true)
            {
                TextosSonEditables(false);
                LimpiarCampos();
            }
            else
            {
                LimpiarCampos();
                ElementosHabilitados(true);
            }
        }
    }
}
