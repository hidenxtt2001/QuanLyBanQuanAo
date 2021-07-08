using AnimatedColorfulMenu.Model;
using AnimatedColorfulMenu.View;
using Firebase.Storage;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace AnimatedColorfulMenu.ViewModel
{
    class UpdateProductViewModel : BaseViewModel
    {

        public ICommand themProduct { get; set; }

        public ICommand chonHinhAnh { get; set; }

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

        private string imagePath { get; set; }



        private UpdateProduct updateProduct;
        public UpdateProductViewModel()
        {
            themProduct = new RelayCommand<Object>((p) => { return checkValidInput(); }, (p) => { addProduct(); });
            chonHinhAnh = new RelayCommand<Image>((p) => { return true; }, (p) => { chooseImage(p); });
            
            
        }

        Product product1;
        public void SetupUpdate(Product product)
        {
            product1 = product;
            NameProduct = product.productName;
            PriceProduct = product.price.ToString();
            bitmapImage = new BitmapImage(new Uri(product.imageUrl));
            updateProduct = new UpdateProduct();
            updateProduct.ShowDialog();
        }

        private bool checkValidInput()
        {
            return NameProduct.Length > 0 && PriceProduct.Length > 0 && (imagePath != null || bitmapImage != null);
        }

        private async void addProduct()
        {
            string id = product1.productID.ToString();
            string link = "";
            if (imagePath != null)
            {
                link = await upLoadimageAsync(id);
            }
                
            Product productz = new Product() { productID = product1.productID, productName = NameProduct, price = float.Parse(PriceProduct), imageUrl = imagePath != null ? link : product1.imageUrl };
            iaddProduct(productz);
            updateProduct.Close();
        }

        public void iaddProduct(Product product)
        {
            var result = DataProvider.Ins.DB.Products.Where(i => i.productID == product.productID).ToList()[0];
            result.imageUrl = product.imageUrl;
            result.price = product.price;
            result.productName = product.productName;
            DataProvider.Ins.DB.SaveChanges();
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
