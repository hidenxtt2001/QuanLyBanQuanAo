using AnimatedColorfulMenu.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimatedColorfulMenu.ViewModel
{

    class ThongKeViewModel : BaseViewModel
    {
        private ObservableCollection<Payment> _payments;
        public ObservableCollection<Payment> payments
        {
            get => this._payments; set
            {
                this._payments = value;
                OnPropertyChanged();

            }
        }

        private ObservableCollection<Bill> _bills;
        public ObservableCollection<Bill> bills
        {
            get => this._bills; set
            {
                this._bills = value;
                OnPropertyChanged();

            }
        }
        public ThongKeViewModel()
        {
            payments = new ObservableCollection<Payment>();
            bills = new ObservableCollection<Bill>();
            loadDataFunc();
        }

        public void loadDataFunc()
        {
            loadPayment();
            loadBill();
        }

        #region Chi Tieu
        private double _tongthu;
        public double tongthu
        {
            get => this._tongthu; set
            {
                this._tongthu = value;
                doanhthu = tongthu - tongchi;
                OnPropertyChanged();
            }
        }

        private double _tongchi;
        public double tongchi
        {
            get => this._tongchi; set
            {
                this._tongchi = value;
                doanhthu = tongthu - tongchi;
                OnPropertyChanged();
            }
        }

        private double _doanhthu;
        public double doanhthu
        {
            get => this._doanhthu; set
            {
                this._doanhthu = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Filtter

        private int _day;
        public int day
        {
            get => this._day; set
            {
                this._day = value;
                loadDataFunc();
                OnPropertyChanged();
            }
        }

        private int _month;
        public int month
        {
            get => this._month; set
            {
                this._month = value;
                loadDataFunc();
                OnPropertyChanged();
            }
        }

        private int _year;
        public int year
        {
            get => this._year; set
            {
                this._year = value;
                loadDataFunc();
                OnPropertyChanged();
            }
        }

        private int _dayNext;
        public int dayNext
        {
            get => this._dayNext; set
            {
                this._dayNext = value;
                loadDataFunc();
                OnPropertyChanged();
            }
        }

        private int _monthNext;
        public int monthNext
        {
            get => this._monthNext; set
            {
                this._monthNext = value;
                loadDataFunc();
                OnPropertyChanged();
            }
        }

        private int _yearNext;
        public int yearNext
        {
            get => this._yearNext; set
            {
                this._yearNext = value;
                loadDataFunc();
                OnPropertyChanged();
            }
        }


        #endregion

        public void loadPayment()
        {
            payments.Clear();
            tongchi = 0;
            try
            {

                var date = DateTime.Parse(string.Format("{0}/{1}/{2} 0:00 AM", month, day, year));
                var dateNext = DateTime.Parse(string.Format("{0}/{1}/{2} 23:59 PM", monthNext, dayNext, yearNext));
                var k = DataProvider.Ins.DB.Payments.Where(t => t.dateCreate >= date && t.dateCreate <= dateNext).ToList();
                foreach (Payment i in k)
                {
                    tongchi += i.sumPrice;
                    payments.Add(i);
                }
            }
            catch
            {

            }


        }

        public void loadBill()
        {
            bills.Clear();
            tongthu = 0;
            try
            {
                var date = DateTime.Parse(string.Format("{0}/{1}/{2} 0:00 AM", month, day, year));
                var dateNext = DateTime.Parse(string.Format("{0}/{1}/{2} 23:59 PM", monthNext, dayNext, yearNext));
                var k = DataProvider.Ins.DB.Bills.Where(t => t.dateCreate >= date && t.dateCreate <= dateNext).ToList();
                foreach (Bill i in k)
                {
                    tongthu += i.sumPrice;
                    bills.Add(i);
                }
            }
            catch
            {

            }

        }
    }
}
