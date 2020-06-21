using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IURIS.MOVIL
{
    public partial class App : Application
    {
        public static MasterDetailPage masterDetail { get; set; }
        public App()
        {
            InitializeComponent();

            var pagina = new NavigationPage(new MainPage());
            pagina.BackgroundColor = (Color)App.Current.Resources["PrimaryColor"];
            pagina.BarBackgroundColor = (Color)App.Current.Resources["PrimaryColor"];
            MainPage = pagina;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
