using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Infrastructure.Gluten.Facades;

namespace GlutenCheckApp.ViewModels
{
    public class StandardViewModel : INotifyPropertyChanged
    {
        public ICommand CheckGlutenCommand { get; }
        public ICommand TakePhotoCommand { get; }

        public ICommand HandleBarCodeCommand { get; }

        public event PropertyChangedEventHandler? PropertyChanged;

        private MainFetchFacade _mainProductFetchFacade;

        private string _glutenStatus;
       
        public string GlutenStatus
        {
            get
            {
                return _glutenStatus;
            }
            set
            {
                if (_glutenStatus != value)
                {
                    _glutenStatus = value;
                    OnPropertyChanged(nameof(GlutenStatus));
                }
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public StandardViewModel(MainFetchFacade facade)
        {
            _mainProductFetchFacade = facade;
            CheckGlutenCommand = new Command(async () => await CheckGluten());
            TakePhotoCommand = new Command(async () => await TakePhoto());
            HandleBarCodeCommand = new Command<string>(async (barcode) => await HandlebarCode(barcode));
        }

        private async Task HandlebarCode(string barcode)
        {
            var returnData = await _mainProductFetchFacade.UserBarcodeCheck(barcode);
            if (returnData == null)
            {
                return;
            }
            if (returnData.Model.MainAllergen.Contains("gluten"))
            {
                GlutenStatus = "Kan innehålla gluten🌾🌾🌾";
            }
            else
            {
                GlutenStatus = "Kan vara fri 🚫🚫🚫 från gluten";
            }
            Console.WriteLine(returnData);
        }

        private async Task CheckGluten()
        {
            var data = await _mainProductFetchFacade.UserPressGetInfo();
        }
        private async Task TakePhoto()
        {
            var result = await _mainProductFetchFacade.TakePhotoScan();
        }
    }
}
