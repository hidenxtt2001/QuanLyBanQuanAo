using AnimatedColorfulMenu.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AnimatedColorfulMenu.ViewModel
{
    class LapHoaDonViewModel : MainViewModel
    {

        public ICommand selectProduct { get; set; }

        public LapHoaDonViewModel()
        {
            selectProduct = new RelayCommand<Product>((p) =>
            {
                return true;
            },
                (p) =>
            {

            });
        }
    }
}
