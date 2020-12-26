using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Windows;
using System.Windows.Input;
using System.Windows.Forms;
using System.IO;
using CakeShop.Model;

namespace CakeShop.ViewModel
{
    public class AddViewModel : BaseViewModel
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
        public int AddPrice { get => _AddPrice; set => _AddPrice = value; }
        public string AddFormatedPrice { get => _AddFormatedPrice; set => _AddFormatedPrice = value; }
        public string AddImage { get => _AddImage; set { _AddImage = value; OnPropertyChanged(); } }
        public string AddType { get => _AddType; set => _AddType = value; }
        public string AddName { get => _AddName; set => _AddName = value; }
        public string AddInfo { get => _AddInfo; set => _AddInfo = value; }

        private int _AddPrice;
        private string _AddFormatedPrice;
        private string _AddImage;
        private string _AddType;
        private string _AddName;
        private string _AddInfo;
        public ICommand AddImageCommand { get; set; }
        public ICommand Save { get; set; }

        public void DoSomething() { }
        public void GetTypeList()
        {
            var query = DataProvider.Ins.DB.CAKE_TYPE;
            foreach (var i in query)
            {
                Type.Add(new TypeCollector() { ID=i.ID, C_NAME=i.C_NAME });
                ListType.Add(i.C_NAME);
            }
        }
        public AddViewModel()
        {
            GetTypeList();
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
                    AddImage = ofd.FileName;
                }
            });
            Save = new RelayCommand<object>((p) => 
            {
                if(AddName == null || AddType == null)
                {
                    return false;
                }
                return true; 
            }, (p) => 
            {
                int cakeID = DataProvider.Ins.DB.CAKEs.Max(x => x.ID) + 1;
                int typeID = 0;
                foreach(var i in Type)
                {
                    if (i.C_NAME == AddType)
                    {
                        typeID = i.ID;
                        break;
                    }
                }
                if(typeID == 0)
                {
                    typeID = Type.Max(x => x.ID) + 1;
                    DataProvider.Ins.DB.CAKE_TYPE.Add(new CAKE_TYPE() { ID = typeID, C_NAME = AddType });
                    Type.Add(new TypeCollector() { ID = typeID, C_NAME = AddType });
                    DataProvider.Ins.DB.SaveChanges();
                }

                string newName = "";
                changedLocation(AddImage, System.IO.Path.GetFileName(AddImage), ref newName);
                AddImage = newName;

                DataProvider.Ins.DB.CAKEs.Add(new CAKE() { C_NAME = AddName, ID = cakeID, PRICE = AddPrice, IMG = AddImage, TYPEID =  typeID});
                DataProvider.Ins.DB.SaveChanges();
            });
        }

        private void getFileName(ref string path)
        {
            string res = System.IO.Path.GetFileName(path);
            path = path.Substring(0, path.IndexOf(res) - 1);
            //return res;
        }
        private void changedLocation(string sourcePath, string name, ref string newName)
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

            newName = name;
        }
    }
}
