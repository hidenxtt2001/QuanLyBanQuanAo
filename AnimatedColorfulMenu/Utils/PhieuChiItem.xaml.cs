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
    /// Interaction logic for PhieuChiItem.xaml
    /// </summary>
    public partial class PhieuChiItem : UserControl
    {

        public Resource resourcePhieuChi
        {
            get { return (Resource)GetValue(itemPhieuChi); }
            set
            {

                SetValue(itemPhieuChi, value);
                sumPriceR = numberR * resourcePhieuChi.price;
            }
        }

        public static readonly DependencyProperty itemPhieuChi = DependencyProperty.Register("resourcePhieuChi", typeof(Resource), typeof(PhieuChiItem));


        public int numberR
        {
            get { return (int)GetValue(numberResource); }
            set { SetValue(numberResource, value); }
        }

        public static readonly DependencyProperty numberResource = DependencyProperty.Register("numberPhieuChi", typeof(int), typeof(PhieuChiItem));


        public double sumPriceR
        {
            get { return (double)GetValue(sumPriceResource); }
            set { SetValue(sumPriceResource, value); }
        }

        public static readonly DependencyProperty sumPriceResource = DependencyProperty.Register("sumPricePhieuChi", typeof(double), typeof(PhieuChiItem));

        public PhieuChiItem()
        {

            InitializeComponent();
            sumPriceR = 0;
            numberR = 1;
        }

        private void changeNumber_Click(object sender, RoutedEventArgs e)
        {
            numberR = ((Button)sender).Name == "plus" ? numberR + 1 : numberR - 1 < 1 ? 1 : numberR - 1;
            sumPriceR = numberR * resourcePhieuChi.price;
        }
    }
}
