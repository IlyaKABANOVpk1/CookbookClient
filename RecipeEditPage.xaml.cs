using CookbookClient.DTO;
using CookbookClient.ViewModel;

namespace CookbookClient;

[QueryProperty(nameof(Recipe), "recipe")]
public partial class RecipeEditPage : ContentPage
{
    private readonly RecipeEditViewModel _vm;

    private RecipeDto _recipe;
    public RecipeDto Recipe
    {
        get => _recipe;
        set
        {
            _recipe = value;
          
        }
    }

    public RecipeEditPage(RecipeEditViewModel vm)
    {
        InitializeComponent();
        _vm = vm;
        BindingContext = vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

       
        await _vm.LoadAsync(_recipe);
    }
}