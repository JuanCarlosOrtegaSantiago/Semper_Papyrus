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

[assembly: ExportRenderer(typeof(CustomLabeJustifity), typeof(CusomLabelJustifity))]
namespace IURIS.MOVIL.Droid
{

    public class CusomLabelJustifity:LabelRenderer
    {
        public CusomLabelJustifity(Context context) : base(context)
        {
            //Control.JustificationMode = JustificationMode.InterWord;

        }
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.JustificationMode = JustificationMode.InterWord;

            }
        }
    }
}