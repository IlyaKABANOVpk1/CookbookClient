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
}