using System.Globalization;
using System.Windows.Data;

namespace ASiNet.App.VWA.Converters;
public class TimeOnlyConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return ((DateTime)value).ToString("hh:mm:ss:fff");
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
