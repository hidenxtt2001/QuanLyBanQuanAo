using AnimatedColorfulMenu.Model;
using AnimatedColorfulMenu.ViewModel;
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

namespace AnimatedColorfulMenu.Utils
{
    /// <summary>
    /// Interaction logic for BillItem.xaml
    /// </summary>
    public partial class BillItem : UserControl
    {
        public Product productBill
        {
            get { return (Product)GetValue(item); }
            set { SetValue(item, value); sumPrice = number * productBill.price; }
        }

        public static readonly DependencyProperty item = DependencyProperty.Register("productBill", typeof(Product), typeof(BillItem));


        public int number
        {
            get { return (int)GetValue(numberProduct); }
            set { SetValue(numberProduct, value); }
        }

        public static readonly DependencyProperty numberProduct = DependencyProperty.Register("number", typeof(int), typeof(BillItem));

        public double sumPrice
        {
            get { return (double)GetValue(sumPriceProduct); }
            set { SetValue(sumPriceProduct, value); }
        }

        public static readonly DependencyProperty sumPriceProduct = DependencyProperty.Register("sumPrice", typeof(double), typeof(BillItem));

        public BillItem()
        {
            InitializeComponent();
            number = 1;
            sumPrice = 0;

        }

        private void changeNumber_Click(object sender, RoutedEventArgs e)
        {
            number = ((Button)sender).Name == "plus" ? number + 1 : number - 1 < 1 ? 1 : number - 1;
            sumPrice = number * productBill.price;
        }
    }
}
