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
    public class CategoryEditViewModel : BindableObject
    {
        private readonly CategoryService _service;

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

        public CategoryEditViewModel(CategoryService service)
        {
            _service = service;

            SaveCommand = new Command(async () => await SaveAsync());
            BackCommand = new Command(async () => await BackAsync());
        }

        public async Task LoadAsync(CategoryDto category)
        {
            if (category == null) return;

            Id = category.Id;
            Name = category.Name;
        }

        private async Task SaveAsync()
        {
            if (string.IsNullOrWhiteSpace(Name))
                return;

            if (Id == 0)
                await _service.CreateAsync(new CategoryDto { Name = Name });
            else
                await _service.UpdateAsync(new CategoryDto { Id = Id, Name = Name });

            await BackAsync();
        }

        private async Task BackAsync()
        {
            await Shell.Current.GoToAsync("///CategoriesPage");
        }
    }
}
