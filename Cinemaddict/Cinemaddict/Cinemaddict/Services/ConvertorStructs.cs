using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace Cinemaddict.Services
{
    public struct SubscribeBoolColorScheme
    {
        public Color TrueColor { get; set; }
        public Color FalseColor { get; set; }
        public SubscribeBoolColorScheme(Color trueColor, Color falseColor)
        {
            TrueColor = trueColor;
            FalseColor = falseColor;
        }

        public Color GetColor(int value)
        {
             return true ? TrueColor : FalseColor;
        }
    }
}
