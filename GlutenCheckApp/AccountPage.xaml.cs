using GlutenCheckApp.ViewModels;

namespace GlutenCheckApp;

public partial class Account : ContentPage
{
	public Account(StandardViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}