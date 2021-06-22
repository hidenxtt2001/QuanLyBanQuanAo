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
using System.Windows.Shapes;

namespace AnimatedColorfulMenu.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void DockPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.MouseDevice.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void CloseForm_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Navbar_Selected(object sender, RoutedEventArgs e)
        {
            switch (((ListViewItem)sender).Tag.ToString())
            {
                case "main":
                    this.formPanel.SelectedIndex = 0;
                    break;
                case "product":
                    this.formPanel.SelectedIndex = 1;
                    break;
                case "bill":
                    this.formPanel.SelectedIndex = 2;
                    break;
                case "payment":
                    this.formPanel.SelectedIndex = 3;
                    break;
                case "statistics":
                    this.formPanel.SelectedIndex = 4;
                    break;
            }
        }
    }
}
