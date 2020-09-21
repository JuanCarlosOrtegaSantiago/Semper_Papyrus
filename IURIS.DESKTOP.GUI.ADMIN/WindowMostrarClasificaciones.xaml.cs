using IURIS.BIZ;
using IURIS.COMMON.Entidades.Ley.ClasificacionDeLey;
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
    /// Lógica de interacción para WindowMostrarClasificaciones.xaml
    /// </summary>
    public partial class WindowMostrarClasificaciones : Window
    {
        IManejadorDeClasificaciones manejadorDeClasificaciones;
        static TimeoutException ExSinInternet = new TimeoutException();
        public bool HayInternet = true;
        static bool _EditarLey;
        static bool _VenderLey;

        public WindowMostrarClasificaciones(bool EditarLey, bool VenderLey)
        {
            InitializeComponent();
            manejadorDeClasificaciones = new ManejadorDeClasificaciones(new RepositorioGenerico<Clasificacion>());

            _EditarLey = EditarLey;
            _VenderLey = VenderLey;

            try
            {
            ListaDeClasificaciones.ItemsSource = null;
            ListaDeClasificaciones.ItemsSource = manejadorDeClasificaciones.Listar;

            }
            catch (Exception ex)
            {

                if (ex.HResult == ExSinInternet.HResult)
                {
                    HayInternet = false;
                    return;
                }
            }
        }

        private void ListaDeClasificaciones_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Clasificacion clasificacion = (Clasificacion)ListaDeClasificaciones.SelectedItem;
            if (clasificacion != null)
            {
                WindowMostrarListaDeLeyes windowMostrarListaDeLeyes = new WindowMostrarListaDeLeyes(clasificacion, _EditarLey, _VenderLey);
                this.Close();
                windowMostrarListaDeLeyes.Show();
            }
        }

        private void BtnRegresarAMenuDeOperaciones_Click(object sender, RoutedEventArgs e)
        {
            WindowOperaciones windowOperaciones = new WindowOperaciones();
            this.Close();
            windowOperaciones.Show();
        }
    }
}
