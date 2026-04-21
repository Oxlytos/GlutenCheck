using GlutenCheckApp.ViewModels;

namespace GlutenCheckApp
{
    public partial class MainPage : ContentPage
    {
        StandardViewModel _vm;
        public MainPage(StandardViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
            _vm = vm;
            PageBarCodeReader.Options = new ZXing.Net.Maui.BarcodeReaderOptions
            {
                //European barcode format
                Formats = ZXing.Net.Maui.BarcodeFormat.Ean13,
                TryHarder = true

            };

        }

        //When detected barcode
        private void PageBarCodeReader_BarcodesDetected(object sender, ZXing.Net.Maui.BarcodeDetectionEventArgs e)
        {
            
            var firstHit = e.Results.FirstOrDefault();
            
            if (firstHit == null)
            {
                return;
            }

            Dispatcher.DispatchAsync(async () =>
            {
                await DisplayAlert("Hittade en strekkod!", firstHit.Value, "Okej!");
            });

            
            if(BindingContext is StandardViewModel vm && vm.HandleBarCodeCommand.CanExecute(firstHit.Value))
            {
                _vm.HandleBarCodeCommand.Execute(firstHit.Value);

            }
        }
    }

}
