using AnimatedColorfulMenu.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AnimatedColorfulMenu.ViewModel
{
    class LoginViewModel : BaseViewModel
    {
        public static bool loginSuccess = false;

        private string _username;
        public string username
        {
            get => this._username; set
            {
                this._username = value;
                OnPropertyChanged();
            }
        }

        private string _password;
        public string password
        {
            get => new String('●', _password.Length); set
            {
                if (value.Length < this._password.Length)
                {
                    this._password = this._password.Remove(this._password.Length - 1);
                }
                else this._password += value[value.Length - 1];
                OnPropertyChanged();
            }
        }


        private string _tooltip;
        public string tooltip
        {
            get => _tooltip; set
            {
                this._tooltip = value;
                OnPropertyChanged();
            }
        }

        public ICommand login { get; set; }
        public LoginViewModel()
        {
            _password = "";
            username = "";
            login = new RelayCommand<Window>((p) => { return valid(); }, (p) =>
            {
                checkLoginSucces(p);
            });
        }

        private void checkLoginSucces(Window p)
        {
            if (DataProvider.Ins.DB.Accounts.Where(t => t.username == username && t.pass == _password).Count() != 0)
            {
                loginSuccess = true;
                p.Close();
            }
            else MessageBox.Show("Đăng nhập thất bại");
        }



        private bool valid()
        {
            var validUser = new Regex("^[a-z0-9_-]{3,16}$");
            var validPassword = new Regex(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$");
            bool result = true;
            if (!validUser.IsMatch(username))
            {
                tooltip = "Tài khoản từ 3 - 16 ký tự";
                result = false;
            }
            else if (!validPassword.IsMatch(_password))
            {
                tooltip = "Mật khẩu ít nhất có 8 kí tự , bao gồm chữ và số";
                result = false;
            }

            if (result) tooltip = "Có thể đăng nhập";
            return result;
        }
    }
}
