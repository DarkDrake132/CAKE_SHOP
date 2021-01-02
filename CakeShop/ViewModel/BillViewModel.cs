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

        private DateTime _date;

        public DateTime Date
        {
            get { return _date; }
            set { _date = value; OnPropertyChanged(); }
        }

        private string _couponType;

        public string CouponType
        {
            get 
            { 
                if(_couponType == null)
                {
                    _couponType = "Không";
                }
                return _couponType; 
            }
            set { _couponType = value; CalculateSum(); }
        }


        private ObservableCollection<string> _couponList;

        public ObservableCollection<string> CouponList
        {
            get 
            { 
                if(_couponList == null)
                {
                    _couponList = new ObservableCollection<string>();
                }
                return _couponList; 
            }
            set { _couponList = value; }
        }


        public ICommand Incre { get; set; }
        public ICommand Decre { get; set; }
        public ICommand Delete { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand SubmitCommand { get; set; }

        public BillViewModel()
        {
            Date = DateTime.Now;
            OnPropertyChanged("Date");
            LoadCoupon();
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
                var id = DataProvider.Ins.DB.RECEIPTs.Max(x => x.ID) + 1;

                DataProvider.Ins.DB.RECEIPTs.Add(new RECEIPT() { ID = id, INPUTDATE = Date });
                DataProvider.Ins.DB.SaveChanges();

                int serial = 1;
                foreach(var item in List)
                {
                    var typeID = DataProvider.Ins.DB.CAKEs.Where(x => x.ID == item.ID).SingleOrDefault().TYPEID;
                    DataProvider.Ins.DB.RECEIPT_DETAIL.Add(new RECEIPT_DETAIL() { RECEIPT_ID = id, SERIAL = serial, CAKE_ID = item.ID, TYPEID = typeID, AMOUNT = item.SL, TOTAL = item.Total });
                    DataProvider.Ins.DB.SaveChanges();
                }

                Global.Cart.List.Clear();
                List.Clear();
                LoadData();
            });
        }

        private void LoadCoupon()
        {
            CouponList.Add("Không");
            CouponList.Add("10%");
            CouponList.Add("20%");
            CouponList.Add("50%");
            CouponType = "Không";
            OnPropertyChanged("CouponList");
        }

        private void CalculateSum()
        {
            TotalCost = 0;
            foreach (var item in List)
            {
                TotalCost += item.Total;
            }
            if (CouponType != "Không")
            {
                var num = int.Parse(CouponType.TrimEnd(new char[] { '%', ' ' }));
                TotalCost = TotalCost - TotalCost * num / 100;
            }
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
