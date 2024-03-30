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
            List<string> categoriesNames = new List<string>();
            categoriesNames = await App.categoryService.GetAllByCategoriesNameAsync();


            foreach (var item in categoriesNames)
            {
                if (item == CategoryName)
                {
                    await Application.Current.MainPage.DisplayAlert("Cannot Add", "Such category already existed.", "OK");
                    CategoryName = string.Empty;
                }
            }

            if (!string.IsNullOrWhiteSpace(CategoryName))
            {
                var newCategory = new Category { CategoryName = CategoryName };
                await App.categoryService.SaveCategoriesAsync(newCategory);
                GetCategories();
                CategoryName = string.Empty; // Clear the entry after adding
            }
        }

        private void CancelExecute()
        {
            CategoryName = string.Empty;
            Shell.Current.GoToAsync($"//{nameof(ListOfCategories)}");

        }

        private async void GetCategories()
        {
            var categories = await App.categoryService.GetCategoriesAsync();
            CategoryList = new ObservableCollection<Category>(categories);
        }
    }
}

