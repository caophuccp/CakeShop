using CakeShop.Models;
using CakeShop.View.AddScreen;
using System;
using System.Collections.Generic;
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

namespace CakeShop.View
{
    /// <summary>
    /// Interaction logic for HomeScreen.xaml
    /// </summary>
    public partial class HomeScreen : UserControl
    {

        public delegate void BuyCakeDelegate(Cake cake, int quantity);
        public BuyCakeDelegate BuyCake;

        List<Cake> data = CakeDAO.GetAll();
        List<Cake> categoryListData = new List<Cake>();
        public HomeScreen()
        {
            InitializeComponent();
            calculateCategory();
            categoryList.ItemsSource = categoryListData;
            
        }
        void calculateCategory()
        {
            foreach (var i in data)
            {
                categoryListData.Add(i);
            }
            for (int i = 0; i < categoryListData.Count; i++)
            {
                for (int j = i + 1; j < categoryListData.Count; j++)
                {
                    if (categoryListData[i].Category == categoryListData[j].Category)
                    {
                        categoryListData.RemoveAt(j);
                    }
                }
            }
        }

        private void categoryList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = categoryList.SelectedIndex;
            List<Cake> cakeCategory = new List<Cake>();
            if(index >= 0 && index < data.Count)
            {
                Cake c = data[index];
                for(int i = 0; i < data.Count; i++)
                {
                    if (data[i].Category == c.Category)
                    {
                        cakeCategory.Add(data[i]);
                    }
                }
            }
            cakeList.ItemsSource = cakeCategory;
        }

        private void BuyButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Cake cake = button.DataContext as Cake;
            if (BuyCake != null)
            {
                var window = new AddCakeWindow();
                if (window.ShowDialog() == true) {
                    BuyCake?.Invoke(cake, window.Answer);
                    Window.GetWindow(this).Close();
                }
            } else
            {

            }
        }
    }
}
