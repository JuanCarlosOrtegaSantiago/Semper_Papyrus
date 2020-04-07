using IURIS.BIZ;
using IURIS.COMMON.Entidades.ContraseniaDeAccesoUnico;
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
        IManejadorDeUsuarioGlobal manejadorDeUsuarioGlobal;

        ContraseniaUnica contrasenia;

        public MainWindow()
        {
            InitializeComponent();
            manejadorDeUsuarioAdministrador = new ManejadorDeUsuariosAdministrador(new RepositorioGenerico<UsuarioAdministrador>());
            manejadorDeContraseniaUnica = new ManejadorDeContraseniaUnica(new RepositorioGenerico<ContraseniaUnica>());
            manejadorDeUsuarioGlobal = new ManejadorDeUsuarioGlobal(new RepositorioGenerico<UsuarioGlobal>());
            IniciandoPrograma();
            CosasAInicializarConUsuarios();
        }

        private void CosasAInicializarConUsuarios()
        {
            CmbxUsuario.ItemsSource = null;
            CmbxUsuario.ItemsSource = manejadorDeUsuarioAdministrador.Listar;
        }

        private void IniciandoPrograma()
        {

            try
            {

                if (manejadorDeUsuarioGlobal.Listar.Count == 0)
                {

                    UsuarioGlobal usuario1 = new UsuarioGlobal()
                    {
                        Contrasenia = "Admin",
                        NombreCompleto = "Admin"
                    };

                    manejadorDeUsuarioGlobal.AGREGAR(usuario1);

                }

                if (manejadorDeUsuarioAdministrador.Listar.Count == 0)
                {
                    UsuarioAdministrador usuarioAdministrador = new UsuarioAdministrador()
                    {
                        NombreCompleto = "User1"
                    };

                    manejadorDeUsuarioAdministrador.AGREGAR(usuarioAdministrador);
                    CosasAInicializarConUsuarios();
                }

                if (manejadorDeContraseniaUnica.Listar.Count == 0)
                {

                    ContraseniaUnica contraseniaUnica = new ContraseniaUnica()
                    {
                        Password = "Admin.Semper_Papyrus"
                    };

                    if (manejadorDeContraseniaUnica.AGREGAR(contraseniaUnica))
                        contrasenia = manejadorDeContraseniaUnica.Listar.SingleOrDefault();

                }
                else
                {
                    contrasenia = manejadorDeContraseniaUnica.Listar.SingleOrDefault();
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show("Error al inicar error:\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Hand);

            }

        }

        private void BtnSalir_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("¿Está seguro de salir?","Salir",MessageBoxButton.YesNo,MessageBoxImage.Question,MessageBoxResult.No)==MessageBoxResult.Yes)
                this.Close();
        }

        private void BtnEntrar_Click(object sender, RoutedEventArgs e)
        {

            AccionEntrar();

        }

        void AccionEntrar()
        {

            //if (CmbxUsuario.SelectedItem == null)
            //{
            //    MessageBox.Show("Aún no has seleccionado tu usuario", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            //}
            //else
            //{
            //    if (PswrDeUsuario.Password == contrasenia.Password)
            //    {
            WindowOperaciones windowOperaciones = new WindowOperaciones();
            this.Close();
            windowOperaciones.Show();
            //    }
            //    else
            //    {
            //        LblErrorDeContrasenia.Visibility = Visibility.Visible;
            //    }
            //}
        }

        private void PswrDeUsuario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) 
            {
                LblErrorDeContrasenia.Visibility = Visibility.Collapsed;

            }
            if (e.Key == Key.Enter)
            {
                AccionEntrar();

            }
        }
        
    }
}
