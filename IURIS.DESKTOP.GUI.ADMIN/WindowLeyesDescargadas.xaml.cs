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

using LiveCharts;
using LiveCharts.Wpf;
using System.Net;
using System.IO;
using System.Windows.Threading;
using LiveCharts.Defaults;
using LiveCharts.Configurations;
using IURIS.COMMON.Interfaces;
using IURIS.DAL;
using IURIS.COMMON.Entidades.Ley;
using IURIS.BIZ;

namespace IURIS.DESKTOP.GUI.ADMIN
{
    /// <summary>
    /// Lógica de interacción para WindowLeyesDescargadas.xaml
    /// </summary>
    public partial class WindowLeyesDescargadas : Window
    {

        IManejadorDeLeyes manejadorDeLeyes;

        Leyes LeyMasDescargada = null;
        int TotalDeDescargas=0;


        public WindowLeyesDescargadas()
        {
            InitializeComponent();
            manejadorDeLeyes = new ManejadorDeLeyes(new RepositorioGenerico<Leyes>());
            //LeyMasDescargada = manejadorDeLeyes.Listar.Where(e => e.EsModificacion==false).SingleOrDefault();
            LeyMasDescargada = manejadorDeLeyes.Listar.Where(e => e.EsModificacion==false).FirstOrDefault();

            if (manejadorDeLeyes.Listar.Count <= 0)
                if (MessageBox.Show("Aun no tiene leyes agregadas", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning) == MessageBoxResult.OK)
                    this.Close();
            DatosAInicializar();

            ListLeyes.ItemsSource = manejadorDeLeyes.MostrarLeyes;

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(5);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        private void DatosAInicializar()
        {
            ListLeyes.ItemsSource = null;
            ListLeyes.ItemsSource = manejadorDeLeyes.MostrarLeyes;

            foreach (var item in manejadorDeLeyes.Listar)
            {
                if (item.numDescargas > LeyMasDescargada.numDescargas)
                    LeyMasDescargada = item;

                TotalDeDescargas += item.numDescargas;
            }
        }

        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            ListLeyes.ItemsSource = manejadorDeLeyes.BuscarEnLeyes(txtbuscar.Text);
        }

        private void BtnRegresarAMenuDeOperaciones_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("¿Realmente decea salir?", "Advertencia", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No) == MessageBoxResult.Yes)
            {
                WindowOperaciones windowOperaciones = new WindowOperaciones();
                this.Close();
                windowOperaciones.Show();

            }
        }

        private void BtnCambiarDeAlturaMinimizar_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
                BtnCambiarDeAlturaMaximisar.Visibility = Visibility.Visible;
                BtnCambiarDeAlturaMinimizar.Visibility = Visibility.Collapsed;
            }

        }

        private void BtnCambiarDeAlturaMaximisar_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
            {
                this.WindowState = WindowState.Maximized;
                BtnCambiarDeAlturaMaximisar.Visibility = Visibility.Collapsed;
                BtnCambiarDeAlturaMinimizar.Visibility = Visibility.Visible;
            }
        }

        private void BtnEditarLey_Click(object sender, RoutedEventArgs e)
        {
            if (ListLeyes.SelectedItem != null)
            {
                Leyes ley = ListLeyes.SelectedItem as Leyes;
                WindowEditarLey windowEditarLey = new WindowEditarLey(ley);
                this.Close();
                windowEditarLey.Show();
            }
        }


        void timer_Tick(object sender, EventArgs e)
        {
            //try
            //{
            //    //String Str = "https://api.thingspeak.com/channels/";
            //    //String End = "/feeds.json?results=1";
            //    //String Mid = "308573";  //Channel ID Go's Here - Must be set to public

            //    //String All1 = String.Join(Mid, Str, End);


            //    ////WebRequest request = WebRequest.Create(All1);
            //    ////HttpWebResponse responce = (HttpWebResponse)request.GetResponse();
            //    ////Stream datastream = responce.GetResponseStream();
            //    ////StreamReader reader = new StreamReader(datastream);
            //    ////strData = reader.ReadToEnd();
            //}
            //catch (Exception)
            //{


            //}
            

            try
            {
                LblGraficaNombreDeLEy.Content = LeyMasDescargada.NombreLey;
                GaugeIOT.Value= LeyMasDescargada.numDescargas+3;
                GaugeIOT.To = TotalDeDescargas+3;
                GaugeIOT.From = 0;

                //GaugeIOT.Value = manejadorDeLeyes.Listar.Where(i=>i.numDescargas!=0).LongCount();
                //GaugeIOT.From = 0;
                //GaugeIOT.To = manejadorDeLeyes.Listar.Count;
            }
            catch (Exception)
            {

            }


        }

        private void ListLeyes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListLeyes.SelectedItem != null)
            {
                Leyes leyes = ListLeyes.SelectedItem as Leyes;

                LblGraficaNombreDeLEy.Content = leyes.NombreLey;
                GaugeIOT.Value = leyes.numDescargas+2;
                GaugeIOT.To = TotalDeDescargas+3;

            }
        }
    }
}
