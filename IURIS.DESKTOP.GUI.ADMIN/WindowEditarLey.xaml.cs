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

        Titulo _titulo=null;
        Capitulo _capitulo = null;
        Articulo _articulo=null;

        public WindowEditarLey(Leyes Ley)
        {
            InitializeComponent();

            this.WindowState = WindowState.Maximized;
            CopiaLey = Ley;
            LblEditarLey.Content = string.Format("Editar ley {0}", CopiaLey.NombreLey);
            txtCodigo.Text = CopiaLey.CodigoLey;
            txtNombre.Text = CopiaLey.NombreLey;

            ListTitulos.ItemsSource = CopiaLey.ListaDeTitulos;

            //foreach (ListView item in ListTitulos.SelectedItems)
            //{
            //    item.SelectedItem = false;
            //}

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
            _articulo = null;
            _capitulo = null;
            _titulo = null;


            List<Titulo> titulos = CopiaLey.ListaDeTitulos as List<Titulo>;


            if (Titulo && !string.IsNullOrWhiteSpace(txtNumeroTitulo.Text))
            {


                _titulo = titulos.Find(i => i.NumTitulo == txtNumeroTitulo.Text) as Titulo;
                if (_titulo != null)
                {

                    WindowModificarDatos windowModificarDatos = new WindowModificarDatos(_titulo,_capitulo,_articulo,Titulo,Capitulo,Articulo);
                    windowModificarDatos.ShowDialog();
                }
                else
                {

                    MessageBox.Show("El titulo no fue encontrado\nrevisar numero introducido", "Titulo", MessageBoxButton.OK, MessageBoxImage.Error);
                }




            }

            else if (Capitulo && !string.IsNullOrWhiteSpace(txtNumeroTitulo.Text) && !string.IsNullOrWhiteSpace(txtNumeroCaputilo.Text))
            {

                _titulo = titulos.Find(i => i.NumTitulo == txtNumeroTitulo.Text) as Titulo;
                if (_titulo != null)
                {

                    List<Capitulo> capitulos = _titulo.ListaCapitulos as List<Capitulo>;

                    _capitulo = capitulos.Find(i => i.NumCapitulo == txtNumeroCaputilo.Text) as Capitulo;
                    if (_capitulo != null)
                    {
                        WindowModificarDatos windowModificarDatos = new WindowModificarDatos(_titulo, _capitulo, _articulo, Titulo, Capitulo, Articulo);
                        windowModificarDatos.ShowDialog();

                    }
                    else
                    {

                        MessageBox.Show("El capitulo no fue encontrado\nrevisar numero introducido", "Capitulo", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }
                else
                {

                    MessageBox.Show("El titulo no fue encontrado\nrevisar numero introducido", "Titulo", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }

            else if (Articulo && !string.IsNullOrWhiteSpace(txtNumeroTitulo.Text) && !string.IsNullOrWhiteSpace(txtNumeroCaputilo.Text) && !string.IsNullOrWhiteSpace(txtNumeroArtitulo.Text))
            {
                _titulo = titulos.Find(i => i.NumTitulo == txtNumeroTitulo.Text) as Titulo;
                if (_titulo != null)
                {

                    List<Capitulo> capitulos = _titulo.ListaCapitulos as List<Capitulo>;

                    _capitulo = capitulos.Find(i => i.NumCapitulo == txtNumeroCaputilo.Text) as Capitulo;
                    if (_capitulo != null)
                    {
                        List<Articulo> articulos = _capitulo.ListaArticulos as List<Articulo>;

                        _articulo = articulos.Find(i => i.NumArticulo == txtNumeroArtitulo.Text) as Articulo;
                        if (_articulo != null)
                        {
                            WindowModificarDatos windowModificarDatos = new WindowModificarDatos(_titulo, _capitulo, _articulo, Titulo, Capitulo, Articulo);
                            windowModificarDatos.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show("El articulo no fue encontrado\nrevisar numero introducido", "Articulo", MessageBoxButton.OK, MessageBoxImage.Error);

                        }

                    }
                    else
                    {

                        MessageBox.Show("El capitulo no fue encontrado\nrevisar numero introducido", "Capitulo", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }
                else
                {

                    MessageBox.Show("El titulo no fue encontrado\nrevisar numero introducido", "Titulo", MessageBoxButton.OK, MessageBoxImage.Error);
                }





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
