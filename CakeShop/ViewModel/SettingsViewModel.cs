using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Configuration;

namespace CakeShop.ViewModel
{
    public class SettingsViewModel : BaseViewModel
    {
        public ICommand ConfirmCommand { get; set; }
        public bool CheckDialog { get; set; }

        public SettingsViewModel()
        {
            ConfirmCommand = new RelayCommand<SettingsUserControl>((p) =>
            {
                return true;
            }, (p) =>
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings["checkShowDialog"].Value = CheckDialog ? "true" : "false";
                config.Save(ConfigurationSaveMode.Minimal);
                ConfigurationManager.RefreshSection("appSettings");

                //p.Close();
            });
        }
    }
}
