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
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Appbar(object sender, MouseButtonEventArgs e)
        {
            if (e.MouseDevice.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void Navbar_Selected(object sender, RoutedEventArgs e)
        {
            switch (((ListViewItem)sender).Tag.ToString())
            {
                case "main":
                    formPanel.SelectedIndex = 0;
                    break;
                case "product":
                    formPanel.SelectedIndex = 1;
                    break;
                case "bill":
                    formPanel.SelectedIndex = 2;
                    break;
                case "payment":
                    formPanel.SelectedIndex = 3;
                    break;
                case "statistics":
                    formPanel.SelectedIndex = 4;
                    break;
            }
        }

        private void CloseForm_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


    }
}
