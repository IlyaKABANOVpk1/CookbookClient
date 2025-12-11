using CookbookClient.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookbookClient.Services
{
    public interface IRecipeService
    {
        IEnumerable<RecipeDto> GetAll();
        RecipeDto? GetById(int id);
        RecipeDto Create(RecipeCreateDto dto);
        RecipeDto Update(RecipeUpdateDto dto);
        bool Delete(int id);
    }
}
