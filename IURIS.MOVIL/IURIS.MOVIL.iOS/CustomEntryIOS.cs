using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using IURIS.MOVIL.iOS;
using IURIS.MOVIL.Modelos_y_clases;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryIOS))]

namespace IURIS.MOVIL.iOS
{
    public class CustomEntryIOS:EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            //if (Control != null)
            //{
            //    GradientDrawable gd = new GradientDrawable();
            //    gd.SetColor(global::Android.Graphics.Color.Transparent);
            //    Control.SetBackgroundDrawable(gd);
            //}
        }
    }
}