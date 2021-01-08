using CakeShop.Models;
using OrderShop.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        public BindingList<OrderCake> Products { get; set; } = new BindingList<OrderCake>();

        public AddScreen()
        {
            InitializeComponent();
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            OrderCake ordercake = button.DataContext as OrderCake;
            if (ordercake != null)
            {
                Products.Remove(ordercake);
            }
        }

        private void AddCake_Click(object sender, RoutedEventArgs e)
        {
            var home = new HomeScreen();
            home.BuyCake = BuyCake;
            Uri iconUri = new Uri("pack://application:,,,/Images/shop.ico", UriKind.RelativeOrAbsolute);

            Window window = new Window()
            {
                Title = "Choose",
                Content = home,
                Icon = BitmapFrame.Create(iconUri),

            };
            window.ShowDialog();
        }

        private void BuyCake(Cake cake, int quantity)
        {
            if (cake != null)
            {
                var orderCake = Products.FirstOrDefault(e => e.CakeID == cake.ID);
                if (orderCake != null)
                {
                    orderCake.Quantity += quantity;
                    OCListView.Items.Refresh();
                }
                else
                {
                    OrderCake newOrderCake = new OrderCake() { CakeID = cake.ID, Quantity = quantity };
                    Products.Add(newOrderCake);
                }
                var total = Products.Join(CakeDAO.Cache,
                    oc => oc.CakeID,
                    c => c.ID,
                    (oc, c) => oc.Quantity * c.Price
                ).Sum();
                TotalTextBox.Text = total.ToString();
            }
        }

        private void OrderAddButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateForm())
            {
                Order order = GetFormData();
                var error = OrderDAO.Insert(order);
                if (error)
                {
                    MessageBox.Show("Đã thêm hoá đơn", "Cake Shop", MessageBoxButton.OK, MessageBoxImage.Information);
                    ClearForm();
                } else
                {
                    MessageBox.Show("Đã có lỗi không rõ xảy ra!\n Vui lòng thử lại", "Cake Shop", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void ClearForm()
        {
            NameTextBox.Clear();
            EmailTextBox.Clear();
            PhoneTextBox.Clear();
            AddressTextBox.Clear();
            Products.Clear();
            TotalTextBox.Text = "0";
        }

        private bool ValidateForm()
        {
            NameTextBox.HasError = string.IsNullOrEmpty(NameTextBox.Text);
            EmailTextBox.HasError = string.IsNullOrEmpty(EmailTextBox.Text) ? true : !emailRegex.IsMatch(EmailTextBox.Text);
            PhoneTextBox.HasError = string.IsNullOrEmpty(PhoneTextBox.Text) ? true : !phoneRegex.IsMatch(PhoneTextBox.Text);
            AddressTextBox.HasError = string.IsNullOrEmpty(AddressTextBox.Text);
            bool cakeError = Products.Count == 0;
            bool error = NameTextBox.HasError | EmailTextBox.HasError | PhoneTextBox.HasError | AddressTextBox.HasError | cakeError;
            return !error;
        }

        private Order GetFormData()
        {
            Order result = null;
            result = new Order()
            {
                CustomerName = NameTextBox.Text,
                Email = EmailTextBox.Text,
                Phone = PhoneTextBox.Text,
                Address = AddressTextBox.Text,
                DateCreated = DateTime.Now,
                Products = Products.ToList(),
            };
            result.Total = result.Products.Join(CakeDAO.Cache,
                oc => oc.CakeID,
                c => c.ID,
                (oc, c) => oc.Quantity * c.Price
            ).Sum();
            return result;
        }

        private Regex emailRegex = new Regex(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?");
        private Regex phoneRegex = new Regex(@"^(0|\+84)[0-9]{9}$");
    }
}
