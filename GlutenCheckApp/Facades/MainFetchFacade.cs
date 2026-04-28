using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Gluten.Models;
using Gluten.ContractDTOs.Models;
using GlutenCheckApp.Interfaces;
using Infrastructure.Gluten.Interfaces;

namespace Infrastructure.Gluten.Facades
{
    public class MainFetchFacade
    {
        IProductFetcher _productFetcher;
        IAllergenParser _allergenParser;
        ICameraService _cameraService;
        IPhotoRepo _photoRepo;
        IAccountService _accountService;
        IJsonStorageService _jsonStorageService;
        public MainFetchFacade(
            IProductFetcher productFetcher, 
            IAllergenParser allergenParser, 
            ICameraService cameraService, 
            IPhotoRepo photoRepo,
            IAccountService accountService,
            IJsonStorageService jsonStorageService
            
            
            )
        {
            _productFetcher = productFetcher;
            _allergenParser = allergenParser;
            _cameraService = cameraService;
            _photoRepo = photoRepo;
            _accountService = accountService;
            _jsonStorageService = jsonStorageService;
        }
        public async Task<AllergenResult> UserPressGetInfo()
        {
            string lingonGrovaBarCode = "7311071330525";
            var data = await _productFetcher.GetProductData(lingonGrovaBarCode);

            Console.WriteLine(data);

            var parsedData = await _allergenParser.ParseAllergen(data);

            return parsedData;
        }
       
        public async Task<AllergenResult> UserBarcodeCheck(string barcode)
        {
            var data = await _productFetcher.GetProductData(barcode);

            if (data == null)
            {
                return null;
            }


            var parsedData = await _allergenParser.ParseAllergen(data);
            if (parsedData == null)
            {
                return null;
            }

            return parsedData;
        }
        public async Task<FileResult?> TakePhotoScan()
        {
            var photo =  await _cameraService.TakePhotoAsync();
            await _photoRepo.SavePhoto(photo);
            return photo;
        }

        public async Task<AccountModel> GetAccountModelAsync(int id=1)
        {
            var account = await _accountService.GetAccountAsync(id);
            return account;
        }


        public async Task<bool> SaveAccount(AccountModel accountModel)
        {
            if (accountModel == null)
            {
                return false;
            }

            var res =await _accountService.CreateAccount(accountModel);
            return res;
        }

        public async Task SaveUserAccountInfo(AccountModel account)
        {
            await _accountService.SaveAccount(account);
        }
        public AccountModel GenereateRandomAccount()
        {
            AccountModel accountModel = new AccountModel
            {
                Id = 1,
                FirstName = "Anders",
                LastName = "Andersson",
                BirthDate = DateOnly.Parse("1968-03-22"),
            };

            Allergen gluten = new Allergen
            {
                Id = 1,
                Name = "Gluten"
            };

            accountModel.RegisteredAllergens.Add(gluten);
            return accountModel;
        }

        //Check if registered allergens on product matches users allergens
        //If relevant, notify
        //Else, continue as usual
        public async Task<bool> ValidateIfRelevantToUser(AccountModel currentAccount, AllergenResult returnData)
        {
            if (returnData == null)
            {
                return false;
            }
            if(returnData.RegiesteredAllergenResults.Count == 0)
            {
                return false;
            }
            if(currentAccount.RegisteredAllergens.Count == 0)
            {
                return false;
            }

            Console.WriteLine(returnData.RegiesteredAllergenResults);
            //Any match at all
            bool matches = currentAccount.RegisteredAllergens.Any(e => returnData.RegiesteredAllergenResults.Any(
                r=> r.Name!=null && e.Name!=null&&
                r.Name.Contains(e.Name, StringComparison.OrdinalIgnoreCase)));

            return matches;
        }

        internal AccountModel GenereateTomatoAccount()
        {
            AccountModel accountModel = new AccountModel
            {
                Id = 2,
                FirstName = "Thomas",
                LastName = "Matt",
                BirthDate = DateOnly.Parse("1986-07-02"),
            };

            Allergen gluten = new Allergen
            {
                Id = 1,
                Name = "Tomato"
            };

            accountModel.RegisteredAllergens.Add(gluten);
            return accountModel;
        }

        internal AccountModel CreateMilkAccount()
        {
            AccountModel accountModel = new AccountModel
            {
                Id = 3,
                FirstName = "Maurice",
                LastName = "Yolk",
                BirthDate = DateOnly.Parse("2002-01-17"),
            };

            Allergen gluten = new Allergen
            {
                Id = 1,
                Name = "Milk"
            };

            accountModel.RegisteredAllergens.Add(gluten);
            return accountModel;
        }

        public async Task<ObservableCollection<Allergen>> GetUserAllergens(AccountModel currentAccount)
        {
            ObservableCollection<Allergen> allergens = new ObservableCollection<Allergen>();

            foreach(var allergen in currentAccount.RegisteredAllergens)
            {
                allergens.Add((Allergen)allergen);
            }

            return allergens;
        }

        internal async Task RemoveAllergenFromUser(AccountModel currentAccount, Allergen allergen)
        {

            currentAccount.RegisteredAllergens.Remove(allergen);
        }
    }
}
