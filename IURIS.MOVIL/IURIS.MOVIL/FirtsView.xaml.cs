using IURIS.COMMON.Entidades.UsuariosDeAplicacion;
using IURIS.MOVIL.Detail;
using IURIS.MOVIL.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IURIS.MOVIL
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FirtsView : MasterDetailPage
    {
        static Usuarios usuario;
        public FirtsView(Usuarios usuarios)
        {
            InitializeComponent();
            usuario = usuarios;

            this.Master = new ViewMaster(usuario);
            this.Detail = new NavigationPage(new ViewDetail(usuario));
            //Detail = new NavigationPage(new LoginRegister() { BarBackgroundColor = Color.LimeGreen, BarTextColor = Color.White });

            App.masterDetail = this;
        }
    }
}