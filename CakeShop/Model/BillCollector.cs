using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using CakeShop.ViewModel;

namespace CakeShop.Model
{
    public class BillCollector : BaseViewModel
    {
        CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN");

        private string _image;

        public string Image
        {
            get { return _image; }
            set { _image = value; }
        }

        private int _id;

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }


        private int _priceIn;

        public int PriceIn
        {
            get { return _priceIn; }
            set { _priceIn = value; }
        }

        private string _priceOut;

        public string PriceOut
        {
            get { return _priceOut; }
            set { _priceOut = value; OnPropertyChanged(); }
        }

        private int _sl;

        public int SL
        {
            get { return _sl; }
            set { _sl = value; OnPropertyChanged(); }
        }

        private int _totalIn;

        public int TotalIn
        {
            get { return _totalIn; }
            set { _totalIn = value; }
        }

        private string _totalOut;

        public string TotalOut
        {
            get { return _totalOut; }
            set { _totalOut = value; OnPropertyChanged(); }
        }

        public BillCollector(string image, int id, string name, int ?price, int sl)
        {
            Image = image;
            ID = id;
            Name = name;
            PriceIn = price ?? 0;
            PriceOut = PriceIn.ToString("###,###,###,###", cul.NumberFormat) + " đ";
            SL = sl;
            TotalIn = PriceIn * SL;
            TotalOut = TotalIn.ToString("###,###,###,###", cul.NumberFormat) + " đ";
        }

        public void Incre()
        {
            SL++;
            TotalIn = PriceIn * SL;
            TotalOut = TotalIn.ToString("###,###,###,###", cul.NumberFormat) + " đ";
            OnPropertyChanged("SL");
            OnPropertyChanged("TotalOut");
        }

        public void Decre()
        {
            SL--;
            TotalIn = PriceIn * SL;
            TotalOut = TotalIn.ToString("###,###,###,###", cul.NumberFormat) + " đ";
            OnPropertyChanged("SL");
            OnPropertyChanged("TotalOut");
        }
    }
}
