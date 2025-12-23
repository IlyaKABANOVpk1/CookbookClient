using CookbookClient.DTO;
using CookbookClient.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CookbookClient.ViewModel
{
    public class IngredientSelection : IngredientDto
    {
        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set { _isSelected = value; }
        }
    }
    public class RecipeEditViewModel : BindableObject
    {
        private readonly RecipeService _recipeService;
        private readonly CategoryService _categoryService;
        private readonly IngredientService _ingredientService;

        public ObservableCollection<CategoryDto> Categories { get; } = new();
        public ObservableCollection<SelectableIngredient> Ingredients { get; } = new();

        private int _id;
        public int Id
        {
            get => _id;
            set { _id = value; OnPropertyChanged(); }
        }

        private string _name;
        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(); }
        }

        private string _description;
        public string Description
        {
            get => _description;
            set { _description = value; OnPropertyChanged(); }
        }

        private CategoryDto _selectedCategory;
        public CategoryDto SelectedCategory
        {
            get => _selectedCategory;
            set { _selectedCategory = value; OnPropertyChanged(); }
        }

        public ICommand SaveCommand { get; }
        public ICommand BackCommand { get; }

        public RecipeEditViewModel(
            RecipeService recipeService,
            CategoryService categoryService,
            IngredientService ingredientService)
        {
            _recipeService = recipeService;
            _categoryService = categoryService;
            _ingredientService = ingredientService;

            SaveCommand = new Command(async () => await SaveAsync());
            BackCommand = new Command(async () => await BackAsync());
        }

        public async Task LoadAsync(RecipeDto recipe = null)
        {
            Categories.Clear();
            Ingredients.Clear();

            var categories = await _categoryService.GetAllAsync();
            foreach (var c in categories)
                Categories.Add(c);

            var ingredients = await _ingredientService.GetAllAsync();
            foreach (var i in ingredients)
                Ingredients.Add(new SelectableIngredient
                {
                    Id = i.Id,
                    Name = i.Name,
                    IsSelected = recipe?.Ingredients.Any(x => x.Id == i.Id) == true
                });

            if (recipe != null)
            {
                Id = recipe.Id;
                Name = recipe.Name;
                Description = recipe.Description;
                SelectedCategory = Categories.FirstOrDefault(c => c.Id == recipe.CategoryId);
            }
        }

        private async Task SaveAsync()
        {
            
            if (SelectedCategory == null)
            {
                await Shell.Current.DisplayAlert("Ошибка", "Пожалуйста, выберите категорию", "ОК");
                return;
            }

            var ingredientIds = Ingredients
                .Where(i => i.IsSelected)
                .Select(i => i.Id)
                .ToList();

            if (Id == 0)
            {
                await _recipeService.CreateAsync(new RecipeCreateDto
                {
                    Name = Name,
                    Description = Description,
                    CategoryId = SelectedCategory.Id, 
                    IngredientIds = ingredientIds
                });
            }
            else
            {
                await _recipeService.UpdateAsync(new RecipeUpdateDto
                {
                    Id = Id,
                    Name = Name,
                    Description = Description,
                    CategoryId = SelectedCategory.Id,
                    IngredientIds = ingredientIds
                });
            }

            await BackAsync();
        }

        private async Task BackAsync()
        {
            await Shell.Current.GoToAsync("///RecipesPage");
        }
    }
}
