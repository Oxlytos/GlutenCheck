using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Gluten.Models;
using Infrastructure.Gluten.Interfaces;

namespace Infrastructure.Gluten.Services
{
    public class JsonService : IJsonStorageService
    {
        private string _dataPath;
        private IMAUIStorageDirectoryHelper _storageDirectoryHelper;
        public JsonService(IMAUIStorageDirectoryHelper mAUIStorageDirectoryHelper)
        {
            _storageDirectoryHelper = mAUIStorageDirectoryHelper;
            _dataPath = Path.Combine(_storageDirectoryHelper.GetStorageDirectory(), "JsonData");
            Directory.CreateDirectory(_dataPath);
            Directory.CreateDirectory(Path.Combine(_dataPath, "accounts"));
        }
        public async Task<AccountModel> GetAccountInfoAsync(int accountId)
        {
            try
            {
                var filePath = Path.Combine(_dataPath, $"{accountId}.json");

                var json = await File.ReadAllTextAsync(filePath);
                var account = JsonSerializer.Deserialize<AccountModel>(json);
                return account;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error getting account; " + ex.Message);
                return null;
            }
        }

        public async Task<bool> SaveAccountInfoAsync(AccountModel account)
        {
            try
            {
                var json = JsonSerializer.Serialize(account, new JsonSerializerOptions { WriteIndented=true});
                var filePath = Path.Combine(_dataPath, $"{account.Id}.json");
                await File.WriteAllTextAsync(filePath, json);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error saving account; " + ex.Message);
                return false;
            }
        }
    }
}
