using System.Threading.Tasks;
using GlutenCheckApp.ViewModels;

namespace GlutenCheckApp
{
    public partial class MainPage : ContentPage
    {
        StandardViewModel _vm;
        public Microsoft.Maui.Graphics.Color BackgroundColor { get; set; }
        bool isProccsesing =false;
        public MainPage(StandardViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
            _vm = vm;
            PageBarCodeReader.Options = new ZXing.Net.Maui.BarcodeReaderOptions
            {
                //European barcode format
                Formats = ZXing.Net.Maui.BarcodeFormat.Ean13,
                TryHarder = true,
                AutoRotate = true,
                

            };
            BackgroundColor = Color.FromArgb("255,0,0,0");

        }

        //When detected barcode
        private async void PageBarCodeReader_BarcodesDetected(object sender, ZXing.Net.Maui.BarcodeDetectionEventArgs e)
        {
            if(isProccsesing) return;
            var firstHit = e.Results.FirstOrDefault();
            
            if (firstHit == null)
            {
                return;
            }
            isProccsesing=true;

            try
            {
                if (BindingContext is StandardViewModel vm && vm.HandleBarCodeCommand.CanExecute(firstHit.Value))
                {
                    _vm.HandleBarCodeCommand.Execute(firstHit.Value);
                    string result = _vm.GlutenStatus.ToString();
                    if (!string.IsNullOrEmpty(result))
                    {
                        await MainThread.InvokeOnMainThreadAsync(() =>("Gluten resultat", _vm.GlutenStatus, "Okej"));

                    }

                }

            }
            finally
            {
                //Delay rapid fire scans
                await Task.Delay(1000);
                isProccsesing = false;
            }
        }
    }

}
