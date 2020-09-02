using IURIS.COMMON.Entidades.UsuariosDeAplicacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IURIS.MOVIL.Views.ViewsCargarLey
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewCargarLey : ContentPage
    {
        Usuarios _User;
        public ViewCargarLey(Usuarios usuarios)
        {
            InitializeComponent();
            _User = usuarios;

            clltionLeyes.ItemsSource = null;
            clltionLeyes.ItemsSource = _User.MisLeyes;
        }
    }
}