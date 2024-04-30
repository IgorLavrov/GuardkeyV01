using GuardkeyV01.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuardkeyV01.Services
{
    public class CategoryService : ICategoryRepository
    {
        private readonly SQLiteAsyncConnection _database;

        public CategoryService(SQLiteAsyncConnection database)
        {
            _database = database;
            InitializeDatabase();
        }
        private async Task InitializeDatabase()
        {

            await SeedCategoriesAsync();
        }
        private async Task SeedCategoriesAsync()
        {
            var existingCategories = await _database.Table<Category>().ToListAsync().ConfigureAwait(false);

            if (existingCategories.Count == 0)
            {
                var initialCategories = new List<Category>
                    {
                    new Category { CategoryName = "ALL" },
                    new Category { CategoryName = "HOME" },
                    new Category { CategoryName = "WORK" }
                    };

                await _database.InsertAllAsync(initialCategories).ConfigureAwait(false);
            }
        }
        public async Task<List<string>> GetAllByCategoriesNameAsync()
        {
            var categories = await _database.Table<Category>().ToListAsync();
            return categories.Select(c => c.CategoryName).ToList();
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            var categories = await _database.Table<Category>().ToListAsync();
            return categories;
        }
        public Task<Category> GetCategory(int id)
        {
            return _database.Table<Category>().Where(c => c.Id == id).FirstOrDefaultAsync();
        }


        public Task<List<Category>> GetAllCategoriesAsync()
        {
            return _database.Table<Category>().ToListAsync();
        }

        public Task<List<Category>> FilterCategoriesAsync(string selectedFilter)
        {
            return _database.Table<Category>()
                            .Where(c => c.CategoryName.ToLower() == selectedFilter.ToLower())
                            .ToListAsync();
        }


        public Task<int> SaveCategoriesAsync(Category category)
        {


            return _database.InsertAsync(category);
        }
        public Task DeleteCategoriesAsync(Category category)
        {
            return _database.DeleteAsync(category);
        }
        public Task<int> UpdateCategoriesAsync(Category category)
        {
            return _database.UpdateAsync(category);
        }
    }
}
