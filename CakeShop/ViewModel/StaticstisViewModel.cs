using CakeShop.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveCharts;
using LiveCharts.Wpf;
using System.Windows.Input;
using System.Windows;

namespace CakeShop.ViewModel
{
    public class StaticstisViewModel : BaseViewModel
    {
        private ObservableCollection<CAKE_TYPE> _listType;
        public ObservableCollection<CAKE_TYPE> ListType
        {
            get { return _listType; } set { _listType = value; }
        }

        private ObservableCollection<RECEIPT_DETAIL> _listDetail;
        public ObservableCollection<RECEIPT_DETAIL> ListDetail
        {
            get { return _listDetail; } set { _listDetail = value; }
        }

        private ObservableCollection<CAKE> _listCake;
        public ObservableCollection<CAKE> ListCake
        {
            get { return _listCake; } set { _listCake = value; }
        }

        private ObservableCollection<AmountSold> _cakeWithSold;
        public ObservableCollection<AmountSold> CakeWithSold
        {
            get { return _cakeWithSold; } set { _cakeWithSold = value; }
        }

        private SeriesCollection _cakeChart;
        public SeriesCollection CakeChart
        {
            get { return _cakeChart; } set { _cakeChart = value; }
        }

        private int _totalIncome;
        public int TotalIncome
        {
            get { return _totalIncome; } set { _totalIncome = value; }
        }

        public int NumberOfType { get; set; } = DataProvider.Ins.DB.CAKE_TYPE.Count();

        private AmountSold _maxSold;
        public AmountSold MaxSold
        {
            get { return _maxSold; } set { _maxSold = value; }
        }

        private int _totalSold;
        public int TotalSold
        {
            get { return _totalSold; } set { _totalSold = value; }
        }

        private int _selectedMonth;
        public int SelectedMonth
        {
            get { return _selectedMonth; } set { _selectedMonth = value; }
        }

        private int _monthIncome;
        public int MonthIncome
        {
            get { return _monthIncome; } set { _monthIncome = value; }
        }

        private ObservableCollection<RECEIPT_DETAIL> _listDetailOfMonth;
        public ObservableCollection<RECEIPT_DETAIL> ListDetailOfMonth
        {
            get { return _listDetailOfMonth; } set { _listDetailOfMonth = value; }
        }

        private DateTime _startDate;
        public DateTime StartDate
        {
            get { return _startDate; } set { _startDate = value; }
        }

        private DateTime _endDate;
        public DateTime EndDate
        {
            get { return _endDate; } set { _endDate = value; }
        }

        private ObservableCollection<RECEIPT> _statMonth;
        public ObservableCollection<RECEIPT> StatMonth
        {
            get { return _statMonth; } set { _statMonth = value; }
        }

        private ObservableCollection<AmountSold> _cakeWithSoldMonth;
        public ObservableCollection<AmountSold> CakeWithSoldMonth
        {
            get { return _cakeWithSoldMonth; } set { _cakeWithSoldMonth = value; }
        }

        private AmountSold _maxSoldMonth;
        public AmountSold MaxSoldMonth
        {
            get { return _maxSoldMonth; } set { _maxSoldMonth = value; }
        }

        private SeriesCollection _columnChart;
        public SeriesCollection ColumnChart
        {
            get { return _columnChart; }
            set { _columnChart = value; }
        }

        private int _monthSold;
        public int MonthSold
        {
            get { return _monthSold; } set { _monthSold = value; }
        }
        public ICommand ChangeStatCommand { get; set; }
        public ObservableCollection<int> Months { get; set; } = new ObservableCollection<int>();
        public string[] LabelX { get; set; } = new[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };
        public Func<double, string> Formatter { get; set; }

        public StaticstisViewModel()
        {
            SelectedMonth = 1;
            LoadData();
            GetStatistics();

            ChangeStatCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                OnPropertyChanged("SelectedMonth");
                GetStatistics();
                OnPropertyChanged("StatMonth"); OnPropertyChanged("MonthIncome"); OnPropertyChanged("MonthSold"); OnPropertyChanged("CakeWithSoldMonth"); OnPropertyChanged("MaxSoldMonth");
            });
        }

        private void LoadData()
        {
            for (int i = 1; i <= 12; i++) 
            {
                Months.Add(i);
            }
            ListType = new ObservableCollection<CAKE_TYPE>(DataProvider.Ins.DB.CAKE_TYPE);
            ListDetail = new ObservableCollection<RECEIPT_DETAIL>(DataProvider.Ins.DB.RECEIPT_DETAIL);
            ListCake = new ObservableCollection<CAKE>(DataProvider.Ins.DB.CAKEs);
            CakeWithSold = new ObservableCollection<AmountSold>();
            GetChart();
            foreach (var cake in ListCake)
            {
                int amount = 0;
                foreach (var detail in ListDetail)
                {
                    if (cake.ID == detail.CAKE_ID)
                    {
                        amount += detail.AMOUNT.GetValueOrDefault();
                    }
                }
                var type = DataProvider.Ins.DB.CAKE_TYPE.Where(x => x.ID == cake.TYPEID).SingleOrDefault();
                var item = new AmountSold(cake.C_NAME, type.C_NAME, amount);
                CakeWithSold.Add(item);
            }
            MaxSold = CakeWithSold.OrderByDescending(x => x.Amount).First();
            TotalSold = CakeWithSold.Sum(x => x.Amount);
        }

        private void GetChart()
        {
            TotalIncome = 0;
            CakeChart = new SeriesCollection();
            ColumnChart = new SeriesCollection();
            ColumnChart.Add(new ColumnSeries { Values = new ChartValues<int> { } });
            TotalIncome = ListDetail.Sum(x => x.TOTAL ?? 0);
            foreach (var item in ListType)
            {
                int typeIncome = 0;
                foreach (var detail in ListDetail)
                {
                    if (item.ID == detail.TYPEID)
                    {
                        typeIncome += detail.TOTAL.GetValueOrDefault();
                    }
                }
                CakeChart.Add(new PieSeries { Values = new ChartValues<double> { Math.Round(100 * (double)typeIncome / (double)TotalIncome, 2) }, Title = item.C_NAME, DataLabels = true });
            }

            for (int i = 1; i <= 12; i++) 
            {
                int income = 0;
                var receiptOfMonth = new ObservableCollection<RECEIPT>();
                var detailOfMonth = new ObservableCollection<RECEIPT_DETAIL>();
                DateTime start = new DateTime(2020, i, 1);
                DateTime end = new DateTime(2020, i == 12 ? 12 : i + 1, 1);
                var query = from a in DataProvider.Ins.DB.RECEIPTs
                            where a.INPUTDATE >= start && a.INPUTDATE < end
                            select a;
                foreach (var item in query)
                {
                    receiptOfMonth.Add(item);
                }
                foreach (var receipt in receiptOfMonth)
                {
                    foreach (var detail in ListDetail)
                    {
                        if (detail.RECEIPT_ID == receipt.ID)
                        {
                            detailOfMonth.Add(detail);
                        }
                    }
                }
                income = detailOfMonth.Sum(x => x.TOTAL.GetValueOrDefault());
                ColumnChart[0].Values.Add(income);
                Formatter = value => value.ToString("###,###,###,###").Replace(',', '.');
            }
        }
        private void GetStatistics()
        {
            MonthIncome = 0;
            MonthSold = 0;
            StatMonth = new ObservableCollection<RECEIPT>();
            ListDetailOfMonth = new ObservableCollection<RECEIPT_DETAIL>();
            CakeWithSoldMonth = new ObservableCollection<AmountSold>();
            StartDate = new DateTime(2020, SelectedMonth, 1);
            EndDate = new DateTime(2020, SelectedMonth == 12 ? 12 : SelectedMonth + 1, 1);
            var query = from a in DataProvider.Ins.DB.RECEIPTs
                        where a.INPUTDATE >= StartDate && a.INPUTDATE < EndDate
                        select a;
            foreach(var item in query)
            {
                StatMonth.Add(item);
            }
            foreach (var receipt in StatMonth)
            {
                foreach (var detail in ListDetail)
                {
                    if (receipt.ID == detail.RECEIPT_ID)
                    {
                        ListDetailOfMonth.Add(detail);
                    }
                }
            }
            MonthIncome = ListDetailOfMonth.Sum(x => x.TOTAL ?? 0);
            MonthSold = ListDetailOfMonth.Sum(x => x.AMOUNT ?? 0);
            foreach (var cake in ListCake)
            {
                int amount = 0;
                foreach (var detail in ListDetailOfMonth)
                {
                    if (detail.CAKE_ID == cake.ID)
                    {
                        amount += detail.AMOUNT.GetValueOrDefault();
                    }
                }
                var type = DataProvider.Ins.DB.CAKE_TYPE.Where(x => x.ID == cake.TYPEID).SingleOrDefault();
                var item = new AmountSold(cake.C_NAME, type.C_NAME, amount);
                CakeWithSoldMonth.Add(item);
            }
            MaxSoldMonth = CakeWithSoldMonth.OrderByDescending(x => x.Amount).First();
            if(MaxSoldMonth.Amount==0)
            {
                MaxSoldMonth = new AmountSold("Không", "Không", 0);
            }
        }
    }
}
