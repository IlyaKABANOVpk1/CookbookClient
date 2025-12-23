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
    public class RecipesViewModel : BindableObject
    {
        private readonly RecipeService _recipeService;
        private readonly AuthStateService _authState;

        public ObservableCollection<RecipeDto> Recipes { get; } = new();
        private List<RecipeDto> _allRecipes = new();

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged();
                ApplyFilter();
            }
        }

        public bool IsAdmin => _authState.Role == "Админ";

        public ICommand LoadCommand { get; }
        public ICommand CreateCommand { get; }
        public ICommand SelectCommand { get; }
        public ICommand DeleteCommand { get; }

        public RecipesViewModel(
            RecipeService recipeService,
            AuthStateService authState)
        {
            _recipeService = recipeService;
            _authState = authState;

            LoadCommand = new Command(async () => await LoadAsync());
            CreateCommand = new Command(async () => await CreateAsync());
            SelectCommand = new Command<RecipeDto>(async r => await EditAsync(r));
            DeleteCommand = new Command<RecipeDto>(async r => await DeleteAsync(r));
        }

        private async Task LoadAsync()
        {
            Recipes.Clear();
            _allRecipes = await _recipeService.GetAllAsync();

            foreach (var recipe in _allRecipes)
                Recipes.Add(recipe);
        }

        private void ApplyFilter()
        {
            Recipes.Clear();

            var filtered = string.IsNullOrWhiteSpace(SearchText)
                ? _allRecipes
                : _allRecipes.Where(r =>
                    r.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase));

            foreach (var recipe in filtered)
                Recipes.Add(recipe);
        }

        private async Task CreateAsync()
        {
            await Shell.Current.GoToAsync("///RecipeEditPage");
        }

        private async Task EditAsync(RecipeDto recipe)
        {
            if (recipe == null) return;

            await Shell.Current.GoToAsync("///RecipeEditPage", new Dictionary<string, object>
            {
                ["recipe"] = recipe
            });
        }

        private async Task DeleteAsync(RecipeDto recipe)
        {
            if (recipe == null || !IsAdmin) return;

            bool confirm = await Application.Current.MainPage.DisplayAlert(
                "Удаление",
                $"Удалить рецепт \"{recipe.Name}\"?",
                "Да",
                "Нет");

            if (!confirm) return;

            await _recipeService.DeleteAsync(recipe.Id);
            Recipes.Remove(recipe);
            _allRecipes.Remove(recipe);
        }
    }
}
