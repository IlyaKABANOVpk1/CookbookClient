using CookbookClient.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookbookClient.Services
{
    public interface ICategoryService
    {
        IEnumerable<CategoryDto> GetAll();
        CategoryDto? GetById(int id);
        CategoryDto Create(CategoryDto dto);
        CategoryDto Update(CategoryDto dto);
        bool Delete(int id);
    }
}
