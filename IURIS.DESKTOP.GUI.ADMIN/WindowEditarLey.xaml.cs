using IURIS.BIZ;
using IURIS.COMMON.Entidades.Ley;
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
    /// Lógica de interacción para WindowEditarLey.xaml
    /// </summary>
    public partial class WindowEditarLey : Window
    {

        IManejadorDeLeyes manejadorDeLeyes;

        public WindowEditarLey()
        {
            InitializeComponent();
            manejadorDeLeyes = new ManejadorDeLeyes(new RepositorioGenerico<Leyes>());

            ListLeyes.ItemsSource = manejadorDeLeyes.Listar;
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            string str = null;
            str = "CSharp TOP 10 BOOKS";
            if (str.Contains("TOP 2") == true)
            {
                MessageBox.Show("The string Contains() 'TOP' ");
            }
            else
            {
                MessageBox.Show("no");
            }
        }

        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            ListLeyes.ItemsSource = manejadorDeLeyes.BuscarEnLeyes(txtbuscar.Text);
        }
    }
}
