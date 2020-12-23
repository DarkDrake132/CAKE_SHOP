using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Configuration;

namespace CakeShop
{
    /// <summary>
    /// Interaction logic for SplashScreen.xaml
    /// </summary>
    public partial class SplashScreen : Window
    {
        public SplashScreen()
        {
            InitializeComponent();
        }

        private void Window_Load(object sender, RoutedEventArgs e)
        {
            var value = ConfigurationManager.AppSettings["checkShowDialog"];
            var showSplash = bool.Parse(value);

            if (showSplash == false)
            {
                var screen = new MainWindow();
                screen.Show();
                this.Close();
            }
        }
    }
}
