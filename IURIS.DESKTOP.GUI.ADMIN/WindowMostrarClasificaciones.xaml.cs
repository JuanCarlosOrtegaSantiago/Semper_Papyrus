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

        public WindowMostrarClasificaciones()
        {
            InitializeComponent();
            manejadorDeClasificaciones = new ManejadorDeClasificaciones(new RepositorioGenerico<Clasificacion>());

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
                WindowMostrarListaDeLeyes windowMostrarListaDeLeyes = new WindowMostrarListaDeLeyes(clasificacion);
                this.Close();
                windowMostrarListaDeLeyes.Show();
            }
        }
    }
}
