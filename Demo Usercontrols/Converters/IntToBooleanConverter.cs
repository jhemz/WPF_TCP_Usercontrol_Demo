using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Demo_Usercontrols.Converters
{
    public class IntToBooleanConverter : IValueConverter
    {
       

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool result = true;
            if((int)value == 0)
            {
                result = false;
            }
            return result;
        }

       

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int result = 1;
            if ((bool)value == false)
            {
                result = 0;
            }
            return result;
        }
    }
}
