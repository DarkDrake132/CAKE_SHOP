using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Windows.Data;

namespace CakeShop.ImageConverter
{
    public class AbsoluteConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string relative = (string)value;
            if (relative == null || relative.Contains("/") || relative.Contains("\\"))
            {
                return relative;
            }    
            string folder = Environment.CurrentDirectory.ToString();
            string temp = System.IO.Directory.GetParent(folder).ToString();
            temp = System.IO.Directory.GetParent(temp).ToString();
            string absolutePath = $"{temp}{"\\Images/"}{relative}";

            return absolutePath;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
