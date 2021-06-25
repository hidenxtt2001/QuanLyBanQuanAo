using AnimatedColorfulMenu.Model;
using AnimatedColorfulMenu.Utils;
using AnimatedColorfulMenu.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AnimatedColorfulMenu.ViewModel
{
    class LapHoaDonViewModel : MainViewModel
    {

        private ObservableCollection<BillItem> _billItem;
        public ObservableCollection<BillItem> billItems
        {
            get => this._billItem;
            set
            {
                this._billItem = value;
                OnPropertyChanged();

            }
        }

        public List<Product> listProduct;

        public ICommand selectProduct { get; set; }

        public ICommand deleteBill { get; set; }

        public ICommand thanhToanBill { get; set; }

        public ICommand huyThanhToan { get; set; }

        public LapHoaDonViewModel()
        {
            listProduct = new List<Product>();
            billItems = new ObservableCollection<BillItem>();
            selectProduct = new RelayCommand<Product>((p) => { return true; }, (p) =>
            {
                if (!listProduct.Contains(p))
                {
                    listProduct.Add(p);
                    billItems.Add(new BillItem() { productBill = p });

                }

            });
            deleteBill = new RelayCommand<BillItem>((p) => { return true; }, (p) =>
            {
                billItems.Remove(p);
                listProduct.Remove(p.productBill);
            });

            thanhToanBill = new RelayCommand<Object>((p) => { return validThanhToan(); }, (p) =>
            {
                thanhToan();
            });


            huyThanhToan = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                resetThanhToan();
            });
            resetThanhToan();

        }

        private string _phoneNumber;
        public string phoneNumber
        {
            get => this._phoneNumber;
            set
            {
                this._phoneNumber = value;
                OnPropertyChanged();
            }
        }

        private string _nameCustomer;
        public string nameCustomer
        {
            get => this._nameCustomer;
            set
            {
                this._nameCustomer = value;
                OnPropertyChanged();
            }
        }

        private bool validThanhToan()
        {
            return billItems.Count != 0 && nameCustomer.Trim() != string.Empty;
        }


        private void resetThanhToan()
        {
            phoneNumber = "";
            nameCustomer = "";
            billItems.Clear();
            listProduct.Clear();
        }

        private void thanhToan()
        {
            double sum = 0;
            foreach (var i in billItems)
            {
                sum += i.number * i.productBill.price;
            }

            if (MessageBox.Show(string.Format("Tổng tiền : {0} VNĐ", sum), "Thanh toán hoá đơn", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    var bill = new Bill() { dateCreate = DateTime.Now, nameCustomer = nameCustomer, phoneNumber = phoneNumber, sumPrice = sum };
                    DataProvider.Ins.DB.Bills.Add(bill);

                    foreach (var i in billItems)
                    {
                        BillDetail billDetail = new BillDetail() { billID = bill.billID, numberProduct = i.number, productID = i.productBill.productID, sumPrice = i.sumPrice };
                        DataProvider.Ins.DB.BillDetails.Add(billDetail);
                    }

                    DataProvider.Ins.DB.SaveChanges();
                    resetThanhToan();
                    MessageBox.Show("Thanh toán thành công");
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }

        }

    }
}
