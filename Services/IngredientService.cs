using CookbookClient.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace CookbookClient.Services
{
    public class IngredientService
    {
        private readonly ApiClient _api;

        public IngredientService(ApiClient api)
        {
            _api = api;
        }


       
        public async Task<List<IngredientDto>> GetAllAsync()
            => await _api.Client.GetFromJsonAsync<List<IngredientDto>>("api/Ingredient");

        public async Task<IngredientDto> GetByIdAsync(int id)
            => await _api.Client.GetFromJsonAsync<IngredientDto>($"api/Ingredient/{id}");

        public async Task<IngredientDto> CreateAsync(IngredientDto dto)
        {
            var response = await _api.Client.PostAsJsonAsync("api/ingredient", dto);

            if (!response.IsSuccessStatusCode)
                throw new Exception(await response.Content.ReadAsStringAsync());

            return await response.Content.ReadFromJsonAsync<IngredientDto>();
        }

        public async Task<IngredientDto> UpdateAsync(IngredientDto dto)
        {
            var response = await _api.Client.PutAsJsonAsync($"api/ingredient/{dto.Id}", dto);

            if (!response.IsSuccessStatusCode)
                throw new Exception(await response.Content.ReadAsStringAsync());

            return await response.Content.ReadFromJsonAsync<IngredientDto>();
        }

        public async Task DeleteAsync(int id)
        {
            var response = await _api.Client.DeleteAsync($"api/ingredient/{id}");

            if (!response.IsSuccessStatusCode)
                throw new Exception("Ошибка удаления");
        }
    }
}
