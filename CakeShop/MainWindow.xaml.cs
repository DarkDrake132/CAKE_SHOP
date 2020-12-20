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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CakeShop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = ListViewMenu.SelectedIndex;
            MoveCursormenu(index);

            switch (index)
            {
                case 0:
                    GridPriciple.Children.Clear();
                    GridPriciple.Children.Add(new HomeUserControl());
                    break;
                case 1:
                    GridPriciple.Children.Clear();
                    GridPriciple.Children.Add(new StatisticsUserControl());
                    break;
                case 2:
                    GridPriciple.Children.Clear();
                    GridPriciple.Children.Add(new AddUserControl());
                    break;
                case 3:
                    GridPriciple.Children.Clear();
                    GridPriciple.Children.Add(new BillUserControl());
                    break;
                case 4:
                    GridPriciple.Children.Clear();
                    GridPriciple.Children.Add(new SettingsUserControl());
                    break;
                default:
                    break;
            }
        }

        private void MoveCursormenu(int index)
        {
            TransitiongContentSldie.OnApplyTemplate();
            GridCursor.Margin = new Thickness(0, 100 + ((60 * index)), 0, 0);
        }
    }
}
