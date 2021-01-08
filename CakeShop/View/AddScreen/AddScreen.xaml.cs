using OrderShop.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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

namespace CakeShop.View.AddScreen
{
    /// <summary>
    /// Interaction logic for AddScreen.xaml
    /// </summary>
    public partial class AddScreen : UserControl
    {
        public BindingList<OrderCake> OrderCakeList { get; set; } = new BindingList<OrderCake>()
        {
            new OrderCake(){CakeID = 1, Quantity = 2},
            new OrderCake(){CakeID = 2, Quantity = 1},
            new OrderCake(){CakeID = 3, Quantity = 3},
            new OrderCake(){CakeID = 4, Quantity = 1},
            new OrderCake(){CakeID = 2, Quantity = 1},
            new OrderCake(){CakeID = 1, Quantity = 1},
            new OrderCake(){CakeID = 4, Quantity = 1},
            new OrderCake(){CakeID = 3, Quantity = 1},
        };
        public AddScreen()
        {
            InitializeComponent();
            //OrderCakeListView.ItemsSource = OrderCakeList;
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            OrderCake ordercake = button.DataContext as OrderCake;
            if (ordercake != null)
            {
                OrderCakeList.Remove(ordercake);
            }
            
        }
    }
}
