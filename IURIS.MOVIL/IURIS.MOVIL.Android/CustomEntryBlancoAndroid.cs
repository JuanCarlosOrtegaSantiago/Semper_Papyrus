using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using IURIS.MOVIL.Droid;
using IURIS.MOVIL.Modelos_y_clases;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryAndroid))]

namespace IURIS.MOVIL.Droid
{
    public class CustomEntryBlancoAndroid : EntryRenderer
    {
        public CustomEntryBlancoAndroid(Context context) : base(context)
        {
        }
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                GradientDrawable gd = new GradientDrawable();
                gd.SetColor(global::Android.Graphics.Color.White);
                Control.SetBackgroundDrawable(gd);
            }

        }
    }
}