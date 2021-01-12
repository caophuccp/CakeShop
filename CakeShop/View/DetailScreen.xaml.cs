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
    }
}