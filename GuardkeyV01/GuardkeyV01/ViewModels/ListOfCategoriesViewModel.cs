using GuardkeyV01.Models;
using GuardkeyV01.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GuardkeyV01.ViewModels
{
    public class ListCategoriesViewModel : BaseViewModel
    {
        public ListCategoriesViewModel()
    {


        OpenCategoryPageCommand = new Command(OpenCategoryPage);

        getCategories();



    }

    private ObservableCollection<Category> categoryList;
    public ObservableCollection<Category> CategoryList
    {
        get { return categoryList; }
        set
        {
            categoryList = value;
            OnPropertyChanged(nameof(CategoryList));
        }
    }

    private Category selectedCategory;
    public Category SelectedCategory
    {
        get { return selectedCategory; }
        set
        {
            selectedCategory = value;
            OnPropertyChanged(nameof(SelectedCategory));
        }
    }


    public Command OpenCategoryPageCommand { get; set; }

    string selectedItem = "All";
    public string SelectedItem
    {
        get => selectedItem;
        set
        {
            if (SetProperty(ref selectedItem, value))
            {
                OnPropertyChanged(nameof(SelectedItem));
                FilterItemsAsync();
            }
        }
    }
    public async void getCategories()
    {
        var categories = await App.categoryService.GetAllCategoriesAsync();

        var observableCollection = new ObservableCollection<Category>(categories);

        CategoryList = observableCollection;
    }

    //public async void getCategories()
    //{
    //    try
    //    {
    //        var categories = await App.CategoryService.GetAllCategoriesAsync();

    //        if (categories != null && categories.Any())
    //        {
    //            // Clear existing items only if the collection is not empty
    //            if (CategoryList.Any())
    //            {
    //                Console.WriteLine("Clearing existing items in CategoryList");
    //                CategoryList.Clear(); // Clear existing items
    //            }

    //            foreach (var category in categories)
    //            {
    //                Console.WriteLine($"Adding category: {category.CategoryName}");
    //                CategoryList.Add(category); // Add new items
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        // Handle or log the exception
    //        Console.WriteLine($"Error in getCategories: {ex.Message}");
    //    }
    //}

    public async Task FilterItemsAsync()
    {


        if (string.IsNullOrEmpty(selectedItem) || selectedItem == "All")
        {

            getCategories();
        }
        else
        {
            var filteredCategories = await App.categoryService.FilterCategoriesAsync(selectedItem);



            CategoryList.Clear();
            foreach (var category in filteredCategories)
            {

                CategoryList.Add(category);
            }
        }
    }





    private int tapCount = 0;
    private const int maxTapCount = 1;
    private const int resetIntervalMilliseconds = 500;
    private async void OpenCategoryPage(object obj)
    {
        tapCount++;


        if (tapCount == maxTapCount)
        {


            await Shell.Current.GoToAsync($"{nameof(LoginPage)}");
            //await Shell.Current.GoToAsync($"//{nameof(UserRecordPage)}");
            tapCount = 0;
        }
    }
}
}
