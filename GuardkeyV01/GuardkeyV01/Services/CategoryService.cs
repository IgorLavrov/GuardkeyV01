using GuardkeyV01.Models;
using SQLite;
using System;
using System.Collections.Generic;
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
            await _database.CreateTableAsync<Category>(); // Create Category table if not exists
            await SeedCategoriesAsync();
        }
        private async Task SeedCategoriesAsync()
        {
            var existingCategories = await _database.Table<Category>().ToListAsync().ConfigureAwait(false);

                if (existingCategories.Count == 0)
                {
                    var initialCategories = new List<Category>
                    {
                    new Category { CategoryName = "All" },
                    new Category { CategoryName = "Home" },
                    new Category { CategoryName = "Groceries" }
                    };

                    await _database.InsertAllAsync(initialCategories).ConfigureAwait(false);
                }
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

        //public Task<Category> FilterCategoriesAsync(string selectedFilter)
        //{
        //    return _database.Table<Category>()
        //                    .Where(c => c.CategoryName.ToLower().Contains(selectedFilter.ToLower()))
        //                    .FirstOrDefaultAsync();
        //}
        public Task<List<Category>> FilterCategoriesAsync(string selectedFilter)
        {
            return _database.Table<Category>()
                            .Where(c => c.CategoryName.ToLower()==selectedFilter.ToLower())
                            .ToListAsync();
        }


        public Task<int> SaveCategoriesAsync(Category category)
        {
            if (string.IsNullOrWhiteSpace(category.CategoryName))
                throw new ArgumentException("CategoryName cannot be null or empty.");

            return _database.InsertOrReplaceAsync(category);
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
