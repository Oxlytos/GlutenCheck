using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Gluten.Models;

namespace Infrastructure.Gluten.Interfaces
{
    public interface IJsonStorageService
    {
        Task<bool> SaveAccountInfoAsync(AccountModel account);

        Task<AccountModel> GetAccountInfoAsync(int accountId);
    }
}
