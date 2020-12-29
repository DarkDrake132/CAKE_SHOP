using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace CakeShop.Model
{
    public class CakeCollector
    {
        CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN");
        private int _id;

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        private string c_name;

        public string C_NAME
        {
            get { return c_name; }
            set { c_name = value; }
        }

        private string _type;

        public string TYPE
        {
            get { return _type; }
            set { _type = value; }
        }

        private string _price;

        public string PRICE
        {
            get { return _price; }
            set { _price = value; }
        }

        private string _image_link;

        public string IMAGE_LINK
        {
            get { return _image_link; }
            set { _image_link = value; }
        }

        private string _info;

        public string INFO
        {
            get { return _info;; }
            set { _info = value; }
        }

        public CakeCollector(int id, string name, string type, int price, string image_link, string info)
        {
            this.ID = id;
            this.C_NAME = name;
            this.TYPE = type;
            this.PRICE = price.ToString("#,###", cul.NumberFormat) + " đ";
            this.IMAGE_LINK = image_link;
            this.INFO = info;
        }
    }
}
