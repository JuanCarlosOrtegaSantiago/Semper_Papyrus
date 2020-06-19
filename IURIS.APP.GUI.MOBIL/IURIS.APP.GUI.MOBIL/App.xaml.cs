using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using IURIS.APP.GUI.MOBIL.Services;
using IURIS.APP.GUI.MOBIL.Views;

namespace IURIS.APP.GUI.MOBIL
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
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
