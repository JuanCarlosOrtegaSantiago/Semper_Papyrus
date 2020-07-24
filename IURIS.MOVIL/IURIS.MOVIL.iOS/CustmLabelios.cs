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

[assembly : ExportRenderer(typeof(CustomLabel),typeof(CustmLabelios) )]
namespace IURIS.MOVIL.iOS
{
    public class CustmLabelios:LabelRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            if (Control!=null)
            {
                Control.TextAlignment = UITextAlignment.Justified;
            }
        }
    }
}