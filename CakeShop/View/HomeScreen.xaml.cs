using CakeShop.Models;
using CakeShop.View.AddScreen;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace CakeShop.View
{
    /// <summary>
    /// Interaction logic for HomeScreen.xaml
    /// </summary>
    public partial class HomeScreen : UserControl
    {

        public delegate void BuyCakeDelegate(DependencyObject sender, Cake cake, int quantity);
        public BuyCakeDelegate BuyCake;

        List<Cake> data = CakeDAO.GetAll();
        //List<Cake> categoryListData = new List<Cake>();
        List<string> categoryListData = new List<string>()         
        {
            "Tất cả", "Bánh dừa", "Bánh nướng", "Bread", "Cream", "Glazed", "Khác"
        };

        public HomeScreen()
        {
            InitializeComponent();
            //calculateCategory();
            categoryList.ItemsSource = categoryListData;
            categoryList.SelectedIndex = 0;

        }
        void calculateCategory()
        {
            categoryListData.Add("Tất cả");
            categoryListData.AddRange(data.GroupBy(e => e.Category).Select(g => g.First().Category));

            //foreach (var i in data)
            //{
            //    categoryListData.Add(i);
            //}
            //for (int i = 0; i < categoryListData.Count; i++)
            //{
            //    for (int j = i + 1; j < categoryListData.Count; j++)
            //    {
            //        if (categoryListData[i].Category == categoryListData[j].Category)
            //        {
            //            categoryListData.RemoveAt(j);
            //        }
            //    }
            //}
        }

        private void categoryList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //int index = categoryList.SelectedIndex;
            //List<Cake> cakeCategory = new List<Cake>();
            //if(index >= 0 && index < data.Count)
            //{
            //    Cake c = data[index];
            //    for(int i = 0; i < data.Count; i++)
            //    {
            //        if (data[i].Category == c.Category)
            //        {
            //            cakeCategory.Add(data[i]);
            //        }
            //    }
            //}
            //cakeList.ItemsSource = cakeCategory;

            ReloadData();
        }

        public void ReloadData()
        {
            int index = categoryList.SelectedIndex;
            List<Cake> cakeCategory = null;
            if (index == 0)
            {
                cakeCategory = data;
            }
            else if (index > 0 && index < categoryListData.Count)
            {
                string category = categoryListData[index];
                cakeCategory = data.Where(c => c.Category == category).ToList();
            }
            //cakeList.ItemsSource = cakeCategory; 
            cakeList.ItemsSource = cakeCategory.ToList();
        }

        private void BuyButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Cake cake = button.DataContext as Cake;
            var qaWindow = new AddCakeWindow();
            if (qaWindow.ShowDialog() == true)
            {
                if (BuyCake != null)
                {
                    BuyCake(this, cake, qaWindow.Answer);
                } else
                {
                    MainWindow mainWindow = Window.GetWindow(this) as MainWindow;
                    mainWindow.ListViewMenu.SelectedItem = mainWindow.ListViewMenu.Items[1];
                    var addScreen = mainWindow.mainGrid.Children[0] as AddScreen.AddScreen;
                    addScreen.BuyCake(null, cake, qaWindow.Answer);
                }
            }
        }

        private void AddNewCakeButton_Click(object sender, RoutedEventArgs e)
        {
            var form = new CakeForm();
            form.OnSubmit = AddCake;
            Uri iconUri = new Uri("pack://application:,,,/Images/shop.ico", UriKind.RelativeOrAbsolute);

            Window window = new Window()
            {
                Title = "Form",
                Content = form,
                Icon = BitmapFrame.Create(iconUri),
                SizeToContent = SizeToContent.Height,
            };
            window.ShowDialog();
        }

        private void AddCake(DependencyObject sender, Cake cake)
        {
            Window.GetWindow(sender).Close();
            var insert = CakeDAO.Insert(cake);
            if (insert == true)
            {
                data.Add(cake);
                int index = categoryListData.IndexOf(cake.Category);
                if (index >= 0 && index != categoryList.SelectedIndex)
                {
                    categoryList.SelectedIndex = index;
                }
                else
                {
                    ReloadData();
                }
            }
        }

        private void cake_SelectionChanged(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var index = cakeList.SelectedIndex;
            if (index >= 0 && index < data.Count)
            {
                Cake c = data[index];
                DetailScreen detail = new DetailScreen(c);
                detail.EndEditing = EndEditing;
                detail.Show();
                //detail.Topmost = true;
                cakeList.UnselectAll();
            }
        }

        private void EndEditing(Cake cake, DetailScreen.EditingStyle style)
        {
            if (style == DetailScreen.EditingStyle.Update)
            {
                var selectedCake = data.FirstOrDefault(e => e.ID == cake.ID);
                if (selectedCake != null)
                {

                    selectedCake.Name = cake.Name;
                    selectedCake.Image = cake.Image;
                    selectedCake.Price = cake.Price;
                    selectedCake.Category = cake.Category;
                    selectedCake.Description = cake.Description;
                }
                ReloadData();
            } else if (style == DetailScreen.EditingStyle.Delete)
            {
                data.Remove(cake);
                ReloadData();
            }
        }
    }
}
