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
using System.Text.RegularExpressions;
using System.IO;
using Microsoft.Win32;
using CakeShop.Models;
using CakeShop.View.AddScreen;

namespace CakeShop.View
{
    /// <summary>
    /// Interaction logic for DetailScreen.xaml
    /// </summary>
    public partial class DetailScreen : Window
    {
        Cake SelectedCake { get; set; }

        public DetailScreen()
        {
            InitializeComponent();
        }

        public DetailScreen(Cake c)
        {
            InitializeComponent();
            SelectedCake = c;
            //cakeName.Content = SelectedCake.Name;
            //cakeDescription.Text = SelectedCake.Description;
            cakeDetail.DataContext = SelectedCake;
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            var msgResult = MessageBox.Show("Bạn chắc chắn muốn xoá - " + SelectedCake.Name, "Cake Shop", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (msgResult == MessageBoxResult.Yes)
            {
                var del = CakeDAO.Delete(SelectedCake.ID);
                if (del != 0)
                {
                    EndEditing.Invoke(SelectedCake, EditingStyle.Delete);
                    Close();
                }
                else
                {
                    MessageBox.Show("Xoá thất bại!\nVui lòng thử lại sau", "Cake Shop", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var form = new CakeForm(SelectedCake);
            form.OnSubmit = UpdateCake;
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

        private void UpdateCake(DependencyObject sender, Cake cake)
        {
            if (sender != null)
            {
                GetWindow(sender).Close();
            }
            cake.ID = SelectedCake.ID;
            var update = CakeDAO.Update(cake);
            if (update == true)
            {
                SelectedCake = cake;
                cakeDetail.DataContext = SelectedCake;
                EndEditing.Invoke(SelectedCake, EditingStyle.Update);
            }
            else
            {
                MessageBox.Show("Lưu thất bại!\nVui lòng thử lại sau", "Cake Shop", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public delegate void EndEditingDelegate(Cake cake, EditingStyle style);

        public EndEditingDelegate EndEditing;
        public enum EditingStyle
        {
            None,
            Update,
            Delete,
        }
    }
}