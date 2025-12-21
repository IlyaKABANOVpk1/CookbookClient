using CookbookClient.DTO;
using CookbookClient.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CookbookClient.ViewModel
{
    public class IngredientEditViewModel : BindableObject
    {
        private readonly IngredientService _service;

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

        public ICommand SaveCommand { get; }
        public ICommand BackCommand { get; }

        public IngredientEditViewModel(IngredientService service)
        {
            _service = service;

            SaveCommand = new Command(async () => await SaveAsync());
            BackCommand = new Command(async () => await BackAsync());
        }

        public Task LoadAsync(IngredientDto ingredient)
        {
            if (ingredient == null) return Task.CompletedTask;

            Id = ingredient.Id;
            Name = ingredient.Name;
            return Task.CompletedTask;
        }

        private async Task SaveAsync()
        {
            if (string.IsNullOrWhiteSpace(Name))
                return;

            if (Id == 0)
                await _service.CreateAsync(new IngredientDto { Name = Name });
            else
                await _service.UpdateAsync(new IngredientDto { Id = Id, Name = Name });

            await BackAsync();
        }

        private async Task BackAsync()
        {
            await Shell.Current.GoToAsync("///IngredientsPage");
        }
    }
}
