using AnimatedColorfulMenu.Model;
using AnimatedColorfulMenu.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public ICommand selectProduct { get; set; }

        public ICommand deleteBill { get; set; }

        public LapHoaDonViewModel()
        {
            billItems = new ObservableCollection<BillItem>();
            selectProduct = new RelayCommand<Product>((p) => { return true; }, (p) => { billItems.Add(new BillItem() { productBill = p }); });
            deleteBill = new RelayCommand<BillItem>((p) => { return true; }, (p) => { billItems.Remove(p); });
        }


    }
}
