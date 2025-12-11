using CookbookClient.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookbookClient.Services
{
    public interface IIngredientService
    {
        IEnumerable<IngredientDto> GetAll();
        IngredientDto? GetById(int id);
        IngredientDto Create(IngredientDto dto);
        IngredientDto Update(IngredientDto dto);
        bool Delete(int id);
    }
}
