using GlutenCheckApp.ViewModels;

namespace GlutenCheckApp;

public partial class AccountSettings : ContentPage
{
	public AccountSettings(StandardViewModel vm)
	{
		InitializeComponent();
		BindingContext=vm;
	}
    protected override void OnAppearing()
    {
        base.OnAppearing();

		if(BindingContext is StandardViewModel vm)
		{
			vm.OnLoadSettings();
		}
    }
}