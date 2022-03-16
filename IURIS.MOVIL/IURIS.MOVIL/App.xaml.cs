using IURIS.DAL;
using IURIS.MOVIL.Modelos_y_clases;
using IURIS.MOVIL.Modelos_y_clases.DB_Local;
using IURIS.MOVIL.Modelos_y_clases.Utils;
using IURIS.MOVIL.Views.Baners;
using IURIS.MOVIL.Views.ViewPayPal;
using Matcha.BackgroundService;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

        [assembly: ExportFont("lucida_handwriting.ttf")]
namespace IURIS.MOVIL
{
    public partial class App : Application
    {
        public static MasterDetailPage masterDetail { get; set; }

        public static MyUser MyUser { get; set; }

        static Database database;
        static LocalDataUltimoUser UltimoUser;
        static SaveMyClasificacionDeLey SaveMyClasificacionDeLey;

        public static Database Database
        {
            get
            {
                if (database == null)
                {
                    database = new Database(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MyUserData.Iuris"));
                }
                return database;
            }
        }

        public static LocalDataUltimoUser ultimoUser
        {
            get
            {
                if (UltimoUser == null)
                {
                    UltimoUser = new LocalDataUltimoUser(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MyUserData.Iuris"));
                }
                return UltimoUser;
            }
        }

        public static SaveMyClasificacionDeLey MyClasificacionDeLey
        {
            get
            {
                if (SaveMyClasificacionDeLey == null)
                {
                    SaveMyClasificacionDeLey = new SaveMyClasificacionDeLey(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MyUserData.Iuris"));
                }
                return SaveMyClasificacionDeLey;
            }
        }

        public App()
        {
            InitializeComponent();
            //Device.SetFlags(new[] { "RadioButton_Experimental" });
            //Device.SetFlags(new[] { "Expander_Experimental" });

            var pagina = new NavigationPage(new PagePrimeravista());
            pagina.BackgroundColor = (Color)App.Current.Resources["PrimaryColor"];
            pagina.BarBackgroundColor = (Color)App.Current.Resources["PrimaryColor"];
            MainPage = pagina;
        }

        protected override void OnStart()
        {
            //Register Periodic Tasks
            BackgroundAggregatorService.Add(() => new GetClasificaiciones(24));
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
            try
            {
            BackgroundAggregatorService.StartBackgroundService();

            }
            catch (Exception)
            {

                return;
            }
        }

        
    }

}
