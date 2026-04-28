using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Domain.Gluten.Models;
using GlutenCheckApp.Models;
using Infrastructure.Gluten.Facades;
using Infrastructure.Gluten.Managers;

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
        public ObservableCollection<SelectableAllergenUI> AllAllergens { get; set; } = new();
        public ObservableCollection<Allergen> UserAllergens { get; set; } = new();
        public ICommand CheckGlutenCommand { get; }
        public ICommand TakePhotoCommand { get; }
        public ICommand HandleBarCodeCommand { get; }

        public ICommand CreateAccountDemoCommand { get; }
        public ICommand CreateTomatoAccountCommand { get; }
        public ICommand CreateMilkCommand { get; }

        public ICommand LoginWithTestAccountCommand {  get; }
        public ICommand LoginTomatoCommand {  get; }
        public ICommand LoginMilkCommand { get; }
        public ICommand GetUserAllergensCommand { get; }

        public ICommand RemoveAllergenCommand { get; }

        public ICommand SaveUserAllergensCommand { get; }

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


           
            CreateTomatoAccountCommand = new Command(async () => await CreateTomatoAccount());
            CreateAccountDemoCommand = new Command(async () => await CreateDemoAccount());
            CreateMilkCommand = new Command(async () => await CreateMilkAccount());

            LoginWithTestAccountCommand = new Command(async () => await LoginWithTestAccount());
            LoginTomatoCommand = new Command(async () => await LoginTomato());
            LoginMilkCommand = new Command(async () => await LoginMilk());

            GetUserAllergensCommand = new Command(async () => await GetUserAllergens());

            RemoveAllergenCommand = new Command<Allergen>(async (Allergen allergen) => await RemoveAllergen(allergen));


            SaveUserAllergensCommand = new Command(async () => await SaveUserAllergens());

        }

        private async Task SaveUserAllergens()
        {
            if(CurrentAccount == null)
            {
                return;
            }

            CurrentAccount.RegisteredAllergens = AllAllergens.Where(
                a=>a.IsSelected).Select(a=> new Allergen { Name = a.Name }).ToList();

            UserAllergens.Clear();

            var userSelectedAllergens = AllAllergens.
                Where(a=>a.IsSelected).Select(a=>new Allergen { Name = a.Name });

            foreach (var allergen in userSelectedAllergens)
            {
                UserAllergens.Add(allergen);
            }

          await  _mainProductFetchFacade.SaveUserAccountInfo(CurrentAccount);

        }

        private async Task RemoveAllergen(Allergen allergen)
        {
            if (allergen == null)
            {
                return;
            }
            await _mainProductFetchFacade.RemoveAllergenFromUser(CurrentAccount, allergen);

            await GetUserAllergens();
        }

        private async Task GetUserAllergens()
        {
            var items = await _mainProductFetchFacade.GetUserAllergens(CurrentAccount);

            UserAllergens.Clear();

            foreach (var item in items) 
            {
                UserAllergens.Add(item);

            }

        }

        private async Task CreateMilkAccount()
        {
            AccountModel demoAccount = _mainProductFetchFacade.CreateMilkAccount();

            await SaveNewAccount(demoAccount);
        }

        private async Task LoginMilk()
        {
            var account = await _mainProductFetchFacade.GetAccountModelAsync(3);

            CurrentAccount = account;
        }

        private async Task LoginTomato()
        {
            var account = await _mainProductFetchFacade.GetAccountModelAsync(2);

            CurrentAccount = account;
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
        private async Task CreateTomatoAccount()
        {

            AccountModel demoAccount = _mainProductFetchFacade.GenereateTomatoAccount();

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

        //When we load the page
        internal async Task OnLoadSettings()
        {
            if(CurrentAccount == null)
            {
                return;
            }
            await GetUserAllergens();
            await BuildSelectListAllergens();
        }
        public async Task BuildSelectListAllergens()
        {
            AllAllergens.Clear();

            foreach(var allergen in AllergenManager.ExampleAllergens)
            {

                bool isUserSelected = UserAllergens.Any(UA=>UA.Name.Equals(allergen.Name, StringComparison.OrdinalIgnoreCase));

                AllAllergens.Add(new SelectableAllergenUI
                {
                    Name = allergen.Name,
                    IsSelected = isUserSelected
                });
            }


        }
    }
}
