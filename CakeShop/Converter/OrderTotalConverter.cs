using CakeShop.Models;
using OrderShop.Models;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace CakeShop.Converter
{
    class OrderTotalConverter : IValueConverter

    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var list = (BindingList<OrderCake>)value;

            var x = list.Join(CakeDAO.Cache,
                oc => oc.CakeID,
                c => c.ID,
                (oc, c) => oc.Quantity * c.Price
                ).Sum();
            return x;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
