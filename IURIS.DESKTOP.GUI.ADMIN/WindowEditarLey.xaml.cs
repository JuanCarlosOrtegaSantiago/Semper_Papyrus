using IURIS.COMMON.Entidades.Ley;
using IURIS.COMMON.Entidades.Ley.ComponentesDeLey;
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
        bool Titulo;
        bool Capitulo;
        bool Articulo;

        Leyes CopiaLey;
        public WindowEditarLey(Leyes Ley)
        {
            InitializeComponent();

            this.WindowState = WindowState.Maximized;
            CopiaLey = Ley;
            LblEditarLey.Content = string.Format("Editar ley {0}", CopiaLey.NombreLey);
            txtCodigo.Text = CopiaLey.CodigoLey;
            txtNombre.Text = CopiaLey.NombreLey;

            ListTitulos.ItemsSource = CopiaLey.ListaDeTitulos;

            foreach (ListView item in ListTitulos.SelectedItems)
            {
                item.SelectedItem = false;
            }

        }

        private void LimpiarCajas()
        {
            txtNumeroArtitulo.Clear();
            txtNumeroCaputilo.Clear();
            txtNumeroTitulo.Clear();

            RdoBtnTitulo.IsChecked = false;
            RdoBtnCapitulo.IsChecked = false;
            RdoBtnArticulo.IsChecked = false;

        }


        private void BtnRegresarAMenuDeOperaciones_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("¿Realmente decea salir?", "Advertencia", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No) == MessageBoxResult.Yes)
            {
                WindowMostrarListaDeLeyes windowMostrarListaDeLeyes= new  WindowMostrarListaDeLeyes();
                this.Close();
                windowMostrarListaDeLeyes.Show();

            }
        }

        private void ListTitulos_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ListTitulos.SelectedItem != null)
            {

                List<Titulo> titulos = CopiaLey.ListaDeTitulos as List<Titulo>;

                var indice = titulos.IndexOf(ListTitulos.SelectedItem as Titulo);

                //titulos.RemoveAt(indice);


                //titulos.IndexOf(ListTitulos.SelectedItem as Titulo, indice);

                Titulo titulo = ListTitulos.SelectedItem as Titulo;

                List<Capitulo> capitulos = titulo.ListaCapitulos as List<Capitulo>;

                foreach (var item in capitulos)
                {
                    MessageBox.Show(item.NombreCapitulo);
                }

            }
        }

        private void BtnEditarLey_Click(object sender, RoutedEventArgs e)
        {
            CopiaLey.NombreLey = txtNombre.Text != CopiaLey.NombreLey ? txtNombre.Text : CopiaLey.NombreLey;
            CopiaLey.CodigoLey= txtCodigo.Text != CopiaLey.CodigoLey ? txtCodigo.Text : CopiaLey.CodigoLey;



        }

        private void RdoBtnTitulo_Click(object sender, RoutedEventArgs e)
        {
            WrpArticulo.Visibility = Visibility.Collapsed;
            WrpCapitulo.Visibility = Visibility.Collapsed;
            Titulo = true;
            Capitulo = false;
            Articulo = false;

        }

        private void RdoBtnCapitulo_Checked(object sender, RoutedEventArgs e)
        {
            WrpCapitulo.Visibility = Visibility.Visible;
            WrpArticulo.Visibility = Visibility.Collapsed;
            Titulo = false;
            Capitulo = true;
            Articulo = false;
        }

        private void RdoBtnArticulo_Checked(object sender, RoutedEventArgs e)
        {
            WrpArticulo.Visibility = Visibility.Visible;
            WrpCapitulo.Visibility = Visibility.Visible;
            Titulo = false;
            Capitulo = false;
            Articulo = true;
        }

        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {

                List<Titulo> titulos = CopiaLey.ListaDeTitulos as List<Titulo>;
            if (Titulo && !string.IsNullOrWhiteSpace(txtNumeroTitulo.Text)) {



                Titulo titulo = titulos.Find(i => i.NumTitulo == txtNumeroTitulo.Text) as Titulo;


                MessageBox.Show("nombre:" + titulo.NombreTitulo + "\nNumero:" + titulo.NumTitulo, "ñ", MessageBoxButton.OK, MessageBoxImage.Stop);


            }
            else if (Capitulo && !string.IsNullOrWhiteSpace(txtNumeroTitulo.Text) && !string.IsNullOrWhiteSpace(txtNumeroCaputilo.Text))

            {

                Titulo titulo = titulos.Find(i => i.NumTitulo == txtNumeroTitulo.Text) as Titulo;

                List<Capitulo> capitulos = titulo.ListaCapitulos as List<Capitulo>;

                Capitulo capitulo = capitulos.Find(i => i.NumCapitulo == txtNumeroCaputilo.Text) as Capitulo;

                MessageBox.Show("nombre:" + capitulo.NombreCapitulo + "\nNumero:" + capitulo.NumCapitulo, "ñ", MessageBoxButton.OK, MessageBoxImage.Stop);

            }
            else if (Articulo && !string.IsNullOrWhiteSpace(txtNumeroTitulo.Text) && !string.IsNullOrWhiteSpace(txtNumeroCaputilo.Text) && !string.IsNullOrWhiteSpace(txtNumeroArtitulo.Text))

            {
                Titulo titulo = titulos.Find(i => i.NumTitulo == txtNumeroTitulo.Text) as Titulo;

                List<Capitulo> capitulos = titulo.ListaCapitulos as List<Capitulo>;

                Capitulo capitulo = capitulos.Find(i => i.NumCapitulo == txtNumeroCaputilo.Text) as Capitulo;

                List<Articulo> articulos = capitulo.ListaArticulos as List<Articulo>;

                Articulo articulo = articulos.Find(i => i.NumArticulo == txtNumeroArtitulo.Text) as Articulo;


                MessageBox.Show("nombre:" + articulo.NombreArticulo+ "\nNumero:" + articulo.NumArticulo+"\nContenido:"+articulo.Contenido, "ñ", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            else
            {

                MessageBox.Show("Aun no has llenado todos los campos", "Error", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            wraMas.Visibility = Visibility.Visible;
            LimpiarCajas();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            wraMas.Visibility = Visibility.Collapsed;
        }
    }
}
