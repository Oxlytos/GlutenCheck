using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public MainFetchFacade(IProductFetcher productFetcher, IAllergenParser allergenParser, ICameraService cameraService, IPhotoRepo photoRepo)
        {
            _productFetcher = productFetcher;
            _allergenParser = allergenParser;
            _cameraService = cameraService;
            _photoRepo = photoRepo;
        }
        public async Task<string> UserPressGetInfo()
        {
            string lingonGrovaBarCode = "7311071330525";
            var data = await _productFetcher.GetProductData(lingonGrovaBarCode);

            Console.WriteLine(data);

            var parsedData = await _allergenParser.ParseAllergen(data);

            return data;
        }
        public async Task<FileResult?> TakePhotoScan()
        {
            var photo =  await _cameraService.TakePhotoAsync();
            await _photoRepo.SavePhoto(photo);
            return photo;
        }
    }
}
