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
    public partial class WindowAccesoRestringido : Window
    {
        IManejadorDeUsuarioGlobal manejadorDeUsuarioGloblal;

        public WindowAccesoRestringido()
        {
            InitializeComponent();
            manejadorDeUsuarioGloblal = new ManejadorDeUsuarioGloblal(new RepositorioGenerico<UsuarioGlobal>());
        }

        private void CosasAInicializar()
        {
            CmbxUsuarioAdmin.ItemsSource = null;
            CmbxUsuarioAdmin.ItemsSource = manejadorDeUsuarioGloblal.Listar;
        }


        private void IniciandoPrograma()
        {
            if (manejadorDeUsuarioGloblal.Listar.Count == 0)
            {

                UsuarioGlobal usuario1 = new UsuarioGlobal()
                {
                    Contrasenia = "Admin",
                    NombreCompleto = "Admin"
                };
                if (manejadorDeUsuarioGloblal.AGREGAR(usuario1))
                {
                    CosasAInicializar();
                }


            }
            else
            {
            }
        }

        private void BtnSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnEntrar_Click(object sender, RoutedEventArgs e)
        {

            //if (CmbxUsuario.SelectedItem == null)
            //{
            //    MessageBox.Show("Aun No has seleccionado tu usuario", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            //}
            //else
            //{
            //    if (PswrDeUsuario.Password == contraseniaUnica.Password)
            //    {
            //        WindowOperaciones windowOperaciones = new WindowOperaciones();
            //        this.Close();
            //        windowOperaciones.Show();
            //    }
            //    else
            //    {
            //        LblErrorDeContrasenia.Visibility = Visibility.Visible;
            //    }
            //}
        }

        private void PswrDeUsuario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.IsDown)
                LblErrorDeContraseniaAdmin.Visibility = Visibility.Collapsed;
        }
    }
}
