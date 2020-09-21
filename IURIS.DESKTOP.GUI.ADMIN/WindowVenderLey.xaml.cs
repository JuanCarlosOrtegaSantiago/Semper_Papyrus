using IURIS.BIZ;
using IURIS.COMMON.Entidades.Ley;
using IURIS.COMMON.Entidades.Ley.ComponentesDeLey;
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
    /// Lógica de interacción para WindowVenderLey.xaml
    /// </summary>
    public partial class WindowVenderLey : Window
    {
        Leyes _Ley;
        IManejadorDeLeyes manejadorDeLeyes;
        public WindowVenderLey(Leyes ley)
        {
            InitializeComponent();
            _Ley = ley;

            manejadorDeLeyes = new ManejadorDeLeyes(new RepositorioGenerico<Leyes>());

            CargarDatos();
            
        }

        private void CargarDatos()
        {
            DTGCodigoDeVentas.ItemsSource = null;
            DTGCodigoDeVentas.ItemsSource = _Ley.CodigosDeVentas;
        }

        private void EstadoBotones(bool v)
        {
            BtnBorrar.IsEnabled = !v;
            BtnNuevaVenta.IsEnabled = v;
            BtnRegresar.IsEnabled = !v;
            txtCodigo.Clear();
        }

        private void BtnNuevaVenta_Click(object sender, RoutedEventArgs e)
        {
            DTGCodigoDeVentas.Visibility = Visibility.Collapsed;
            WrpVenderLey.Visibility = Visibility.Visible;
            EstadoBotones(true);
        }

        private void BtnGuardarCodigo_Click(object sender, RoutedEventArgs e)
        {

            bool ExisteCodigo = false;

            CodigoVenta codigoVenta = new CodigoVenta()
            {
                CodigoDeVenta = txtCodigo.Text
            };

            foreach (var Ley in manejadorDeLeyes.Listar)
            {
                foreach (var Codigo in Ley.CodigosDeVentas)
                {
                    if (Codigo.CodigoDeVenta == codigoVenta.CodigoDeVenta)
                    {
                        ExisteCodigo = true;
                        break;
                    }
                    if (ExisteCodigo)
                        break;
                }
            }

            if (!ExisteCodigo)
            {

                _Ley.CodigosDeVentas.Add(codigoVenta);

                if (manejadorDeLeyes.Modificar(_Ley))
                {

                    DTGCodigoDeVentas.Visibility = Visibility.Visible;
                    WrpVenderLey.Visibility = Visibility.Collapsed;

                    EstadoBotones(false);
                    BtnNuevaVenta.IsEnabled = true;
                    MessageBox.Show("Codigo guardado", "Venta correcta", MessageBoxButton.OK);
                    CargarDatos();
                }
                else
                {
                    MessageBox.Show("Intenta mas tarde", "Error", MessageBoxButton.OK);
                }
            }
            else
            {
                MessageBox.Show("El codigo ingresado ya existe", "Error", MessageBoxButton.OK);
            }


        }

        private void BtnBorrar_Click(object sender, RoutedEventArgs e)
        {
            CodigoVenta codigoVenta = (CodigoVenta)DTGCodigoDeVentas.SelectedItem;
            {
                if (MessageBox.Show("¿Decea eliminar este codigo?", "Advertencia", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No) == MessageBoxResult.Yes)
                {
                    _Ley.CodigosDeVentas.Remove(codigoVenta);
                    if (manejadorDeLeyes.Modificar(_Ley))
                    {
                        MessageBox.Show("El codigo se elimino", "Correcto", MessageBoxButton.OK);
                        CargarDatos();
                    }
                    else
                    {
                        MessageBox.Show("No se elimino el codigo intente mas tarde", "Error", MessageBoxButton.OK);
                    }

                }
                else
                {
                    MessageBox.Show("Intenta mas tarde", "Error", MessageBoxButton.OK);
                }
            }
        }

        private void BtnRegresar_Click(object sender, RoutedEventArgs e)
        {
            WindowOperaciones windowOperaciones = new WindowOperaciones();
            this.Close();
            windowOperaciones.Show();
        }
    }
}
