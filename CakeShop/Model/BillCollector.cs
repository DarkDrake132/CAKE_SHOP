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


        private int _price;

        public int Price
        {
            get { return _price; }
            set { _price = value; }
        }

        private int _sl;

        public int SL
        {
            get { return _sl; }
            set { _sl = value; OnPropertyChanged(); }
        }

        private int _total;

        public int Total
        {
            get { return _total; }
            set { _total = value; }
        }

        public BillCollector(string image, int id, string name, int ?price, int sl)
        {
            Image = image;
            ID = id;
            Name = name;
            Price = price ?? 0;
            SL = sl;    
            Total = Price * SL;
        }

        public void Incre()
        {
            SL++;
            Total = Price * SL;
            OnPropertyChanged("SL");
            OnPropertyChanged("Total");
        }

        public void Decre()
        {
            SL--;
            Total = Price * SL;
            OnPropertyChanged("SL");
            OnPropertyChanged("Total");
        }
    }
}
