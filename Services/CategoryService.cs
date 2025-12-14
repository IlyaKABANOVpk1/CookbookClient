using CookbookClient.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace CookbookClient.Services
{
    public class CategoryService
    {
        private readonly ApiClient _api;

        public CategoryService(ApiClient api)
        {
            _api = api;
        }

        public async Task<List<CategoryDto>> GetAllAsync()
        {
            return await _api.Client.GetFromJsonAsync<List<CategoryDto>>("api/category");
        }

        public async Task<CategoryDto> GetByIdAsync(int id)
        {
            return await _api.Client.GetFromJsonAsync<CategoryDto>($"api/category/{id}");
        }

        public async Task<CategoryDto> CreateAsync(CategoryDto dto)
        {
            var response = await _api.Client.PostAsJsonAsync("api/category", dto);

            if (!response.IsSuccessStatusCode)
                throw new Exception(await response.Content.ReadAsStringAsync());

            return await response.Content.ReadFromJsonAsync<CategoryDto>();
        }

        public async Task<CategoryDto> UpdateAsync(CategoryDto dto)
        {
            var response = await _api.Client.PutAsJsonAsync($"api/category/{dto.Id}", dto);

            if (!response.IsSuccessStatusCode)
                throw new Exception(await response.Content.ReadAsStringAsync());

            return await response.Content.ReadFromJsonAsync<CategoryDto>();
        }

        public async Task DeleteAsync(int id)
        {
            var response = await _api.Client.DeleteAsync($"api/category/{id}");

            if (!response.IsSuccessStatusCode)
                throw new Exception("Ошибка удаления");
        }
    }
}
