using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlutenCheckApp.Interfaces
{
    public interface IPhotoRepo
    {
        Task<string> SavePhoto(FileResult photoUrl);
        Task<string> GetPhoto(string photoUrl);
    }
}
