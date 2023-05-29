using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Views;
using Android.Widget;
using IURIS.MOVIL.Droid;
using IURIS.MOVIL.Modelos_y_clases;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly:ExportRenderer(typeof(CustomLabel),typeof(CustomLabelAndriod))]
namespace IURIS.MOVIL.Droid
{
    public class CustomLabelAndriod:LabelRenderer
    {

        public CustomLabelAndriod(Context context):base(context)
        {
                //Control.JustificationMode = JustificationMode.InterWord;

        }
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.SetTextIsSelectable(true);
                Control.Selected = true;
                Control.SetAutoSizeTextTypeWithDefaults(AutoSizeTextType.Uniform);
                //Control.SetSelectAllOnFocus(true);

            }
        }
    }
}