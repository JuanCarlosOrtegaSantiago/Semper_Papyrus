using IURIS.COMMON.Entidades.Ley.ComponentesDeLey;
using Microsoft.Win32;
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
                lblNombreDeComponente.Content = "Título";
                txtNombre.Text = _titulo.NombreTitulo;
                txtNumero.Text = _titulo.NumTitulo;
                GridFotos.Visibility = Visibility.Collapsed;
                this.Height = 350;
            }
            if (capitulo)
            {
                lblNombreDeComponente.Content = "Capítulo";
                txtNombre.Text = _capitulo.NombreCapitulo;
                txtNumero.Text = _capitulo.NumCapitulo;
                GridFotos.Visibility = Visibility.Collapsed;
                this.Height = 350;
            }

            if (articulo)
            {

                lblContenido.Visibility = Visibility.Visible;
                RtcTxtContenido.Visibility = Visibility.Visible;
                lblNombreDeComponente.Content = "Artiículo";
                txtNombre.Text = _articulo.NombreArticulo;
                txtNumero.Text = _articulo.NumArticulo;
                if (_articulo.FotoAdjunta)
                    ActualizarListaFotos();
                else
                    GridFotos.Visibility = Visibility.Collapsed;


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
            GridFotos.IsEnabled = v;

            LimpiarDatosFoto();
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
                    _articulo.Contenido = Contenido(RtcTxtContenido);
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
            this.Close();
        }

        private string Contenido(RichTextBox caja)
        {
            return new TextRange(caja.Document.ContentStart, caja.Document.ContentEnd).Text;
        }

        private void LimpiarCajaDeContenido(RichTextBox caja)
        {
            _ = new TextRange(caja.Document.ContentStart, caja.Document.ContentEnd)
            {
                Text = ""
            };
        }

        public byte[] ImageToByte(ImageSource image)
        {
            if (image == null)
                return null;

                MemoryStream memoryStream = new MemoryStream();
                JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(image as BitmapSource));
                encoder.Save(memoryStream);
                return memoryStream.ToArray();
        }
         
        private void listFotos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listFotos.SelectedItem == null)
                return;

            Fotografia fotografia = listFotos.SelectedItem as Fotografia;

            WindowEditarFoto windowEditarFoto = new WindowEditarFoto(fotografia);
            windowEditarFoto.ShowDialog();

            ActualizarListaFotos();
        }

        private void ActualizarListaFotos()
        {
            listFotos.ItemsSource = null;
            listFotos.ItemsSource = _articulo.Fotografia;
        }

        private void btnAddPhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Selecciona la fotografia";
            dialog.Filter = "Formato de imagen|*.jpg; *.png";
            if (dialog.ShowDialog().Value)
            {
                Img.Source = new BitmapImage(new Uri(dialog.FileName));
            }
        }

        private void BtnAddImg_Click(object sender, RoutedEventArgs e)
        {

            if (Contenido(RtcTxtDescripcionIMG) == "")
                return;

            Fotografia fotografia = new Fotografia()
            {
                Descripcion = Contenido(RtcTxtDescripcionIMG),
                Foto = ImageToByte(Img.Source)
            };

            _articulo.Fotografia.Add(fotografia);

            ActualizarListaFotos();

            LimpiarDatosFoto();
        }

        private void LimpiarDatosFoto()
        {
            LimpiarCajaDeContenido(RtcTxtDescripcionIMG);
            BitmapImage bi3 = new BitmapImage();
            bi3.BeginInit();
            bi3.UriSource = new Uri("Imagenes/camera.png", UriKind.Relative);
            bi3.EndInit();
            Img.Source = bi3;
        }
    }
}
