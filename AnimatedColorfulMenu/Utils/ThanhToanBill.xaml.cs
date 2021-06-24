using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for ThanhToanBill.xaml
    /// </summary>
    public partial class ThanhToanBill : UserControl
    {
        public ThanhToanBill()
        {
            InitializeComponent();
        }
        public void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            if (((TextBox)sender).Text.Length > 11)
            {
                e.Handled = true;
                return;
            }


            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
