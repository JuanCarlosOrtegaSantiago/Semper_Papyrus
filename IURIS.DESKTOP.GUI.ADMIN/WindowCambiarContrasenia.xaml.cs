using IURIS.BIZ;
using IURIS.COMMON.Entidades.ContraseniaDeAccesoUnico;
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
    /// Lógica de interacción para WindowCambiarContrasenia.xaml
    /// </summary>
    public partial class WindowCambiarContrasenia : Window
    {

        ContraseniaUnica contraseniaUnica;

        IManejadorDeContraseniaUnica manejadorDeContraseniaUnica;
        public WindowCambiarContrasenia()
        {
            InitializeComponent();
            manejadorDeContraseniaUnica = new ManejadorDeContraseniaUnica(new RepositorioGenerico<ContraseniaUnica>());
            contraseniaUnica = manejadorDeContraseniaUnica.Listar.SingleOrDefault();
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            WindowOperaciones windowOperaciones = new WindowOperaciones();
            this.Close();
            windowOperaciones.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtConfirmarContraseniaNueva.Text) && !string.IsNullOrWhiteSpace(txtContraseniaNueva.Text))
            {
                if (txtContraseniaNueva.Text == txtConfirmarContraseniaNueva.Text)
                {
                    contraseniaUnica.Password = txtContraseniaNueva.Text;
                    try
                    {

                        if (manejadorDeContraseniaUnica.Modificar(contraseniaUnica))
                        {
                            if (MessageBox.Show("La contraseña se actualizo correctamente\n Por favor vuelve a iniciar sesion", "Operacion", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK) == MessageBoxResult.OK)
                            {
                                MainWindow mainWindow = new MainWindow();
                                this.Close();
                                mainWindow.Show();
                            }

                        }
                        else
                        {

                MessageBox.Show("Ocurrio un error al Intentar Cambiarla", "Error", MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK);

                        }

                    }
                    catch (Exception ex)
                    {

                MessageBox.Show("Error: "+ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK);
                    }
                }

            }
            else
            {
                MessageBox.Show("Faltan datos por llenar", "Error", MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK);
            }
                    
        }
    }
}
