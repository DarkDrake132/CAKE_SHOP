using CakeShop.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CakeShop.ViewModel
{
    public class BillViewModel :BaseViewModel
    {
        CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN");
        private ObservableCollection<BillCollector> _list;

        public ObservableCollection<BillCollector> List
        {
            get
            { 
                if(_list == null)
                {
                    _list = new ObservableCollection<BillCollector>();
                }
                return _list; 
            }
            set { _list = value; OnPropertyChanged(); }
        }

        private int _totalCost;

        public int TotalCost
        {
            get { return _totalCost; }
            set { _totalCost = value; }
        }

        private string _totalCostStraing;

        public string TotalCostString
        {
            get { return _totalCostStraing; }
            set { _totalCostStraing = value; OnPropertyChanged(); }
        }

        private string _date;

        public string Date
        {
            get { return _date; }
            set { _date = value; OnPropertyChanged(); }
        }


        public ICommand Incre { get; set; }
        public ICommand Decre { get; set; }
        public ICommand Delete { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand SubmitCommand { get; set; }

        public BillViewModel()
        {
            Date = DateTime.Today.ToString();
            OnPropertyChanged("Date");
            LoadData();

            Decre = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                int idGet = Int32.Parse(p.ToString());
                Global.Cart.Decre(idGet);
                if(List.Where(x => x.ID == idGet).SingleOrDefault().SL == 1)
                {
                    List.Remove(List.Where(x => x.ID == idGet).SingleOrDefault());
                    OnPropertyChanged("List");
                }
                else
                {
                    List.Where(x => x.ID == idGet).SingleOrDefault().Decre();
                }
                CalculateSum();
            });
            Incre = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                int idGet = Int32.Parse(p.ToString());
                Global.Cart.Incre(idGet);
                List.Where(x => x.ID == idGet).SingleOrDefault().Incre();
                CalculateSum();
            });

            Delete = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                int idGet = Int32.Parse(p.ToString());
                Global.Cart.Delete(idGet);
                List.Remove(List.Where(x => x.ID == idGet).SingleOrDefault());
                OnPropertyChanged("List");
                CalculateSum();
            });

            CancelCommand = new RelayCommand<object>((p) =>
            {
                if(List.Count() == 0)
                {
                    return false;
                }
                return true;
            }, (p) =>
            {
                Global.Cart.List.Clear();
                List.Clear();
                LoadData();
            });

            SubmitCommand = new RelayCommand<object>((p) =>
            {
                if (List.Count() == 0)
                {
                    return false;
                }
                return true;
            }, (p) =>
            {
                Global.Cart.List.Clear();
                List.Clear();
                LoadData();
            });
        }

        private void CalculateSum()
        {
            TotalCost = 0;
            TotalCostString = "";
            foreach (var item in List)
            {
                TotalCost += item.TotalIn;
            }
            TotalCostString = TotalCost.ToString("###,###,###,###", cul.NumberFormat) + " đ";
            OnPropertyChanged("TotalCostString");
        }

        private void LoadData()
        {
            var CakeData = DataProvider.Ins.DB.CAKEs;
            foreach (var item in Global.Cart.List)
            {
                var cake = CakeData.Where(x => x.ID == item.ID).SingleOrDefault();
                List.Add(new BillCollector(cake.IMG, cake.ID, cake.C_NAME, cake.PRICE, item.SL));
            }
            CalculateSum();
            OnPropertyChanged("List");
        }
    }
}
