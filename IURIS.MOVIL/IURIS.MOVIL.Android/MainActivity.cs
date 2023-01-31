using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Forms;
using Acr.UserDialogs;
using PayPal.Forms.Abstractions;
using PayPal.Forms;
using Android.Content;
using Matcha.BackgroundService.Droid;

namespace IURIS.MOVIL.Droid
{
    [Activity(Label = "IURIS", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            BackgroundAggregator.Init(this);

            base.OnCreate(savedInstanceState);

            UserDialogs.Init(this);

            Rg.Plugins.Popup.Popup.Init(this);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            //Forms.SetFlags(new[] { "Expander_Experimental" });
            Forms.SetFlags(new[] { "RadioButton_Experimental", "Expander_Experimental", "SwipeView_Experimental", "IndicatorView_Experimental", "Shapes_Experimental" });

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());


            var config = new PayPalConfiguration(PayPalEnvironment.NoNetwork, "AauEBKZSRHuvOo2gyEjSwxgxCKR06xI5vbt3ARQaFmyAjrPUaxyPrUXl29RX9QNRXcN0_sUTE-kF4xfT")
            {
                //If you want to accept credit cards
                AcceptCreditCards = true,
                //Your business name
                MerchantName = "Test Store",
                MerchantPrivacyPolicyUri = "https://www.example.com/privacy",
                MerchantUserAgreementUri = "https://www.example.com/legal",
                // OPTIONAL - ShippingAddressOption (Both, None, PayPal, Provided)
                //ShippingAddressOption = ShippingAddressOption.Both,
                // OPTIONAL - Language: Default languege for PayPal Plug-In
                Language = "es",
                // OPTIONAL - PhoneCountryCode: Default phone country code for PayPal Plug-In
                PhoneCountryCode = "52",
            };
            try
            {

            CrossPayPalManager.Init(config, this);
            }
            catch (Exception)
            {
            }
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            PayPalManagerImplementation.Manager.OnActivityResult(requestCode, resultCode, data);
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
            PayPalManagerImplementation.Manager.Destroy();
        }
    }
}