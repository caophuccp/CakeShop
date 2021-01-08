using System;
using System.Windows;

namespace CakeShop.View.AddScreen
{
    /// <summary>
    /// Interaction logic for AddCakeWindow.xaml
    /// </summary>
    public partial class AddCakeWindow : Window
    {

        public int Answer { get; set; } = 0;
        //private Regex numberRegex = new Regex(@"^[0-9]+$");

        public AddCakeWindow()
        {
            InitializeComponent();
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            AnswerHLTextBox.MainTextBox.SelectAll();
            AnswerHLTextBox.MainTextBox.Focus();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            int answer;
            if (int.TryParse(AnswerHLTextBox.Text, out answer) && answer > 0) {
                Answer = answer;
                DialogResult = true;
            } else
            {
                AnswerHLTextBox.HasError = true;
            }
        }
    }
}
