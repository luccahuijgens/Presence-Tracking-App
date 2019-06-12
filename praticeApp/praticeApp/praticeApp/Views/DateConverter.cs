using System;
using System.Collections.Generic;
using System.Text;
using praticeApp.Domain;
using Xamarin.Forms;

namespace praticeApp.Views
{
    public class DateConverter : IValueConverter
    {

        #region IValueConverter implementation

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is DateTime)
            {
                DateTime Date= (DateTime)value;
                if (DateTime.Now.ToString("MM/dd/yyyy").Equals(Date.ToString("MM/dd/yyyy")))
                {
                    return Date.ToString("HH:mm");
                }
                else if (DateTime.Today - Date.Date == TimeSpan.FromDays(1))
                {
                    return "Yesterday";
                }
                else if (DateTime.Today.Year != Date.Year)
                {
                    return Date.ToString("dd/MM/yyyy");
                }
                else
                {
                    return Date.ToString("dd MMM");
                }
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
