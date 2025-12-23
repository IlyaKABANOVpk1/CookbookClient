using CookbookClient.ViewModel;

namespace CookbookClient;

public partial class IngredientsPage : ContentPage
{
    public IngredientsPage(IngredientsViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        ((IngredientsViewModel)BindingContext).LoadCommand.Execute(null);
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
       
        await Shell.Current.GoToAsync("///MainPage");
    }
}