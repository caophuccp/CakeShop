using CakeShop.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CakeShop
{
    /// <summary>
    /// Interaction logic for SplashScreen.xaml
    /// </summary>
    public partial class SplashScreen : Window
    {
        public Random _rng = new Random();
        System.Timers.Timer timer;
        int count = 0;
        int target = 5;
        public SplashScreen()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var value = ConfigurationManager.AppSettings["ShowSplashScreen"];
            var showSplash = bool.Parse(value);
            Debug.WriteLine(value);

            if (showSplash == false)
            {
                CloseSplashScreen();
            }
            else
            {
                timer = new System.Timers.Timer();
                timer.Elapsed += Timer_Elapsed;
                timer.Interval = 1000;

                int index = _rng.Next(InfomationList.Length);
                var infomation = InfomationList[index];
                FoodInformation.Text = infomation;

                SplashOKBtn.Content = $"OK ({target})";
                timer.Start();
            }
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            count++;
            Dispatcher.Invoke(() =>
            {
                SplashOKBtn.Content = $"OK ({target - count})";
            });
            if (count == target)
            {
                timer.Stop();

                Dispatcher.Invoke(() =>
                {
                    CloseSplashScreen();
                });
            }
        }

        private void DontShowThisAgain(object sender, RoutedEventArgs e)
        {
            var config = ConfigurationManager.OpenExeConfiguration(
                ConfigurationUserLevel.None);
            config.AppSettings.Settings["ShowSplashScreen"].Value = "false";
            config.Save(ConfigurationSaveMode.Minimal);
        }

        private void ShowThisAgain(object sender, RoutedEventArgs e)
        {
            var config = ConfigurationManager.OpenExeConfiguration(
                ConfigurationUserLevel.None);
            config.AppSettings.Settings["ShowSplashScreen"].Value = "true";
            config.Save(ConfigurationSaveMode.Minimal);
        }

        string[] InfomationList =
        {
            "Chesse Cake là loại bánh ngọt được làm chủ yếu từ phô mai, tạo vị béo ngậy. Phía trên người ta có thể phủ thêm mứt. Chiếc bánh pho mát kem được làm từ những năm 1800 và trở thành một trong những món bánh quen thuộc của người dân New York.",
            "Tiramisu là loại bánh ngọt vô cùng quen thuộc tại Việt Nam nhưng không phải ai cũng biết rõ về chúng. Tiramisu là loại bánh ngọt tráng miệng vị cà phê rất nổi tiếng có nguồn gốc từ Italy. Bánh gồm các lớp bánh quy Savoiardi nhúng cà phê xen kẽ với hỗn hợp trứng, đường, phô mai mascarpone đánh bông, bột cacao.",
            "Black Forest là một kiểu bánh có nguồn gốc từ Đức với tên gốc là Schwarzwälder Kirschtorte. Nó bao gồm nhiều lớp bánh chocolate sponge, phủ váng sữa đánh tơi, maraschino cherry và chocolate bào mỏng.",
            "Victoria Sponge là loại bánh được đặt theo tên của nữ hoàng Anh Victoria. Một chiếc Victoria Sponge truyền thống gồm mứt và lượng kem nhiều gấp đôi. Bánh thường được dùng trong tiệc trà chiều của người Anh.",
            "Sachertorte là một loại bánh được tạo ra bởi chocolate tuyệt hảo nhất nước Áo. Bánh có vị ngọt dịu nhẹ, gồm nhiều lớp bánh được làm từ bánh mì và bánh sữa chocolate, xen lẫn giữa các lớp bánh là mứt mơ.",
            "Vẻ ngoài của Swedish Princess ngọt ngào như chính tên gọi của mình. Đây là món bánh ngọt truyền thống của Thụy Điển được rất nhiều người yêu thích. Ban đầu, bánh được làm để phục vụ hoàng gia, được làm từ mứt, trứng, sữa, kem và cốt bánh bông lan, bao phủ phía trên là lớp bánh hạnh nhân (thường có màu xanh).",
            "Lamington là chiếc bánh mang theo cả niềm tự hào của người Australia. Bánh bao gồm lớp ruột tơi xốp, mềm mịn làm từ bột và trứng, được phủ chocolate bóng bẩy bên ngoài và rắc dừa xung quanh.",
            "Madeleines là món ăn đơn giản, quen thuộc nhưng rất được yêu thích của người Pháp và thường được gọi với tên bánh con sò, được làm từ bột, đường, bơ và sữa. Người ta thường tôn vinh loại bánh này với tên gọi 'nàng thơ trong các bữa tiệc trà của nước Pháp'.",
            "Mochi là món ăn truyền thống trong ngày Tết được người Nhật yêu thích, món bánh này tượng trưng cho sự may mắn và thịnh vượng trong năm mới. Nhân bánh có thể được làm từ đậu đổ, đậu trắng hoặc dâu tây hay một số loại hoa quả khác kết hợp với đậu đỏ.",
            "Dorayaki nghe có vẻ khá lạ lẫm nhưng nếu nói Bánh rán thì đây lại là món bánh vô cùng quen thuộc với các fan truyện tranh tại Việt Nam. Món bánh rán này đã trở thành huyền thoại đối với các fan của chú mèo máy Doraemon trên toàn thế giới.",
            "Macaron là một loại bánh ngọt của Pháp, bánh được làm từ lòng trắng trứng, đường bột, đường cát, bột hạnh nhân và một ít phẩm màu tự nhiên. Nhân bánh thường là mứt, chocolate hoặc kem bơ kẹp ở giữa.",
            "Bánh Pandan còn có tên gọi khác là bánh bông lan lá dứa. Bánh có màu xanh tự nhiên đẹp mắt, mùi thơm của lá dứa và mềm mịn, tơi xốp nhưng cũng không quá ngọt. Đây là món bánh rất nổi tiếng ở Singapore, thường được nhiều du khách mua về làm quà.",
            "Tapioca là một món ăn có mặt khắp phố phường của Brazil. Bánh được làm với lớp bột mì cán mỏng nướng giòn, khi ăn sẽ kẹp với kem, chuối, pho mát, chocolate. Khi cho một miếng Tapioca vào miệng, bạn sẽ cảm nhận được một hương vị rất lạ với phần nhân bánh rất mềm, vỏ bánh cùng vị chocolate dịu nhẹ.",
            "Bánh Pavlova được đặt theo tên của vũ công ballet hàng đầu của nước Nga, Anna Pavlova. Món bánh được tạo ra để vinh danh cô khi Pavlova đi lưu diễn ở Australia và New Zealand trong những năm 1920. Loại bánh này được làm từ lòng trắng trứng đánh bông với đường, bên ngoài là lớp vỏ cứng nhưng xốp, bên trong là lớp kẹo dẻo.",
            "Bánh táo được xem là biểu tượng của nền văn hóa ẩm thực Mỹ, thể hiện sự thịnh vượng và là niềm tự hào trong suốt những năm của thế kỷ 19 và 20 của đất nước này. Bánh táo với phần vỏ bánh mỏng, giòn mềm, ẩn chứa phần nhân táo thơm ngọt, điểm chút vị chua dịu của trái cây quả sẽ là một lựa chọn hoàn hảo cho những tín đồ bánh ngọt trên toàn thế giới.",
            "Gateau St. Honore, món bánh ngọt tráng miệng nhẹ nhàng này được làm từ những chiếc bánh giống như bánh choux nhúng chocolate được phủ đầy kem tươi đánh và caramel. Gateau St. Honore được đặt theo tên một vị thánh nghề bánh và có xuất sứ từ Pháp nhưng bạn hãy thử loại bánh này ở Bỉ vì nó rất ngon.",
            "Bánh Limburg Pie Hà Lan, những chiếc bánh nhân trái cây đến từ miền Nam Hà Lan này có kết cấu mềm, đơn giản và thường làm từ hỗn hợp trứng, sữa và một ít bánh quy. Vỏ bánh Limburg Pie không giòn. Nó hơi giống với bánh mì nhưng vẫn cho ta một hương vị rất cao cấp.",
            "Carac, chiếc bánh carac của Thụy Sĩ đặc biệt hấp dẫn và sang trọng và là một mặt hàng nổi bật trên hầu hết các cửa hiệu bánh ngọt ở Thụy Sĩ. Vỏ của chiếc bánh tart nhỏ bé này được làm từ chocolate ganache đen, hạt hạnh nhân nghiền nhuyễn trong khi bề mặt của chiếc bánh được phủ một lớp đường đông lạnh màu xanh ngọc rất bắt mắt."
        };

        private void CloseSplashScreen()
        {
            var screen = new MainWindow();
            this.Close();
            screen.Show();
        }

        private void CloseSplashScreenBtn(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            CloseSplashScreen();
        }
    }
}

