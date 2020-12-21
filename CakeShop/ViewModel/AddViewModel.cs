using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace CakeShop.ViewModel
{
    public class AddViewModel : BaseViewModel
    {
        public int AddPrice { get => _AddPrice; set => _AddPrice = value; }
        public string AddFormatedPrice { get => _AddFormatedPrice; set => _AddFormatedPrice = value; }

        //public string AddFormatedPrice 
        //{
        //    get
        //    {
        //        return String.Format("Money {0, 0:C2}", 10000);
        //    }
        //    set 
        //    { 
        //        _AddFormatedPrice = value; 
        //        OnPropertyChanged(); 
        //    } 
        //}

        private int _AddPrice;
        private string _AddFormatedPrice;
        public AddViewModel()
        {
            CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN");   // try with "en-US"
            AddFormatedPrice = double.Parse("12345").ToString("#,###", cul.NumberFormat);
        }

        
    }
}
