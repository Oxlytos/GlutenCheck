using GlutenCheckApp.ViewModels;

namespace GlutenCheckApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage(StandardViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }
       
    }

}
