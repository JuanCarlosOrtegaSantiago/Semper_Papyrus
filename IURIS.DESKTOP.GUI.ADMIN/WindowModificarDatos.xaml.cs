using IURIS.COMMON.Entidades.Ley.ComponentesDeLey;
using Org.Apache.Http.Cookies;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Lógica de interacción para WindowModificarDatos.xaml
    /// </summary>
    public partial class WindowModificarDatos : Window
    {
        bool titulo;
        bool capitulo;
        bool articulo;
        public Titulo _titulo = null;
        public Capitulo _capitulo = null;
        public Articulo _articulo = null;


        public WindowModificarDatos(Titulo _titulo, Capitulo _capitulo,Articulo _articulo,bool titulo, bool capitulo, bool articulo)
        {
            InitializeComponent();

            this.titulo = titulo;
            this.capitulo = capitulo;
            this.articulo = articulo;

            this._titulo = _titulo;
            this._capitulo = _capitulo;
            this._articulo = _articulo;


            DatosAIniciar();


        }

        private void DatosAIniciar()
        {

            EditarCampos(false);
            if (titulo)
            {
                lblNombreDeComponente.Content = "Titulo";
                txtNombre.Text = _titulo.NombreTitulo;
                txtNumero.Text = _titulo.NumTitulo;
            }
            if (capitulo)
            {
                lblNombreDeComponente.Content = "Capitulo";
                txtNombre.Text = _capitulo.NombreCapitulo;
                txtNumero.Text = _capitulo.NumCapitulo;
            }

            if (articulo)
            {

                lblContenido.Visibility = Visibility.Visible;
                RtcTxtContenido.Visibility = Visibility.Visible;
                lblNombreDeComponente.Content = "Articulo";
                txtNombre.Text = _articulo.NombreArticulo;
                txtNumero.Text = _articulo.NumArticulo;

                    var SM = new MemoryStream(Encoding.UTF8.GetBytes(_articulo.Contenido));
                TextRange range;
                range = new TextRange(RtcTxtContenido.Document.ContentStart, RtcTxtContenido.Document.ContentEnd);
                range.Load(SM, System.Windows.DataFormats.Text);
                SM.Close();
            }
        }

        private void EditarCampos(bool v)
        {
            txtNombre.IsEnabled = v;
            RtcTxtContenido.IsEnabled = v;
            txtNumero.IsEnabled = v;
            BtnCancelar.IsEnabled = v;
            BtnEditar.IsEnabled = !v;
            BtnGuardar.IsEnabled = v;
        }

        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (titulo)
            {
                if (!string.IsNullOrWhiteSpace(txtNombre.Text) && !string.IsNullOrWhiteSpace(txtNumero.Text))
                {
                    _titulo.NombreTitulo = txtNombre.Text;
                    _titulo.NumTitulo = txtNumero.Text;
                }
                else
                {

                    MessageBox.Show("Faltan campos por llenar", "Error", MessageBoxButton.OK, MessageBoxImage.Stop);
                }
            }

            if (capitulo)
            {
                if (!string.IsNullOrWhiteSpace(txtNombre.Text) && !string.IsNullOrWhiteSpace(txtNumero.Text))
                {
                    _capitulo.NombreCapitulo = txtNombre.Text;
                    _capitulo.NumCapitulo = txtNumero.Text;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Faltan campos por llenar", "Error", MessageBoxButton.OK, MessageBoxImage.Stop);

                }
            }

            if (articulo)
            {

                if (!string.IsNullOrWhiteSpace(txtNombre.Text) && !string.IsNullOrWhiteSpace(txtNumero.Text) && RtcTxtContenido != null)
                {
                    _articulo.Contenido = Contenido();
                    _articulo.NombreArticulo = txtNombre.Text;
                    _articulo.NumArticulo = txtNumero.Text;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Faltan campos por llenar", "Error", MessageBoxButton.OK, MessageBoxImage.Stop);
                }

            }

            if (MessageBox.Show("¿Está seguro de guardar?", "Guardar", MessageBoxButton.OKCancel, MessageBoxImage.Warning, MessageBoxResult.Cancel) == MessageBoxResult.OK)
                this.Close();
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            DatosAIniciar();
        }

        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
            EditarCampos(true);
        }

        private void BtnRegresar_Click(object sender, RoutedEventArgs e)
        {
            //DatosAIniciar();
            this.Close();
        }

        private string Contenido()
        {
            //string richText;
            return new TextRange(RtcTxtContenido.Document.ContentStart, RtcTxtContenido.Document.ContentEnd).Text;
        }
    }
}
