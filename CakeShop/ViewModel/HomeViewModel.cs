using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CakeShop.ViewModel
{
    public class HomeViewModel : BaseViewModel
    {
        private const int TotalItemsPerPage = 1;
        private int CurrentPage;
        private int LastPage;

        private string _currentPageDisplay;

        public string CurrentPageDisplay
        {
            get { return _currentPageDisplay; }
            set { _currentPageDisplay = value; OnPropertyChanged(); }
        }

        private string _lastPageDisplay;

        public string LastPageDisplay
        {
            get { return _lastPageDisplay; }
            set { _lastPageDisplay = value; }
        }


        public class Cake
        {
            private int _ID;

            public int ID
            {
                get { return _ID; }
                set { _ID = value; }
            }

            private string c_Name;

            public string C_NAME
            {
                get { return c_Name; }
                set { c_Name = value; }
            }

            private int _TYPEID;

            public int TYPEID
            {
                get { return _TYPEID; }
                set { _TYPEID = value; }
            }

            private int _PRICE;

            public int PRICE
            {
                get { return _PRICE; }
                set { _PRICE = value; }
            }

            private string _IMAGE_LINK;

            public string IMAGE_LINK
            {
                get { return _IMAGE_LINK; }
                set { _IMAGE_LINK = value; }
            }


            public Cake(int v1, string v2, int v3, int v4, string v5)
            {
                this.ID = v1;
                this.C_NAME = v2;
                this.TYPEID = v3;
                this.PRICE = v4;
                this.IMAGE_LINK = v5;
            }
        }

        private ObservableCollection<Cake> _oldData;

        public ObservableCollection<Cake> OldData
        {
            get { return _oldData; }
            set { _oldData = value; }
        }


        private ObservableCollection<Cake> _list;

        public ObservableCollection<Cake> List
        {
            get => _list;
            set { _list = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Cake> CreateList()
        {
            ObservableCollection<Cake> Ret = new ObservableCollection<Cake>();
            Ret.Add(new Cake(1, "A", 1, 10000, "./Images/Best-Birthday-Cake-with-milk-chocolate-buttercream-SQUARE.jpg"));
            Ret.Add(new Cake(2, "B", 1, 20000, "./Images/download.jpg"));
            return Ret;
        }

        public ICommand Prev { get; set; }

        public ICommand Next { get; set; }
        public HomeViewModel()
        {
            OldData = CreateList();
            CurrentPage = 1;
            LastPage = (int)Math.Ceiling((double)(OldData.Count() / TotalItemsPerPage));
            List = new ObservableCollection<Cake>(OldData.Skip((CurrentPage - 1) * TotalItemsPerPage).Take(TotalItemsPerPage));
            OnPropertyChanged("List");
            CurrentPageDisplay = CurrentPage.ToString();
            LastPageDisplay = LastPage.ToString();
            OnPropertyChanged("CurrentPageDisplay");
            OnPropertyChanged("LastPageDisplay");

            Prev = new RelayCommand<object>((p) =>
            {
                if(CurrentPage == 1)
                {
                    return false;
                }
                return true;
            }, (p) =>
            {
                CurrentPage--;
                List = new ObservableCollection<Cake>(OldData.Skip((CurrentPage - 1) * TotalItemsPerPage).Take(TotalItemsPerPage));
                OnPropertyChanged("List");
                CurrentPageDisplay = CurrentPage.ToString();
                OnPropertyChanged("CurrentPageDisplay");
            });
            Next = new RelayCommand<object>((p) =>
            {
                if(CurrentPage == LastPage)
                {
                    return false;
                }
                return true;
            }, (p) =>
            {
                CurrentPage++;
                List = new ObservableCollection<Cake>(OldData.Skip((CurrentPage - 1) * TotalItemsPerPage).Take(TotalItemsPerPage));
                OnPropertyChanged("List");
                CurrentPageDisplay = CurrentPage.ToString();
                OnPropertyChanged("CurrentPageDisplay");
            });
        }
    }
}
