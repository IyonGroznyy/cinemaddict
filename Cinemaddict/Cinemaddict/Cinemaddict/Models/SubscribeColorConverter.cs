using Cinemaddict.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Cinemaddict.Models
{
    public class SubscribeColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((ObservableCollection<Color>)value)[(int)(parameter as Button).CommandParameter];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Color.Default;
        }
    }

    public class SubscribeTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(((ObservableCollection<Color>)value)[(int)(parameter as Button).CommandParameter] == Color.Blue)
            {
                return "Subcribe";
            }
            else
            {
                return "Unsubcribe";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "Subcribe";
        }
    }
}
