using CookbookClient.DTO;
using CookbookClient.Services;

namespace CookbookClient;

public partial class CategoryEditPage : ContentPage
{
    private readonly CategoryService _service;
    private CategoryDto _category;

    public CategoryEditPage(CategoryService service)
    {
        InitializeComponent();
        _service = service;
        _category = new CategoryDto();
        BindingContext = _category;
    }

    public CategoryEditPage(CategoryService service, CategoryDto category)
    {
        InitializeComponent();
        _service = service;
        _category = category;
        BindingContext = _category;
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (_category.Id == 0)
            await _service.CreateAsync(_category);
        else
            await _service.UpdateAsync(_category);

        await Shell.Current.GoToAsync("..");
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///CategoriesPage");
    }
}