using CakeShop.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for AddCakeForm.xaml
    /// </summary>
    public partial class CakeForm : UserControl
    {
        public delegate void OnSubmitDelegate(DependencyObject sender, Cake cake);
        public OnSubmitDelegate OnSubmit;

        public static string DefaultImage = "Images/dessert.png";
        private string ImageString = "";
        private List<string> Categories = new List<string>()
        {
            "Bánh dừa", "Bánh nướng", "Bread", "Cream", "Glazed", "Khác"
        };

        public CakeForm()
        {
            InitializeComponent();
            Setup();

            var path = AppDomain.CurrentDomain.BaseDirectory + DefaultImage;
            LoadImage(path);
        }

        public CakeForm(Cake cake)
        {
            InitializeComponent();
            Setup();

            var path = AppDomain.CurrentDomain.BaseDirectory + cake.Image;
            LoadImage(path);

            ImageString = cake.Image;
            NameTextBox.Text = cake.Name;
            PriceTextBox.Text = cake.Price.ToString();
            DescriptionTextBox.Text = cake.Description;
            SubmitButton.Content = "Lưu";
            TitleTextBlock.Text = "Thay đổi thông tin";
        }

        private void Setup()
        {
            CategoryComboBox.ItemsSource = Categories;
            DescriptionTextBox.MainTextBox.AcceptsReturn = true;
            DescriptionTextBox.MainTextBox.TextWrapping = TextWrapping.Wrap;
        }

        private void LoadImage(string image)
        {
            CakeImage.Source = new BitmapImage(new Uri(image, UriKind.Absolute));
        }

        private void EditImageButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.DefaultExt = ".png";
            dialog.Filter = "All Images Files (*.png;*.jpeg;*.gif;*.jpg;*.bmp;*.tiff;*.tif)|*.png;*.jpeg;*.gif;*.jpg;*.bmp;*.tiff;*.tif"
                                + "|PNG Portable Network Graphics (*.png)|*.png"
                                + "|JPEG File Interchange Format (*.jpg *.jpeg *jfif)|*.jpg;*.jpeg;*.jfif"
                                + "|BMP Windows Bitmap (*.bmp)|*.bmp"
                                + "|TIF Tagged Imaged File Format (*.tif *.tiff)|*.tif;*.tiff"
                                + "|GIF Graphics Interchange Format (*.gif)|*.gif";
            if (true == dialog.ShowDialog())
            {
                ImageString = dialog.FileName;
                LoadImage(ImageString);
            }
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            int price = 0;

            NameTextBox.HasError = string.IsNullOrEmpty(NameTextBox.Text);
            PriceTextBox.HasError = !int.TryParse(PriceTextBox.Text, out price);
            DescriptionTextBox.HasError = string.IsNullOrEmpty(DescriptionTextBox.Text);

            if (!(NameTextBox.HasError || DescriptionTextBox.HasError || PriceTextBox.HasError))
            {
                ImageString = string.IsNullOrEmpty(ImageString) ? DefaultImage : CopyImage(ImageString);
                string category = Categories[CategoryComboBox.SelectedIndex];
                Cake cake = new Cake()
                {
                    Name = NameTextBox.Text,
                    Price = price,
                    Description = DescriptionTextBox.Text,
                    Image = ImageString,
                    Category = category,
                };
                OnSubmit?.Invoke(this, cake);
            }
        }

        private string CopyImage(string filename)
        {
            string imageName = System.IO.Path.GetFileName(filename);
            string imageRP = "Images/" + imageName;
            string imagePath = AppDomain.CurrentDomain.BaseDirectory + imageRP;
            var prefix = 0;

            while (File.Exists(imagePath))
            {
                prefix += 1;
                imageRP = "Images/i" + prefix + imageName;
                imagePath = AppDomain.CurrentDomain.BaseDirectory + imageRP;
            }
            File.Copy(filename, imagePath);
            return imageRP;
        }
    }
}
