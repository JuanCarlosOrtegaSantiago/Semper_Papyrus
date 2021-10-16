using IURIS.MOVIL.Modelos_y_clases;
using Xamarin.Forms;

namespace XamarinMarctronAdsApp.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public Command LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
        }

        private async void OnLoginClicked(object obj)
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
          //  await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
        }
    }
}
