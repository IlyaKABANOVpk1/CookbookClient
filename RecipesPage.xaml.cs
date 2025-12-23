using CookbookClient.DTO;
using CookbookClient.ViewModel;

namespace CookbookClient;

public partial class RecipesPage : ContentPage
{
    private readonly RecipesViewModel _viewModel;

    public RecipesPage(RecipesViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
 
        if (_viewModel.LoadCommand.CanExecute(null))
        {
            _viewModel.LoadCommand.Execute(null);
        }
    }


    private async void OnAddRecipeClicked(object sender, EventArgs e)
    {

        await Shell.Current.GoToAsync("///RecipeEditPage");
    }

  
    private async void OnRecipeTapped(object sender, TappedEventArgs e)
    {

        var frame = sender as Frame;
        var recipe = e.Parameter as RecipeDto;

        if (recipe == null) return;

   
        var navigationParameter = new Dictionary<string, object>
            {
                { "recipe", recipe }
            };

        await Shell.Current.GoToAsync("///RecipeEditPage", navigationParameter);
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///MainPage");
    }
}
