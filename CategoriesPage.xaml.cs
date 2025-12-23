using CookbookClient.ViewModel;

namespace CookbookClient;

public partial class CategoriesPage : ContentPage
{
    public CategoriesPage(CategoriesViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        ((CategoriesViewModel)BindingContext).LoadCommand.Execute(null);
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
     
        await Shell.Current.GoToAsync("///MainPage");
    }
}