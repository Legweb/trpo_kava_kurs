using System;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace Kava._2
{
    public class TimespanToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var ts = (TimeSpan?) value;
            if (!ts.HasValue)
            {
                return string.Empty;
            }

            var sb = new StringBuilder();
            if (ts.Value.Hours > 0)
            {
                sb.AppendFormat("{0} г.", ts.Value.Hours);
            }

            if (ts.Value.Minutes > 0)
            {
                sb.AppendFormat("{0} хв.", ts.Value.Minutes);
            }

            if (ts.Value.Seconds > 0)
            {
                sb.AppendFormat("{0} с.", ts.Value.Seconds);
            }

            return sb.ToString();
        }   

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}