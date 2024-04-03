using GuardkeyV01.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GuardkeyV01.Services
{
    public interface ICategoryRepository
    {

        Task<IEnumerable<Category>> GetCategoriesAsync();


        Task<int> SaveCategoriesAsync(Category category);

        Task DeleteCategoriesAsync(Category category);

        Task<int> UpdateCategoriesAsync(Category category);


        Task<Category> GetCategory(int id);

        Task<List<Category>> GetAllCategoriesAsync();

        Task<List<Category>> FilterCategoriesAsync(string selectedFilter);

      



    }
}
