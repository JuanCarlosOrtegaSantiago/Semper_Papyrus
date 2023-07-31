using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace IURIS.MOVIL.Modelos_y_clases
{
    public class CFonts
    {
        public double Subtitle
        {
            get
            {
                return Device.GetNamedSize(NamedSize.Subtitle, typeof(Label));
            }
        }

        public double Body
        {
            get
            {
                return Device.GetNamedSize(NamedSize.Body, typeof(Label));
            }
        }

        public double Title
        {
            get
            {
                return Device.GetNamedSize(NamedSize.Title, typeof(Label));
            }
        }

        public double Large
        {
            get
            {
                return Device.GetNamedSize(NamedSize.Large, typeof(Label));
            }
        }

        public double Small
        {
            get
            {
                return Device.GetNamedSize(NamedSize.Small, typeof(Label));
            }
        }

        public double Medium
        {
            get
            {
                return Device.GetNamedSize(NamedSize.Medium, typeof(Label));
            }
        }
    }
}
