using AnimatedColorfulMenu.Model;
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
    class MainViewModel : BaseViewModel
    {
        private int tempSeleted;

        private int _tabSelected;
        public int tabSelected
        {
            get => this._tabSelected; set
            {
                tempSeleted = _tabSelected;
                this._tabSelected = value;
                OnPropertyChanged("tabSelected");
            }
        }

        private ObservableCollection<Product> _products;
        public ObservableCollection<Product> products
        {
            get => this._products; set
            {
                this._products = value;
                OnPropertyChanged();

            }
        }

        public ICommand tabSwitch { get; set; }

        public ICommand loadData { get; set; }

        public MainViewModel()
        {
            products = new ObservableCollection<Product>();
            loadData = new RelayCommand<MainWindow>((p) =>
            {
                return true;
            }, (p) =>
            {
                if (tempSeleted != tabSelected)
                {
                    switch (tabSelected)
                    {
                        case 0:
                            ((MainViewModel)p.main.DataContext).loadDataFunc();
                            break;
                        case 1:
                            ((SanPhamViewModel)p.product.DataContext).loadDataFunc();
                            break;
                        case 2:
                            ((PhieuChiViewModel)p.payment.DataContext).loadDataFunc();
                            break;
                        case 3:
                            ((ThongKeViewModel)p.statistics.DataContext).loadDataFunc();
                            break;
                    }
                    tempSeleted = tabSelected;
                }


            });
            tabSwitch = new RelayCommand<int>((p) =>
            {
                return true;
            }, (p) =>
            {
                tabSelected = p;
                loadProduct();
            });
            loadProduct();
        }

        public void loadDataFunc()
        {
            loadProduct();
        }

        public void loadProduct()
        {
            products.Clear();
            foreach (Product i in DataProvider.Ins.DB.Products)
            {
                products.Add(i);
            }

        }

        public void addProduct(Product product)
        {
            DataProvider.Ins.DB.Products.Add(product);
            DataProvider.Ins.DB.SaveChanges();
            loadProduct();
        }

        public void deleteProduct(Product selectProduct)
        {
            Product cust = (from c in DataProvider.Ins.DB.Products where c.productID == selectProduct.productID select c).FirstOrDefault();
            DataProvider.Ins.DB.Products.Remove(cust);
            DataProvider.Ins.DB.SaveChanges();
            loadProduct();
        }



    }
}
