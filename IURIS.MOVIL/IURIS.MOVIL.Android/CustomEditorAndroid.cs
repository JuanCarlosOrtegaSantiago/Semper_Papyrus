using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Views;
using Android.Widget;
using IURIS.MOVIL.Droid;
using IURIS.MOVIL.Modelos_y_clases;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomEditor),typeof(CustomEditorAndroid))]

namespace IURIS.MOVIL.Droid
{
    public class CustomEditorAndroid : EditorRenderer
    {
        public CustomEditorAndroid(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                GradientDrawable gd = new GradientDrawable();
                gd.SetColor(global::Android.Graphics.Color.Transparent);
                Control.SetBackgroundDrawable(gd);

                //Control.JustificationMode = JustificationMode.InterWord;
            }
        }

    }
}