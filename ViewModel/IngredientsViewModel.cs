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
    public class IngredientsViewModel : BindableObject
    {
        private readonly IngredientService _service;

        public ObservableCollection<IngredientDto> Ingredients { get; } = new();

        public ICommand LoadCommand { get; }
        public ICommand CreateCommand { get; }
        public ICommand SelectCommand { get; }
        public ICommand DeleteCommand { get; }

        public IngredientsViewModel(IngredientService service)
        {
            _service = service;

            LoadCommand = new Command(async () => await LoadAsync());
            CreateCommand = new Command(async () => await CreateAsync());
            SelectCommand = new Command<IngredientDto>(async i => await EditAsync(i));
            DeleteCommand = new Command<IngredientDto>(async i => await DeleteAsync(i));
        }

        private async Task LoadAsync()
        {
            Ingredients.Clear();
            var items = await _service.GetAllAsync();
            foreach (var item in items)
                Ingredients.Add(item);
        }

        private async Task CreateAsync()
        {
            await Shell.Current.GoToAsync("///IngredientEditPage");
        }

        private async Task EditAsync(IngredientDto ingredient)
        {
            if (ingredient == null) return;

            await Shell.Current.GoToAsync("///IngredientEditPage", new Dictionary<string, object>
            {
                ["ingredient"] = ingredient
            });
        }

        private async Task DeleteAsync(IngredientDto ingredient)
        {
            if (ingredient == null) return;

            await _service.DeleteAsync(ingredient.Id);
            Ingredients.Remove(ingredient);
        }
    }
}
