using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using IURIS.MOVIL.Droid;
using IURIS.MOVIL.Modelos_y_clases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomLabeJustifity), typeof(CustomLabelJust))]
namespace IURIS.MOVIL.Droid
{
    public class CustomLabelJust:LabelRenderer
    {
        public CustomLabelJust(Context context):base(context)
        {

        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
        }
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.SetTextIsSelectable(true);
                Control.Selected = true;
            }
        }
    }
}