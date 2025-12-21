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
    public class CategoriesViewModel : BindableObject
    {
        private readonly CategoryService _service;

        public ObservableCollection<CategoryDto> Categories { get; } = new();

        public ICommand LoadCommand { get; }
        public ICommand CreateCommand { get; }
        public ICommand SelectCommand { get; }
        public ICommand DeleteCommand { get; }

        public CategoriesViewModel(CategoryService service)
        {
            _service = service;

            LoadCommand = new Command(async () => await LoadAsync());
            CreateCommand = new Command(async () => await CreateAsync());
            SelectCommand = new Command<CategoryDto>(async c => await EditAsync(c));
            DeleteCommand = new Command<CategoryDto>(async c => await DeleteAsync(c));
        }

        private async Task LoadAsync()
        {
            Categories.Clear();
            var items = await _service.GetAllAsync();
            foreach (var item in items)
                Categories.Add(item);
        }

        private async Task CreateAsync()
        {
            await Shell.Current.GoToAsync("///CategoryEditPage");
        }

        private async Task EditAsync(CategoryDto category)
        {
            if (category == null) return;

            await Shell.Current.GoToAsync("///CategoryEditPage", new Dictionary<string, object>
            {
                ["category"] = category
            });
        }

        private async Task DeleteAsync(CategoryDto category)
        {
            if (category == null) return;

            await _service.DeleteAsync(category.Id);
            Categories.Remove(category);
        }
    }
}
