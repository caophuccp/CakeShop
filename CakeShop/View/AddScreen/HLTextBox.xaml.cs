using System.Windows;
using System.Windows.Controls;

namespace CakeShop.View.AddScreen
{
    /// <summary>
    /// Interaction logic for HLTextBox.xaml
    /// </summary>
    public partial class HLTextBox : UserControl
    {

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text",
            typeof(string),
            typeof(HLTextBox),
            new PropertyMetadata(null)
        );

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty PlaceholderProperty = DependencyProperty.Register(
            "Placeholder",
            typeof(string),
            typeof(HLTextBox),
            new PropertyMetadata(null)
        );

        public string Placeholder
        {
            get { return (string)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
            "Title",
            typeof(string),
            typeof(HLTextBox),
            new PropertyMetadata(null)
        );

        public bool HasError
        {
            get { return (bool)GetValue(HasErrorProperty); }
            set { SetValue(HasErrorProperty, value); }
        }

        public static readonly DependencyProperty HasErrorProperty = DependencyProperty.Register(
            "HasError",
            typeof(bool),
            typeof(HLTextBox),
            new PropertyMetadata(false)
        );

        public string ErrorMessage
        {
            get { return (string)GetValue(ErrorMessageProperty); }
            set { SetValue(ErrorMessageProperty, value); }
        }

        public static readonly DependencyProperty ErrorMessageProperty = DependencyProperty.Register(
            "ErrorMessage",
            typeof(string),
            typeof(HLTextBox),
            new PropertyMetadata(null)
        );

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public HLTextBox()
        {
            InitializeComponent();
        }

        public void Clear()
        {
            HasError = false;
            Text = null;
        }
    }
}
