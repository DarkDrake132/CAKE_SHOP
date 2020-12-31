using CakeShop.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeShop.ViewModel
{
    public class BillViewModel :BaseViewModel
    {
        public ObservableCollection<CAKE> List { get; set; } 

        public BillViewModel()
        {
            List = new ObservableCollection<CAKE>(DataProvider.Ins.DB.CAKEs);
        }
    }
}
