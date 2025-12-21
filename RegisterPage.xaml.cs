using CookbookClient.ViewModel;

namespace CookbookClient;

public partial class RegisterPage : ContentPage
{
	public RegisterPage(AuthViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///LoginPage");
    }
}