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

            // Load categories first
            LoadCategories();
            LoadNames();
            // Set the default selected item to "All"
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
                FilterItemsAsync(value); // Pass the selected item as a parameter
            }
        }

        public Command OpenCategoryPageCommand { get; }
        public Command<string> SelectedIndexChangedCommand { get; }
        public Command AddRecordCommand { get; }

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
                LoadCategories(); // Load all categories
            }
            else
            {
                var filteredCategories = await App.categoryService.FilterCategoriesAsync(selectedItem);
                CategoryList = new ObservableCollection<Category>(filteredCategories);
            }
        }


        private async void OpenCategoryPage(object obj)
        {
            await Shell.Current.GoToAsync($"//{nameof(NotePage)}");


        }

        private async void OnAddRecord(object obj)
        {
            await Shell.Current.GoToAsync($"//{nameof(AddCategory)}");
            //await Application.Current.MainPage.Navigation.PushAsync(new AddUserRecordPage());
        }
    }
}

   