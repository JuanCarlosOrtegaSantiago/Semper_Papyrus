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

        Leyes CopiaLey;
        public WindowEditarLey(Leyes Ley)
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized;
            CopiaLey = Ley;
            LblEditarLey.Content = string.Format("Editar ley {0}", Ley.NombreLey);
            txtCodigo.Text = Ley.CodigoLey;
            txtNombre.Text = Ley.NombreLey;

            ListTitulos.ItemsSource = Ley.ListaDeTitulos;

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
    }
}
