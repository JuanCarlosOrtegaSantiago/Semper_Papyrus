using IURIS.MOVIL.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IURIS.MOVIL.Master
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewMaster : ContentPage
    {
        public ViewMaster()
        {
            InitializeComponent();
        }

        private async void Primera_Clicked(object sender, EventArgs e)
        {
            App.masterDetail.IsPresented = false;
            await App.masterDetail.Detail.Navigation.PushAsync(new Page1());
        }

        private async void Segundo_Clicked(object sender, EventArgs e)
        {
            App.masterDetail.IsPresented = false;
            await App.masterDetail.Detail.Navigation.PushAsync(new Page1());

        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            //Timer timer = new Timer(5000000);
            StkClasificacionPersonalizada.BackgroundColor= (Color)App.Current.Resources["PrimaryColor"];
            LblClasificacionPersonalizada.TextColor = Color.White;
            //timer.Start();

            //int secs = 0;
            //// fire an event every 1000 ms
            //Timer timer = new Timer(1000);
            //// when event fires, update Label
            ////timer.Elapsed += (sender, e) => { secs++; myLabel.Text = $"{secs} seconds"; };
            //// start the timer
            //timer.Start();

            //Device.StartTimer(TimeSpan.FromSeconds(300), () =>
            //{
            //    Do something
            App.masterDetail.IsPresented = false;
            //    return true; // True = Repeat again, False = Stop the timer
            //});
            ////TimeSpan timeSpan = new TimeSpan(0, 0, 1, 50, 50);
            //Device.StartTimer(2000, () => { DisplayAlert("Alert", "This fired after 2 seconds", "ok"); return true; });


            StkClasificacionPersonalizada.BackgroundColor = Color.White;
            LblClasificacionPersonalizada.TextColor = Color.Black;
            await App.masterDetail.Detail.Navigation.PushAsync(new Page1());
        }
    }
}