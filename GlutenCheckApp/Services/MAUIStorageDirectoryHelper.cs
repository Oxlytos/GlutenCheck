using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlutenCheckApp.Interfaces;
using Infrastructure.Gluten.Interfaces;

namespace GlutenCheckApp.Services
{
    public class MAUIStorageDirectoryHelper : IMAUIStorageDirectoryHelper
    {
        public string GetStorageDirectory()
        {
            return FileSystem.AppDataDirectory;
        }
    }
}
