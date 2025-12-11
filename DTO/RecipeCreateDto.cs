using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookbookClient.DTO
{
    public class RecipeCreateDto
    {
        public string Name { get; set; }
        public int CategoryId { get; set; }

        public List<int> IngredientIds { get; set; }

        public string Description { get; set; } = string.Empty;
    }
}
