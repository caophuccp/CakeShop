using CakeShop.Models;
using OrderShop.Models;
using System;
using System.Collections.Generic;
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
using System.Diagnostics;
using LiveCharts;
using LiveCharts.Wpf;
using System.Globalization;

namespace CakeShop.View
{
    /// <summary>
    /// Interaction logic for Statistic.xaml
    /// </summary>
    public partial class Statistic : UserControl
    {
        public class mMonth
        {
            public string Name { get; set; }
            public double Amount { get; set; }
        }
        public class mCake
        {
            public string Name { get; set; }
            public double Amount { get; set; }
        }

        List<mMonth> moneyPerMonth = new List<mMonth>();
        List<mCake> moneyPerCake = new List<mCake>();
        List<Cake> listCake = CakeDAO.GetAll();
        List<Order> listOrder = OrderDAO.GetAll();

        void getAmount()
        {
            // Amount Month
            mMonth tempm;
            for (int i = 1; i < 13; i++)
            {
                tempm = new mMonth();
                tempm.Name = DateTimeFormatInfo.CurrentInfo.GetMonthName(i);
                tempm.Amount = 0;
                foreach (Order o in listOrder)
                {
                   if(i == o.DateCreated.Month && o.DateCreated.Year == 2020)
                    {
                        tempm.Amount += o.Total;
                    }
                }
                moneyPerMonth.Add(tempm);
            }

            // Amount Cake
            mCake temp;
            foreach (Cake m in listCake)
            {
                temp = new mCake();
                temp.Name = m.Name;
                temp.Amount = 0;
                foreach (Order o in listOrder)
                {
                    foreach (OrderCake oc in o.Products)
                    {
                        if (oc.CakeID == m.ID)
                        {
                            temp.Amount += m.Price * oc.Quantity;
                        }
                    }
                }
                moneyPerCake.Add(temp);
            }
        }

        public Statistic()
        {
            InitializeComponent();
            getAmount();
            drawCharts();
            
        }

        void drawCharts()
        {
            monthChart.Series = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "",
                    Values = new ChartValues<double> { moneyPerMonth[0].Amount, moneyPerMonth[1].Amount, moneyPerMonth[2].Amount, moneyPerMonth[3].Amount, moneyPerMonth[4].Amount, moneyPerMonth[5].Amount, moneyPerMonth[6].Amount, moneyPerMonth[7].Amount, moneyPerMonth[8].Amount, moneyPerMonth[9].Amount, moneyPerMonth[10].Amount, moneyPerMonth[11].Amount, }
                }
                
            };

            axx.Labels = new[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };
            //monthChart.AxisX.Add(new Axis
            //{
            //    Labels = new[] { "1","2","3","4","5","6","7","8","9","10","11","12" },
            //});

                cakeChart.Series = new LiveCharts.SeriesCollection();
            foreach(mCake mc in moneyPerCake)
            {
                PieSeries xc = new PieSeries
                {
                    Title = mc.Name,
                    Values = new ChartValues<double> {mc.Amount},
                    DataLabels = true,
                };
                cakeChart.Series.Add(xc);
            }
        }
    }
}
