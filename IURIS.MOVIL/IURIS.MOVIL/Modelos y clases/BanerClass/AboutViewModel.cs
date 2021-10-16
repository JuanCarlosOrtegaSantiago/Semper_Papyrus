using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;


namespace IURIS.MOVIL.Modelos_y_clases
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamarin-quickstart"));
        }

        public ICommand OpenWebCommand { get; }
    }
}
