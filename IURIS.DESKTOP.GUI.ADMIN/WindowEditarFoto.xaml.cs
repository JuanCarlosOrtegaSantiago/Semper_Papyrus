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
    /// Lógica de interacción para WindowEditarFoto.xaml
    /// </summary>
    public partial class WindowEditarFoto : Window
    {
        Fotografia _fotografia;
        public WindowEditarFoto( Fotografia  fotografia)
        {
            InitializeComponent();

            _fotografia = fotografia;


            _ = new TextRange(RtcTxtDescripcionIMG.Document.ContentStart, RtcTxtDescripcionIMG.Document.ContentEnd)
            {
                Text = fotografia.Descripcion
            };
            //RtcTxtDescripcionIMG  fotografia.Descripcion;
            lblIMG.Visibility = Visibility.Collapsed;
            Img.Source = ByteToImagen(fotografia.Foto);
        }

        private ImageSource ByteToImagen(byte[] imageData)
        {
            if (imageData == null)
            {
                return null;
            }
            else
            {
                BitmapImage bitimg = new BitmapImage();
                MemoryStream las = new MemoryStream(imageData);
                bitimg.BeginInit();
                bitimg.StreamSource = las;
                bitimg.EndInit();
                ImageSource imgSrc = bitimg as ImageSource;
                return imgSrc;
            }
        }

        private string Contenido(RichTextBox caja)
        {
            return new TextRange(caja.Document.ContentStart, caja.Document.ContentEnd).Text;
        }

        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            _fotografia.Descripcion = Contenido(RtcTxtDescripcionIMG);
            _fotografia.Foto = ImageToByte(Img.Source);
            MessageBox.Show("Cambios guardados", "Atención", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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

        public byte[] ImageToByte(ImageSource image)
        {
            if (image != null)
            {
                MemoryStream memoryStream = new MemoryStream();
                JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(image as BitmapSource));
                encoder.Save(memoryStream);
                return memoryStream.ToArray();
            }
            else
            {
                return null;
            }
        }

    }
}
