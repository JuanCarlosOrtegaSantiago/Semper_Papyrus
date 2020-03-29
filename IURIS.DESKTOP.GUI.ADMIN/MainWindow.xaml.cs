using IURIS.BIZ;
using IURIS.COMMON.Entidades.ContraseniaDeAccesoUnico;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IURIS.DESKTOP.GUI.ADMIN
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IManejadorDeUsuarioAdministrador manejadorDeUsuarioAdministrador;
        IManejadorDeContraseniaUnica manejadorDeContraseniaUnica;

        ContraseniaUnica contraseniaUnica;

        public MainWindow()
        {
            InitializeComponent();
            manejadorDeUsuarioAdministrador = new ManejadorDeUsuariosAdministrador(new RepositorioGenerico<UsuarioAdministrador>());
            manejadorDeContraseniaUnica = new ManejadorDeContraseniaUnica(new RepositorioGenerico<ContraseniaUnica>());

            IniciandoPrograma();
            cosasAInicializar();
        }

        private void cosasAInicializar()
        {
            CmbxUsuario.ItemsSource = null;
            CmbxUsuario.ItemsSource = manejadorDeUsuarioAdministrador.Listar;
        }


        private void IniciandoPrograma()
        {
            if (manejadorDeContraseniaUnica.Read.Count == 0)
            {
                ContraseniaUnica contraseniaUnica = new ContraseniaUnica()
                {
                    Password = "Admin.Semper_Papyrus"
                };
                if (!manejadorDeContraseniaUnica.Create(contraseniaUnica))
                {
                    this.Close();
                }
            }
            else
            {
                contraseniaUnica = manejadorDeContraseniaUnica.Read.SingleOrDefault();
            }
        }

        private void BtnSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnEntrar_Click(object sender, RoutedEventArgs e)
        {

            if (CmbxUsuario.SelectedItem == null)
            {
                MessageBox.Show("Aun No has seleccionado tu usuario", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                if (PswrDeUsuario.Password == contraseniaUnica.Password)
                {
                    WindowOperaciones windowOperaciones = new WindowOperaciones();
                    this.Close();
                    windowOperaciones.Show();
                }
                else
                {
                    LblErrorDeContrasenia.Visibility = Visibility.Visible;
                }
            }
        }

        private void PswrDeUsuario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.IsDown)
                LblErrorDeContrasenia.Visibility = Visibility.Collapsed;
        }
    }
}
