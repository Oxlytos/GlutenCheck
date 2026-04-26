using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Domain.Gluten.Models;
using Infrastructure.Gluten.Facades;

namespace GlutenCheckApp.ViewModels
{
    public class StandardViewModel : INotifyPropertyChanged
    {
        public string LoggedInText => CurrentAccount?.LoggedInDisplayString ?? "Inte inloggad";

        private AccountModel _account;
        public AccountModel CurrentAccount
        {
            get { return _account; }
            set
            {
                if(value != _account)
                {
                    _account = value;
                    OnPropertyChanged(nameof(CurrentAccount));
                    OnPropertyChanged(nameof(LoggedInText));
                }
            }
        }
        public ICommand CheckGlutenCommand { get; }
        public ICommand TakePhotoCommand { get; }
        public ICommand HandleBarCodeCommand { get; }

        public ICommand CreateAccountDemoCommand { get; }

        public ICommand LoginWithTestAccountCommand {  get; }

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
            CreateAccountDemoCommand = new Command(async () => await CreateDemoAccount());
            LoginWithTestAccountCommand = new Command(async () => await LoginWithTestAccount());
        }

        private async Task LoginWithTestAccount()
        {
            var account = await _mainProductFetchFacade.GetAccountModelAsync(1);

            CurrentAccount = account;
        }

        private async Task CreateDemoAccount()
        {

            AccountModel demoAccount = _mainProductFetchFacade.GenereateRandomAccount();
            
            await SaveNewAccount(demoAccount);
            
        }
        public async Task SaveNewAccount(AccountModel account)
        {
            if (account == null)
            {
                return;
            }

           bool success = await _mainProductFetchFacade.SaveAccount(account);
            if (success)
            {
                Console.WriteLine("Sparade konto");
            }
            else
            {
                Console.WriteLine("Fel med att spara konto");
            }
        }

        private async Task<string?> HandlebarCode(string barcode)
        {
            GlutenStatus = "";
            var returnData = await _mainProductFetchFacade.UserBarcodeCheck(barcode);
            if (returnData == null)
            {
                GlutenStatus = "Ingen träff";
                return null;
            }

            var relevantToUser = await _mainProductFetchFacade.ValidateIfRelevantToUser(CurrentAccount, returnData);

            if (relevantToUser)
            {
                GlutenStatus = "Kan innehålla ingredienser du INTE tål";
            }
            else
            {
                GlutenStatus = "MÖJLIGEN säker fast DUBBELKOLLA SJÄLV!";
            }
               
            return "Working on it";
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
