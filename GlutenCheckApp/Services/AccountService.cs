using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Gluten.Models;
using GlutenCheckApp.Interfaces;
using Infrastructure.Gluten.Interfaces;

namespace GlutenCheckApp.Services
{
    public class AccountService : IAccountService
    {
        private readonly IJsonStorageService _jsonStorageService;
        public AccountService(IJsonStorageService jsonStorageService)
        {
            _jsonStorageService = jsonStorageService;
        }
        public async Task<bool> CreateAccount(AccountModel account)
        {
            return await _jsonStorageService.SaveAccountInfoAsync(account);
        }

        public async Task<AccountModel> DeleteAccount(int accountId)
        {
            throw new NotImplementedException();
        }

        public async Task<AccountModel> GetAccountAsync(int accountId)
        {
            return await _jsonStorageService.GetAccountInfoAsync(accountId);
            throw new NotImplementedException();
        }

        public async Task SaveAccount(AccountModel account)
        {
             await _jsonStorageService.SaveAccountInfoAsync(account);
        }

        public async Task<AccountModel> UpdateAccount(int accountId, Account account)
        {
            throw new NotImplementedException();
        }
    }
}
