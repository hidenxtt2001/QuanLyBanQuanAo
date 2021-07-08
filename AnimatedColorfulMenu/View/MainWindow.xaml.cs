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
            LoginWindow();
        }

        private void LoginWindow()
        {
            LoginActivity loginActivity = new LoginActivity();
            loginActivity.ShowDialog();
            if (!LoginViewModel.loginSuccess) this.Close();
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
            if (MessageBox.Show("Bạn có muốn thoát khỏi hệ thống?", "Thông báo!", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                this.Close();
            }
            e.Handled = false;
        }


    }
}
