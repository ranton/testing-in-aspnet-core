using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleWebApplication
{
    public class WeatherService
    {
        readonly AppDbContext _context;
        public WeatherService(AppDbContext context)
        {
            _context = context; // EF Core DbContext is injected in the constructor
        }

        public Weather GetRecipe(int id)
        {
            return _context.Weathers 
                           .Where(x => x.Id == id) // Uses DbSet<Recipes> property to load weather and creates a RecipeViewModel
                           .Select(x => new Weather
                            {
                                Id = x.Id,
                                Name = x.Name
                            })
                            .SingleOrDefault();
        }
    }

}
