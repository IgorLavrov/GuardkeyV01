using GuardkeyV01.Models;
using GuardkeyV01.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GuardkeyV01.ViewModels
{
    public class ListOfCategoriesViewModel : BaseViewModel
    {
        public ListOfCategoriesViewModel()
        {


            OpenCategoryPageCommand = new Command(OpenCategoryPage);
            SelectedIndexChangedCommand = new Command<string>(FilterItemsAsync);
            AddRecordCommand = new Command(OnAddRecord);
            DeleteCategoryCommand = new Command(DeleteRecord);

          
            LoadCategories();
            LoadNames();
            
            FilterItemsAsync("All");
        }

        private ObservableCollection<Category> _categoryList;
        public ObservableCollection<Category> CategoryList
        {
            get => _categoryList;
            set => SetProperty(ref _categoryList, value);
        }

        private ObservableCollection<string> _categoryNames;
        public ObservableCollection<string> CategoryNames
        {
            get => _categoryNames;
            set => SetProperty(ref _categoryNames, value);
        }

        private Category _selectedCategory;
        public Category SelectedCategory
        {
            get => _selectedCategory;
            set => SetProperty(ref _selectedCategory, value);
        }

        private string _selectedItem = "All";
        public string SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                FilterItemsAsync(value); 
            }
        }

        public Command OpenCategoryPageCommand { get; }
        public Command<string> SelectedIndexChangedCommand { get; }
        public Command AddRecordCommand { get; }
        public Command DeleteCategoryCommand { get; }
       

        public async void LoadCategories()
        {
            var categories = await App.categoryService.GetAllCategoriesAsync();
            CategoryList = new ObservableCollection<Category>(categories);
        }
        public async void LoadNames()
        {
            var categories = await App.categoryService.GetAllCategoriesAsync();
            CategoryNames = new ObservableCollection<string>(categories.Select(c => c.CategoryName));
        }

        public async void FilterItemsAsync(string selectedItem)
        {
            if (string.IsNullOrEmpty(selectedItem) || selectedItem == "All")
            {
                LoadCategories(); 
            }
            else
            {
                var filteredCategories = await App.categoryService.FilterCategoriesAsync(selectedItem);
                CategoryList = new ObservableCollection<Category>(filteredCategories);
            }
        }
        private async void OpenCategoryPage(object obj)
        {
            if (SelectedCategory != null)
            {
                await Shell.Current.Navigation.PushAsync(new NotePage(SelectedCategory));
            }
        }



        private async void OnAddRecord(object obj)
        {
            await Shell.Current.GoToAsync($"//{nameof(AddCategory)}");
          
        }
        private async void DeleteRecord(object obj)
        {
            if (obj is Category category)
            {
                if (category.CategoryName != "All")
                {
                    try
                    {
                        
                        await App.categoryService.DeleteCategoriesAsync(category);

                     
                        CategoryList.Remove(category);
                    }
                    catch (Exception ex)
                    {
                        

                        await Application.Current.MainPage.DisplayAlert("Error", $"Failed to delete the category: {ex.Message}", "OK");
                    }
                }
                else
                {
                    
                  
                    await Application.Current.MainPage.DisplayAlert("Cannot Delete", "The 'All' option cannot be deleted.", "OK");
                }
            }
        }


    }
}

   