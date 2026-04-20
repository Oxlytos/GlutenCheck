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

        public event PropertyChangedEventHandler? PropertyChanged;

        private MainFetchFacade _mainProductFetchFacade;

        public StandardViewModel(MainFetchFacade facade)
        {
            _mainProductFetchFacade = facade;
            CheckGlutenCommand = new Command(async () => await CheckGluten());
            TakePhotoCommand = new Command(async () => await TakePhoto());
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
