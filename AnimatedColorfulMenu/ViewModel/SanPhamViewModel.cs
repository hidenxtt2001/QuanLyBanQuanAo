using AnimatedColorfulMenu.Model;
using AnimatedColorfulMenu.View;
using Firebase.Storage;
using Google.Cloud.Firestore;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace AnimatedColorfulMenu.ViewModel
{
    class SanPhamViewModel : MainViewModel
    {

        public ICommand themProduct { get; set; }

        public ICommand chonHinhAnh { get; set; }

        public ICommand xoaProduct { get; set; }

        public ICommand suaProduct { get; set; }

        private string imagePath { get; set; }



        private BitmapImage _bitmapImage;
        public BitmapImage bitmapImage
        {
            get => this._bitmapImage; set
            {
                this._bitmapImage = value;
                OnPropertyChanged();
            }
        }



        private string _nameProduct;
        public string NameProduct
        {
            get => _nameProduct; set
            {
                _nameProduct = value;
                OnPropertyChanged();
            }
        }

        private string _priceProduct;
        public string PriceProduct
        {
            get => _priceProduct; set
            {
                _priceProduct = value; OnPropertyChanged();
            }
        }

        SanPham sanphamWD;
        public ICommand setupSanPham { get; set; }

        public SanPhamViewModel()
        {

            ResetFormInput();
            string path = AppDomain.CurrentDomain.BaseDirectory + @"quanlyquanao-firebase-adminsdk-x253q-2787a0673d.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);
            setupSanPham = new RelayCommand<SanPham>((p) => { return true; }, (p) => sanphamWD = p);
            themProduct = new RelayCommand<SanPham>((p) => { return checkValidInput(p); }, (p) => { addProduct(p); });
            chonHinhAnh = new RelayCommand<Image>((p) => { return true; }, (p) => { chooseImage(p); });
            xoaProduct = new RelayCommand<Product>((p) =>
            {
                return p != null;
            }, (p) =>
            {
                if(MessageBox.Show("Bạn có muốn xóa sản phẩm?", "Thông báo!", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    RemoveProduct(p);
                }
                
            });

            suaProduct = new RelayCommand<Product>((p) => { return true; }, (p) => {
                EditProduct(p);
            });

        }


        private void EditProduct(Product e)
        {
            UpdateProductViewModel updateProductViewModel = sanphamWD.updateVMT.DataContext as UpdateProductViewModel;
            updateProductViewModel.SetupUpdate(e);
            loadProduct();
        }

        private void RemoveProduct(Product e)
        {
            deleteProduct(e);
        }
        private void ResetFormInput()
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.UriSource = new Uri("pack://application:,,,/Assets/school-bag.png");
            bitmap.EndInit();
            bitmapImage = bitmap.Clone();
            PriceProduct = null;
            NameProduct = null;
        }

        private bool checkValidInput(SanPham sanPham)
        {
            if (sanPham == null) return false;
            return sanPham.productName.Text.Length > 0 && sanPham.productPrice.Text.Length > 0 && imagePath != null;
        }

        private async void addProduct(SanPham sanPham)
        {
            string id = Guid.NewGuid().ToString("N");
            string link = await upLoadimageAsync(id);
            Product product = new Product() { productName = NameProduct, price = float.Parse(PriceProduct), imageUrl = link };
            addProduct(product);
            ResetFormInput();
        }

        private async Task<string> upLoadimageAsync(string pid)
        {

            var task = new FirebaseStorage("quanlyquanao.appspot.com").Child("shopstore").Child("product").Child(pid).PutAsync(File.Open(imagePath, FileMode.Open));
            task.Progress.ProgressChanged += (s, e) => Console.WriteLine($"Progress: {e.Percentage} %");
            var downloadUrl = await task;
            return downloadUrl;
        }
        private void chooseImage(Image image)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();


                openFileDialog.Title = "Open Image";
                openFileDialog.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";

                if (openFileDialog.ShowDialog() == true)
                {

                    string selectedFileName = openFileDialog.FileName;
                    imagePath = openFileDialog.FileName;


                    bitmapImage = getImage();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Lỗi Format hình ảnh");
            }
        }

        private BitmapImage getImage()
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.UriSource = new Uri(imagePath);
            bitmap.EndInit();
            var k = bitmap.Clone();
            return k;
        }
    }
}
