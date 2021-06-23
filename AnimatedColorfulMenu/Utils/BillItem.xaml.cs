using AnimatedColorfulMenu.Model;
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
            set { SetValue(item, value); }
        }

        public static readonly DependencyProperty item = DependencyProperty.Register("productBill", typeof(Product), typeof(BillItem));

        public int number { get; set; }

        public BillItem()
        {
            InitializeComponent();
        }
    }
}
