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
    /// Lógica de interacción para WindowOperaciones.xaml
    /// </summary>
    public partial class WindowOperaciones : Window
    {
        private bool EsNuevaLey=false;
        public WindowOperaciones()
        {
            InitializeComponent();
        }

        private void BtnUsuarios_Click(object sender, RoutedEventArgs e)
        {
            WindowUsuarios windowUsuarios = new WindowUsuarios();
            this.Close();
            windowUsuarios.Show();
        }

        private void BtnSubirNuevaLey_Click(object sender, RoutedEventArgs e)
        {
            EsNuevaLey = true;
            WindowOperacionesDeLeyes windowOperacionesDeLeyes = new WindowOperacionesDeLeyes();
            //WindowOperacionesDeLeyes windowOperacionesDeLeyes = new WindowOperacionesDeLeyes(EsNuevaLey);
            this.Close();
            windowOperacionesDeLeyes.Show();
        }

        private void BtnModificarLey_Click(object sender, RoutedEventArgs e)
        {
            WindowOperacionesDeLeyes windowOperacionesDeLeyes = new WindowOperacionesDeLeyes();
            //WindowOperacionesDeLeyes windowOperacionesDeLeyes = new WindowOperacionesDeLeyes(EsNuevaLey);
            this.Close();
            windowOperacionesDeLeyes.Show();
        }
    }
}
