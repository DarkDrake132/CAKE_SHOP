using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Globalization;

namespace CakeShop.Converter
{
    public class VNDConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var temp = Int32.Parse(value.ToString());
            if (temp == 0)
            {
                return temp;
            }
            string con = temp.ToString("###,###,###,### đ").Replace(',', '.');
            return con;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
