using AnimatedColorfulMenu.Model;
using AnimatedColorfulMenu.Utils;
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
    class PhieuChiViewModel : BaseViewModel
    {
        private ObservableCollection<Resource> _resources;
        public ObservableCollection<Resource> resources
        {
            get => this._resources;
            set
            {
                this._resources = value;
                OnPropertyChanged();

            }
        }

        private ObservableCollection<PhieuChiItem> _phieuChiItems;
        public ObservableCollection<PhieuChiItem> phieuChiItems
        {
            get => this._phieuChiItems;
            set
            {
                this._phieuChiItems = value;
                OnPropertyChanged();

            }
        }

        public List<Resource> listSelect;

        private Resource _selectResource;
        public Resource selectResource
        {
            get => this._selectResource;
            set
            {
                this._selectResource = value;
                OnPropertyChanged();

            }
        }

        public ICommand addResource { get; set; }

        public ICommand deleteResource { get; set; }


        public ICommand themPhieuChi { get; set; }

        public ICommand xoaPhieuChi { get; set; }
        public PhieuChiViewModel()
        {
            resources = new ObservableCollection<Resource>();
            phieuChiItems = new ObservableCollection<PhieuChiItem>();
            addResource = new RelayCommand<TextBox>((p) =>
            {
                return selectResource != null && !listSelect.Contains(selectResource);
            }, (p) =>
            {
                listSelect.Add(selectResource);
                phieuChiItems.Add(new PhieuChiItem() { resourcePhieuChi = selectResource });
            });
            deleteResource = new RelayCommand<PhieuChiItem>((p) => { return true; }, (p) =>
            {
                listSelect.Remove(p.resourcePhieuChi);
                phieuChiItems.Remove(p);

            });
            themPhieuChi = new RelayCommand<Object>((p) => { return phieuChiItems.Count != 0; }, (p) => { phieuChi(); });
            xoaPhieuChi = new RelayCommand<Object>((p) => { return true; }, (p) => { resetPhieuChi(); });
            loadResource();
        }


        public void loadDataFunc()
        {
            loadResource();
        }

        private void loadResource()
        {
            resources.Clear();
            listSelect = new List<Resource>();
            foreach (Resource i in DataProvider.Ins.DB.Resources)
            {
                resources.Add(i);
            }
        }

        private void phieuChi()
        {
            double sum = 0;
            foreach (var i in phieuChiItems)
            {
                sum += i.sumPriceR;
            }

            if (MessageBox.Show(string.Format("Tổng tiền : {0} VNĐ", sum), "Thêm phiếu chi", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    var payment = new Payment() { dateCreate = DateTime.Now, sumPrice = sum };
                    DataProvider.Ins.DB.Payments.Add(payment);

                    foreach (var i in phieuChiItems)
                    {
                        PaymentDetail paymentDetail = new PaymentDetail() { payID = payment.payID, resourcesID = i.resourcePhieuChi.resourcesID, numberResources = i.numberR, sumPrice = i.sumPriceR };
                        DataProvider.Ins.DB.PaymentDetails.Add(paymentDetail);
                    }

                    DataProvider.Ins.DB.SaveChanges();
                    resetPhieuChi();
                    MessageBox.Show("Thêm phiếu chi thành công");
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }

        }

        private void resetPhieuChi()
        {
            phieuChiItems.Clear();
            listSelect.Clear();
            selectResource = null;
        }
    }
}
