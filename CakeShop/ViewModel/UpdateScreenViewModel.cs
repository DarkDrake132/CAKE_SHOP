using CakeShop.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace CakeShop.ViewModel
{
    public class UpdateScreenViewModel : BaseViewModel
    {
        public ObservableCollection<TypeCollector> Type
        {
            get => _Type;
            set
            {
                _Type = value;
            }
        }
        public ObservableCollection<string> ListType { get => _ListType; set => _ListType = value; }
        private ObservableCollection<TypeCollector> _Type = new ObservableCollection<TypeCollector>();
        private ObservableCollection<string> _ListType = new ObservableCollection<string>();
        public int AddPrice { get => _AddPrice; set { _AddPrice = value; OnPropertyChanged(); } }
        public string AddFormatedPrice { get => _AddFormatedPrice; set => _AddFormatedPrice = value; }
        public string AddImage { get => _AddImage; set { _AddImage = value; OnPropertyChanged(); } }
        public string AddType { get => _AddType; set { _AddType = value; OnPropertyChanged(); } }
        public string AddName { get => _AddName; set { _AddName = value; OnPropertyChanged(); } }
        public string AddInfo { get => _AddInfo; set { _AddInfo = value; OnPropertyChanged(); } }

        private int _AddPrice;
        private string _AddFormatedPrice;
        private string _AddImage;
        private string _AddType;
        private string _AddName;
        private string _AddInfo;
        public ICommand AddImageCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand ReloadImageCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public void DoSomething() { }
        public void GetTypeList()
        {
            Type.Clear();
            ListType.Clear();
            var query = DataProvider.Ins.DB.CAKE_TYPE;
            foreach (var i in query)
            {
                Type.Add(new TypeCollector() { ID = i.ID, C_NAME = i.C_NAME });
                ListType.Add(i.C_NAME);
            }
        }
        public UpdateScreenViewModel()
        {
            LoadData();
            GetTypeList();
            string tempName = null;
            //CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN");   // try with "en-US"
            //AddFormatedPrice = double.Parse("12345").ToString("#,###", cul.NumberFormat);
            AddImageCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Multiselect = true;
                ofd.Filter = "(*.jpg)|*.jpg|(*.png)|.*png";
                DialogResult dr = ofd.ShowDialog();
                if (dr == System.Windows.Forms.DialogResult.OK)
                {
                    tempName = AddImage;
                    AddImage = ofd.FileName;
                }
            });

           ReloadImageCommand = new RelayCommand<object>((p) =>
           {
               if (tempName == null)
               {
                   return false;
               }
               return true;
           }, (p) =>
           {
               AddImage = tempName;
               tempName = null;
           });

            SaveCommand = new RelayCommand<object>((p) =>
            {
                if (AddName == null || AddType == null)
                {
                    return false;
                }
                return true;
            }, (p) =>
            {
                int typeID = 0;
                foreach (var i in Type)
                {
                    if (i.C_NAME == AddType)
                    {
                        typeID = i.ID;
                        break;
                    }
                }

                changedLocation(AddImage, System.IO.Path.GetFileName(AddImage));

                var item = DataProvider.Ins.DB.CAKEs.Where(x => x.ID == Global.SelectedID).SingleOrDefault();

                item.C_NAME = AddName;
                item.INFO = AddInfo;
                item.PRICE = AddPrice;
                item.IMG = AddImage;
                item.TYPEID = typeID;

                DataProvider.Ins.DB.SaveChanges();
            });

            DeleteCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                var visible = DataProvider.Ins.DB.CAKEs.Where(x => x.ID == Global.SelectedID).SingleOrDefault();
                visible.VISIBLE = 0;
                DataProvider.Ins.DB.SaveChanges();
                AddName = null;
                AddPrice = 0;
                AddType = null;
                AddInfo = null;
                AddImage = null;
            });
        }

        private void getFileName(ref string path)
        {
            string res = System.IO.Path.GetFileName(path);
            path = path.Substring(0, path.IndexOf(res) - 1);
            //return res;
        }
        private void changedLocation(string sourcePath, string name)
        {
            string targetPath = Environment.CurrentDirectory.ToString();
            string temp = System.IO.Directory.GetParent(targetPath).ToString();
            temp = System.IO.Directory.GetParent(temp).ToString();
            targetPath = temp + "/Images";

            getFileName(ref sourcePath);
            string sourceFile = System.IO.Path.Combine(@sourcePath, name);
            var tail = name.Substring(name.Length - 4);
            name = DateTime.Now.ToString("yyyyMMddHHmmssfff") + tail;
            string destFile = System.IO.Path.Combine(@targetPath, name);
            FileInfo f1 = new FileInfo(sourceFile);
            FileInfo f2 = new FileInfo(destFile);
            f1.CopyTo(destFile);
        }

        private void LoadData()
        {
            var item = DataProvider.Ins.DB.CAKEs.Where(x => x.ID == Global.SelectedID).SingleOrDefault();
            AddName = item.C_NAME;
            AddImage = item.IMG;
            AddInfo = item.INFO;
            AddPrice = item.PRICE ?? default(int);
            AddType = DataProvider.Ins.DB.CAKE_TYPE.Where(x => x.ID == item.TYPEID).SingleOrDefault().C_NAME;
        }
    }
}
