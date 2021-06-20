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

namespace AnimatedColorfulMenu
{
    /// <summary>
    /// Interaction logic for ProductItem.xaml
    /// </summary>
    public partial class ProductItem : UserControl
    {

        private Product product;
        public ProductItem(Product product)
        {
            InitializeComponent();
            this.product = product;
        }

        private void Product_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("CLick item");
        }
    }
}
