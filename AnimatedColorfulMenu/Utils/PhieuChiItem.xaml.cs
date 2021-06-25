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

        public static readonly DependencyProperty itemPhieuChi = DependencyProperty.Register("resourcePhieuChi", typeof(Resource), typeof(PhieuChiItem), new FrameworkPropertyMetadata
        {
            BindsTwoWayByDefault = true,
        });


        public int numberR
        {
            get { return (int)GetValue(numberResource); }
            set { SetValue(numberResource, value); numberResourcess.Text = value.ToString(); }
        }

        public static readonly DependencyProperty numberResource = DependencyProperty.Register("numberPhieuChi", typeof(int), typeof(PhieuChiItem), new FrameworkPropertyMetadata
        {
            BindsTwoWayByDefault = true,
        });


        public double sumPriceR
        {
            get { return (double)GetValue(sumPriceResource); }
            set { SetValue(sumPriceResource, value); priceResource.Text = string.Format("{0} VNĐ", value); }
        }

        public static readonly DependencyProperty sumPriceResource = DependencyProperty.Register("sumPricePhieuChi", typeof(double), typeof(PhieuChiItem), new FrameworkPropertyMetadata
        {
            BindsTwoWayByDefault = true,
        });

        public PhieuChiItem()
        {

            InitializeComponent();
            this.DataContext = this;
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
