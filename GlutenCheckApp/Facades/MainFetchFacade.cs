using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public MainFetchFacade(IProductFetcher productFetcher, IAllergenParser allergenParser, ICameraService cameraService, IPhotoRepo photoRepo)
        {
            _productFetcher = productFetcher;
            _allergenParser = allergenParser;
            _cameraService = cameraService;
            _photoRepo = photoRepo;
        }
        public async Task<AllergensModel> UserPressGetInfo()
        {
            string lingonGrovaBarCode = "7311071330525";
            var data = await _productFetcher.GetProductData(lingonGrovaBarCode);

            Console.WriteLine(data);

            var parsedData = await _allergenParser.ParseAllergen(data);

            return parsedData;
        }
        public async Task<AllergensModel> UserBarcodeCheck(string barcode)
        {
            var data = await _productFetcher.GetProductData(barcode);

            Console.WriteLine(data);

            var parsedData = await _allergenParser.ParseAllergen(data);

            return parsedData;
        }
        public async Task<FileResult?> TakePhotoScan()
        {
            var photo =  await _cameraService.TakePhotoAsync();
            await _photoRepo.SavePhoto(photo);
            return photo;
        }
    }
}
