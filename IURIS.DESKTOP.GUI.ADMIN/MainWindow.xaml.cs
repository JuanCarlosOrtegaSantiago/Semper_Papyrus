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
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnEntrar_Click(object sender, RoutedEventArgs e)
        {
            //validar que el usuario ingresado exista asi mismo que la contraseña ingresada corresponda al usuario 
            WindowOperaciones windowOperaciones = new WindowOperaciones();
            this.Close();
            windowOperaciones.Show();
        }
    }
}
