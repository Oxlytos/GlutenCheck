using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlutenCheckApp.Services;
using Infrastructure.Gluten.Interfaces;

namespace Infrastructure.Gluten.Services
{
    public class CameraService : ICameraService
    {
     
        async Task<FileResult?> ICameraService.TakePhotoAsync()
        {
            if(!MediaPicker.Default.IsCaptureSupported)
            {
                return null;
            }
            var photo = await MediaPicker.Default.CapturePhotoAsync(new MediaPickerOptions
            {
                Title = "Kolla efter produkt QR Code/ Sträckkod"
            });

            return photo;
                
        }
    }
}
