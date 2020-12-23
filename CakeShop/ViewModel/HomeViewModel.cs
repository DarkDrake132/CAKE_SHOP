﻿using CakeShop.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CakeShop.ViewModel
{
    public class HomeViewModel : BaseViewModel
    {
        private const int TotalItemsPerPage = 4;
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

        private string _SearchName;

        public string SearchName
        {
            get { return _SearchName; }
            set { _SearchName = value; OnPropertyChanged(); }
        }

        private string _SearchType;

        public string SearchType
        {
            get { return _SearchType; }
            set { _SearchType = value; }
        }



        private ObservableCollection<CakeCollector> _oldData;

        public ObservableCollection<CakeCollector> OldData
        {
            get { return _oldData; }
            set { _oldData = value; }
        }


        private ObservableCollection<CakeCollector> _list;

        public ObservableCollection<CakeCollector> List
        {
            get => _list;
            set { _list = value; OnPropertyChanged(); }
        }

        public ICommand Prev { get; set; }

        public ICommand Next { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand BuyCommand { get; set; }
        public ICommand UpdateCommand { get; set; }

        public HomeViewModel()
        {
            LoadData("", "");

            SearchName = "";
            SearchType = "";

            Prev = new RelayCommand<object>((p) =>
            {
                if(CurrentPage <= 1)
                {
                    return false;
                }
                return true;
            }, (p) =>
            {
                CurrentPage--;
                List = new ObservableCollection<CakeCollector>(OldData.Skip((CurrentPage - 1) * TotalItemsPerPage).Take(TotalItemsPerPage));
                OnPropertyChanged("List");
                CurrentPageDisplay = CurrentPage.ToString();
                OnPropertyChanged("CurrentPageDisplay");
            });

            Next = new RelayCommand<object>((p) =>
            {
                if(CurrentPage >= LastPage)
                {
                    return false;
                }
                return true;
            }, (p) =>
            {
                CurrentPage++;
                List = new ObservableCollection<CakeCollector>(OldData.Skip((CurrentPage - 1) * TotalItemsPerPage).Take(TotalItemsPerPage));
                OnPropertyChanged("List");
                CurrentPageDisplay = CurrentPage.ToString();
                OnPropertyChanged("CurrentPageDisplay");
            });

            SearchCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                LoadData(SearchName, SearchType);
            });

            BuyCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {

            });

            UpdateCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                MessageBox.Show(p.ToString());
            });
        }

        private void LoadData(string searchName, string searchType)
        {
            OldData = new ObservableCollection<CakeCollector>();

            var query = from b in DataProvider.Ins.DB.CAKEs
                        join c in DataProvider.Ins.DB.CAKE_TYPE on b.TYPEID equals c.ID
                        where b.C_NAME.Contains(searchName) && c.C_NAME.Contains(searchType)
                        select new
                        {
                            id = b.ID,
                            name = b.C_NAME,
                            type = c.C_NAME,
                            price = b.PRICE ?? 0,
                            image = b.IMG
                        };

            foreach (var item in query)
            {
                OldData.Add(new CakeCollector(item.id, item.name, item.type, item.price, item.image));
            }

            CurrentPage = 1;
            LastPage = (int)Math.Ceiling(((double)OldData.Count() / (double)TotalItemsPerPage));
            List = new ObservableCollection<CakeCollector>(OldData.Skip((CurrentPage - 1) * TotalItemsPerPage).Take(TotalItemsPerPage));
            OnPropertyChanged("List");
            CurrentPageDisplay = CurrentPage.ToString();
            LastPageDisplay = LastPage.ToString();
            OnPropertyChanged("CurrentPageDisplay");
            OnPropertyChanged("LastPageDisplay");

        }
    }
}
