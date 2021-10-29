using IURIS.MOVIL.Modelos_y_clases.Utils;
using IURIS.MOVIL.Views.Baners;
using IURIS.MOVIL.Views.ViewPayPal;
using Matcha.BackgroundService;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

        [assembly: ExportFont("lucida_handwriting.ttf")]
namespace IURIS.MOVIL
{
    public partial class App : Application
    {
        public static MasterDetailPage masterDetail { get; set; }
        public App()
        {
            InitializeComponent();
            //Device.SetFlags(new[] { "RadioButton_Experimental" });
            //Device.SetFlags(new[] { "Expander_Experimental" });

            var pagina = new NavigationPage(new MainPage());
            pagina.BackgroundColor = (Color)App.Current.Resources["PrimaryColor"];
            pagina.BarBackgroundColor = (Color)App.Current.Resources["PrimaryColor"];
            MainPage = pagina;
        }

        protected override void OnStart()
        {
            //Register Periodic Tasks
            BackgroundAggregatorService.Add(() => new BackGroundService(3));
            //BackgroundAggregatorService.Add(() => new PeriodicCall2(4));

            //Start the background service
            BackgroundAggregatorService.StartBackgroundService();
        }

        protected override void OnSleep()
        {
            BackgroundAggregatorService.StopBackgroundService();
        }

        protected override void OnResume()
        {
            BackgroundAggregatorService.StartBackgroundService();
        }
    }
}
