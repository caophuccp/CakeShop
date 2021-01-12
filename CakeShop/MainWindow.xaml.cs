using CakeShop.View;
using CakeShop.View.AddScreen;
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
            ListViewMenu.SelectedIndex = 0;
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = ListViewMenu.SelectedIndex;
            switch (index)
            {
                case 0:
                    //Homescreen
                    mainGrid.Children.Clear();
                    mainGrid.Children.Add(new HomeScreen());
                    break;
                case 1:
                    //Add screen
                    mainGrid.Children.Clear();
                    mainGrid.Children.Add(new AddScreen());
                    break;
                case 2:
                    //Statistic 
                    mainGrid.Children.Clear();
                    mainGrid.Children.Add(new Statistic());
                    break;
                case 3:
                    //Order
                    break;
            }
        }
    }
}
