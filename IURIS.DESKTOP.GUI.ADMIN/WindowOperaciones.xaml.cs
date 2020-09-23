using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
        private bool EditarLey = false;
        private bool VenderLey = false;
        public WindowOperaciones( )
        {
            InitializeComponent();

            //try
            //{
            //    string mail = "salut";
            //    MailMessage msg = new MailMessage();
            //    msg.From = new MailAddress("tecprogjuancarlos@gmail.com");
            //    msg.To.Add(new MailAddress("juankxsantix@gmail.com"));
            //    msg.Body = mail;
            //    SmtpClient client = new SmtpClient("smtp.googl.com",587);
            //    client.EnableSsl = false;
            //    client.Timeout = 10000;
            //    //client.Port = 587;
            //    client.Credentials = new NetworkCredential("tecprogjuancarlos@gmail.com", "juankx99stx");
            //    client.Send(msg);

            //    MessageBox.Show("Successfully Message Sent!");
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Message not sent because of:" + ex.Message);
            //}

        }

        private void BtnSubirNuevaLey_Click(object sender, RoutedEventArgs e)
        {
            //EsNuevaLey = true;
            WindowOperacionesDeLeyes windowOperacionesDeLeyes = new WindowOperacionesDeLeyes();
            //WindowOperacionesDeLeyes windowOperacionesDeLeyes = new WindowOperacionesDeLeyes(EsNuevaLey);
            this.Close();
            windowOperacionesDeLeyes.Show();
        }

        private void BtnModificarLey_Click(object sender, RoutedEventArgs e)
        {
            EditarLey = true;
            WindowMostrarClasificaciones windowMostrarClasificaciones= new  WindowMostrarClasificaciones();
            //WindowOperacionesDeLeyes windowOperacionesDeLeyes = new WindowOperacionesDeLeyes(EsNuevaLey);
            if (windowMostrarClasificaciones.HayInternet)
            {
                this.Close();
                windowMostrarClasificaciones.Show();
            }
            else
            {
                MessageBox.Show("Revisa tu conexion a internet", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BtnSalir_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("¿Desa salir de la apliación?", "Advertencia", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No) == MessageBoxResult.Yes)
            
                this.Close();
            
        }

        private void BtnAdministrador_Click(object sender, RoutedEventArgs e)
        {
            windowAccesoRestringido windowAccesoRestringido = new windowAccesoRestringido();

            windowAccesoRestringido.ShowDialog();

            if (windowAccesoRestringido.Entro)
                this.Close();
        }

        private void BtnLeyesDescargadas_Click(object sender, RoutedEventArgs e)
        {
            WindowLeyesDescargadas windowLeyesDescargadas = new WindowLeyesDescargadas();
            this.Close();
            windowLeyesDescargadas.Show();
        }

        private void BtnClasificaciones_Click(object sender, RoutedEventArgs e)
        {
            WindowClasificacion windowClasificacion = new WindowClasificacion();
            this.Close();
            windowClasificacion.Show();
        }

        //private void BtnNuevaVenta_Click(object sender, RoutedEventArgs e)
        //{
        //    VenderLey = true;
        //    WindowMostrarClasificaciones windowMostrarClasificaciones = new WindowMostrarClasificaciones(EditarLey, VenderLey);
        //    //WindowOperacionesDeLeyes windowOperacionesDeLeyes = new WindowOperacionesDeLeyes(EsNuevaLey);
        //    if (windowMostrarClasificaciones.HayInternet)
        //    {
        //        this.Close();
        //        windowMostrarClasificaciones.Show();
        //    }
        //    else
        //    {
        //        MessageBox.Show("Revisa tu conexion a internet", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
        //    }
        //}
    }
}
