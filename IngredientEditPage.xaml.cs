using CookbookClient.DTO;
using CookbookClient.Services;

namespace CookbookClient;

[QueryProperty(nameof(Ingredient), "ingredient")]
public partial class IngredientEditPage : ContentPage
{
    private readonly IngredientService _service;
    private IngredientDto _ingredient;

    public IngredientDto Ingredient
    {
        get => _ingredient;
        set
        {
            _ingredient = value;
            BindingContext = _ingredient; 
        }
    }

    public IngredientEditPage(IngredientService service)
    {
        InitializeComponent();
        _service = service;

        
        _ingredient = new IngredientDto();
        BindingContext = _ingredient;
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(_ingredient.Name)) return;

        if (_ingredient.Id == 0)
            await _service.CreateAsync(_ingredient);
        else
            await _service.UpdateAsync(_ingredient);

        await Shell.Current.GoToAsync("///IngredientsPage");
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///IngredientsPage");
    }
}