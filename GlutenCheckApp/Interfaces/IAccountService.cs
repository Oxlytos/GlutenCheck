using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Gluten.Models;

namespace GlutenCheckApp.Interfaces
{
    public interface IAccountService
    {
        Task<AccountModel> GetAccountAsync(int accountId);
        Task<bool> CreateAccount(AccountModel accountModel);
        Task<AccountModel> UpdateAccount(int accountId, Account account);
        Task<AccountModel> DeleteAccount(int accountId);
        Task SaveAccount(AccountModel account);
    }
}
