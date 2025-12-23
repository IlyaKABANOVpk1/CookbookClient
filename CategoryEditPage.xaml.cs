using CookbookClient.DTO;
using CookbookClient.Services;

namespace CookbookClient;

[QueryProperty(nameof(Category), "category")]
public partial class CategoryEditPage : ContentPage
{
    private readonly CategoryService _service;
    private CategoryDto _category;

    public CategoryDto Category
    {
        get => _category;
        set
        {
            _category = value;
          
            BindingContext = _category;
        }
    }

    public CategoryEditPage(CategoryService service)
    {
        InitializeComponent();
        _service = service;

       
        _category = new CategoryDto();
        BindingContext = _category;
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(_category.Name)) return;

        if (_category.Id == 0)
            await _service.CreateAsync(_category);
        else
            await _service.UpdateAsync(_category);

        await Shell.Current.GoToAsync(".."); 
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///MainPage");
    }
}