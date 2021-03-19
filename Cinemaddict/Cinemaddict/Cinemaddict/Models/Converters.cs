using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Cinemaddict.Models
{
    public class SubscribeColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int localId = ((parameter as Button).CommandParameter as Tuple<int, int>).Item2;
            return ((ObservableCollection<Color>)value)[localId];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Color.Default;
        }
    }

    public class LikeImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var LikeInfo = ((parameter as ImageButton).CommandParameter as Tuple<int, Tuple<int, List<int>>>)?.Item2;
            var LikeIds = LikeInfo?.Item2;
            if (LikeIds == null)
            {
                return "DisLike.png";
            }    
            if(LikeIds.Contains(Preferences.Get("Id", 0)))
            {
                return "Like.png";
            }
            else
            {
                return "DisLike.png";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "DisLike.png";
        }
    }

    public class LikeLabelConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var LikeInfo = ((parameter as ImageButton).CommandParameter as Tuple<int, Tuple<int, List<int>>>)?.Item2;
            var LikeCount = LikeInfo?.Item1;
            if (LikeCount == null)
            {
                return "Нравится : 0";
            }
            else
            {
                return $"Нравится : {LikeCount}";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "DisLike.png";
        }
    }

    public class SubscribeTitleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int localId = ((parameter as Button).CommandParameter as Tuple<int, int>).Item2;
            if (((ObservableCollection<Color>)value)[localId] == Color.Blue)
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
