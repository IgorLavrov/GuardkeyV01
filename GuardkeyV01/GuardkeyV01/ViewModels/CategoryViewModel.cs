using GuardkeyV01.Models;
using GuardkeyV01.Services;
using GuardkeyV01.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace GuardkeyV01.ViewModels
{
    public class CategoryViewModel : BaseViewModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _categoryName;
        public string CategoryName
        {
            get { return _categoryName; }
            set
            {
                _categoryName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CategoryName)));
            }
        }

        private ObservableCollection<Category> _categoryList;
        public ObservableCollection<Category> CategoryList
        {
            get { return _categoryList; }
            set
            {
                _categoryList = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CategoryList)));
            }
        }

        public ICommand AddCommand { get; }
        public ICommand CancelCommand { get; }

        public CategoryViewModel()
        {
            AddCommand = new Command(AddExecute);
            CancelCommand = new Command(CancelExecute);

            GetCategories();
        }

        private async void AddExecute()
        {
            var newCategory = new Category { CategoryName = CategoryName };
            await App.categoryService.SaveCategoriesAsync(newCategory);
            GetCategories();
            CategoryName = string.Empty; // Clear the entry after adding

        }

        private void CancelExecute()
        {
            CategoryName = string.Empty; // Clear the entry if canceled
        }

        private async void GetCategories()
        {
            var categories = await App.categoryService.GetCategoriesAsync();
            CategoryList = new ObservableCollection<Category>(categories);
        }
    }
}
//    private async void ProcessCategories(object obj)
//    {
//        var categories = await App.categoryService.GetCategoriesAsync();

//        if (TypeCommand == "Add" || TypeCommand == "Update")
//        {
//            foreach (var category in categories)
//            {
//                if (category.CategoryName == CategoryName)
//                {
//                    await Application.Current.MainPage.DisplayAlert("Cannot Add", "Such category already exists.", "OK");
//                    TypeCommand = string.Empty;
//                    return;
//                }
//            }
//        }
//        if (TypeCommand == "Add")
//        {
//            var newCategory = new Category { CategoryName = CategoryName };
//            await App.categoryService.SaveCategoriesAsync(newCategory);
//        }
//        else if (TypeCommand == "Update")
//        {
//            if (SelectedCategory != null)
//            {
//                SelectedCategory.CategoryName = CategoryName;
//                await App.categoryService.UpdateCategoriesAsync(SelectedCategory);
//            }
//        }
//        else if (TypeCommand == "Delete")
//        {
//            if (SelectedCategory != null)
//            {
//                await App.categoryService.DeleteCategoriesAsync(SelectedCategory);
//            }
//        }

//        TypeCommand = string.Empty;
//        SelectedCategory = null;
//        getCategories(); // Make sure to await the method call
//        await Shell.Current.GoToAsync($"//{nameof(ListOfCategories)}");
//    }    


