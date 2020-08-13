using IURIS.BIZ;
using IURIS.COMMON.Entidades.Ley;
using IURIS.COMMON.Entidades.Ley.ClasificacionDeLey;
using IURIS.COMMON.Entidades.UsuarioGlobal;
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
    /// Lógica de interacción para WindowClasificacion.xaml
    /// </summary>
    public partial class WindowClasificacion : Window
    {
        readonly IManejadorDeClasificaciones ManejadorDeClasificaciones;
        readonly IManejadorDeLeyes manejadorDeLeyes;
        bool EsEditar = false;
        public Clasificacion clasificacion = null;
        public bool NoHayClasificacion_Agregar = false;
        public bool SeAgregoUnaClasificacion = false;


        public WindowClasificacion()
        {
            InitializeComponent();

            //UsuarioGlobal = usuarioGlobal;
            ManejadorDeClasificaciones = new ManejadorDeClasificaciones(new RepositorioGenerico<Clasificacion>());
            manejadorDeLeyes = new ManejadorDeLeyes(new RepositorioGenerico<Leyes>());

            CargarDatos();
            CamposHabilitados(false);
            LimpiarCampos();
        }

        private void CamposHabilitados(bool v)
        {
            TxtNombreClasificacion.IsEnabled = v;

            BtnCancelar.IsEnabled = v;
            BtnEliminar.IsEnabled = !v;
            BtnGuardar.IsEnabled = v;
            BtnNuevo.IsEnabled = !v;
            BtnRegresar.IsEnabled = !v;
        }

        private void LimpiarCampos()
        {
            TxtNombreClasificacion.Clear();
        }

        private void CargarDatos()
        {
            DTGClasificaciones.ItemsSource = null;
            DTGClasificaciones.ItemsSource = ManejadorDeClasificaciones.Listar;
        }

        private void BtnNuevo_Click(object sender, RoutedEventArgs e)
        {
            CamposHabilitados(true);
            LimpiarCampos();
            WrpAgregarNuevaClasificacion.Visibility = Visibility.Visible;
            DTGClasificaciones.Visibility = Visibility.Collapsed;
        }

        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (!EsEditar)
            {

                if (!string.IsNullOrWhiteSpace(TxtNombreClasificacion.Text))
                {
                    Clasificacion clasificacion = new Clasificacion()
                    {
                        Nombre = TxtNombreClasificacion.Text
                    };
                    if (ManejadorDeClasificaciones.AGREGAR(clasificacion))
                    {
                        MessageBox.Show("La clasificación se agrego correctamente", "", MessageBoxButton.OK, MessageBoxImage.Information);
                        CamposHabilitados(false);
                        LimpiarCampos();
                        CargarDatos();
                        WrpAgregarNuevaClasificacion.Visibility = Visibility.Collapsed;
                        DTGClasificaciones.Visibility = Visibility.Visible;
                        if (NoHayClasificacion_Agregar)
                            SeAgregoUnaClasificacion = true;
                    }
                    else
                    {
                        MessageBox.Show("No se pudo agregar la nueva clasificación ", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                    }
                }
                else
                {
                    MessageBox.Show("Faltan datos por llenar", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(TxtNombreClasificacion.Text))
                {
                    clasificacion.Nombre = TxtNombreClasificacion.Text;
                    if (ManejadorDeClasificaciones.Modificar(clasificacion))
                    {
                        MessageBox.Show("La clasificación se modifico correctamente", "", MessageBoxButton.OK, MessageBoxImage.Information);
                        CamposHabilitados(false);
                        LimpiarCampos();
                        CargarDatos();
                        WrpAgregarNuevaClasificacion.Visibility = Visibility.Collapsed;
                        DTGClasificaciones.Visibility = Visibility.Visible;

                        int leyes = 0;
                        while (leyes <= manejadorDeLeyes.Listar.Count())
                        {

                            foreach (var item in manejadorDeLeyes.Listar)
                            {
                                leyes++;
                                if (item.Clasificacion.id == clasificacion.id)
                                {
                                    item.Clasificacion = clasificacion;
                                    manejadorDeLeyes.Modificar(item);
                                }
                            }
                        }
                        clasificacion = null;

                    }
                    else
                    {
                        MessageBox.Show("No se pudo modificar la clasificación ", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                    }
                }
                else
                {
                    MessageBox.Show("Faltan datos por llenar", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            CamposHabilitados(false);
            WrpAgregarNuevaClasificacion.Visibility = Visibility.Collapsed;
            DTGClasificaciones.Visibility = Visibility.Visible;
        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            Clasificacion clasificacion = (Clasificacion)DTGClasificaciones.SelectedItem;

            if (clasificacion != null)
            {
                if (MessageBox.Show("Realmente decea eliminar " + clasificacion.Nombre, "", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No) == MessageBoxResult.Yes)
                {
                    if (manejadorDeLeyes.Listar.Where(w => w.Clasificacion.Nombre == clasificacion.Nombre).Count() >= 1)
                    {
                        MessageBox.Show("No se puede eliminar la clasificacion\nya que tiene leyes con esta clasificación", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);

                    }
                    else
                    {
                        if (ManejadorDeClasificaciones.Eliminar(clasificacion.id))
                        {
                            MessageBox.Show("La clasificación se elimino correctamente", "", MessageBoxButton.OK, MessageBoxImage.Information);
                            CargarDatos();
                            CamposHabilitados(false);
                            WrpAgregarNuevaClasificacion.Visibility = Visibility.Collapsed;
                            DTGClasificaciones.Visibility = Visibility.Visible;
                        }

                    }

                }
            }
            else
            {
                MessageBox.Show("No has seleccionado ningun elemento", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void BtnRegresar_Click(object sender, RoutedEventArgs e)
        {
            if (NoHayClasificacion_Agregar)
            {
                this.Close();
            }
            else
            {
                if (MessageBox.Show("¿Esta seguro de regresar?", "Advertencia", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
                {
                    WindowOperaciones windowOperaciones = new WindowOperaciones();
                    this.Close();
                    windowOperaciones.Show();
                }

            }
        }

        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
            clasificacion = (Clasificacion)DTGClasificaciones.SelectedItem;

            if (clasificacion != null)
            {
                CamposHabilitados(true);

                WrpAgregarNuevaClasificacion.Visibility = Visibility.Visible;
                DTGClasificaciones.Visibility = Visibility.Collapsed;

                TxtNombreClasificacion.Text = clasificacion.Nombre;

                EsEditar = true;
            }
            else
            {
                MessageBox.Show("No has seleccionado ningun elemento", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }
    }
}
