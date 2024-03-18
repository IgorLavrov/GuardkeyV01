using GuardkeyV01.Models;
using GuardkeyV01.Services;
using GuardkeyV01.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace GuardkeyV01.ViewModels
{
    public  class CategoryViewModel : BaseViewModel,INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;      

        private bool isViewDetail = false;
        public bool IsViewDetail
        {
            get { return isViewDetail; }
            set
            {
                isViewDetail = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsViewDetail"));
            }
        }
        private string typeCommand = string.Empty;
        public string TypeCommand
        {
            get { return typeCommand; }
            set
            {
                typeCommand = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TypeCommand"));
            }
        }

        private string categoryName;
        public string CategoryName
        {
            get { return categoryName; }
            set
            {
                categoryName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CategoryName"));
            }
        }
        private Category selectedCategory;
        public Category SelectedCategory
        {
            get { return selectedCategory; }
            set
            {
                selectedCategory = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedCategory"));
            }
        }
        private ObservableCollection<Category> categoryList;
        public ObservableCollection<Category> CategoryList
        {
            get { return categoryList; }
            set
            {
                categoryList = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CategoryList"));
            }
        }
        public Command cmdProcessTask { get; set; }
        public Command cmdCancelTask { get; set; }
        //-----------
        public Command cmdAddaTask { get; set; }
        public Command cmdDeleteaTask { get; set; }
        public Command cmdUpdateaTask { get; set; }
        public Command cmdMapTask { get; set; }
        //public Command OpenCategoryPageCommand { get; set; }
        public CategoryViewModel()
        {
            cmdProcessTask = new Command(ProcessCategories);
            cmdCancelTask = new Command(CancelCategories);

            cmdAddaTask = new Command(AddCategories);
            cmdDeleteaTask = new Command(DeleteCategories);
            cmdUpdateaTask = new Command(UpdateaTask);
            //OpenCategoryPageCommand = new Command(OpenCategoryPage);

            getCategories();
        }
        private void UpdateaTask(object obj)
        {
            IsViewDetail = true;
            TypeCommand = "Update";
            if (selectedCategory != null)
            {
                CategoryName = selectedCategory.CategoryName;
            }
            else
            {
                IsViewDetail = false;
                typeCommand = string.Empty;
            }
        }
        private async void DeleteCategories(object obj)
        {
            if (selectedCategory != null && selectedCategory.CategoryName != "All")
            {
                IsViewDetail = true;
                TypeCommand = "Delete";
                CategoryName = selectedCategory.CategoryName;
            }
            else
            {
                IsViewDetail = false;
                typeCommand = string.Empty;
                await Application.Current.MainPage.DisplayAlert("Cannot Delete", "The 'All' option cannot be deleted.", "OK");
            }
        }
        private async void AddCategories(object obj)
        {
            IsViewDetail = true;
            TypeCommand = "Add";
            CategoryName = string.Empty;
        }
        private void CancelCategories(object obj)
        {
            IsViewDetail = false;
            typeCommand = string.Empty;
        }
        private async void ProcessCategories(object obj)
        {
            var categories = await App.categoryService.GetCategoriesAsync();

            if (TypeCommand == "Add" || TypeCommand == "Update")
            {
                foreach (var category in categories)
                {
                    if (category.CategoryName == CategoryName)
                    {
                        await Application.Current.MainPage.DisplayAlert("Cannot Add", "Such category already exists.", "OK");
                        TypeCommand = string.Empty;
                        return;
                    }
                }
            }
            if (TypeCommand == "Add")
            {
                var newCategory = new Category { CategoryName = CategoryName };
                await App.categoryService.SaveCategoriesAsync(newCategory);
            }
            else if (TypeCommand == "Update")
            {
                if (SelectedCategory != null)
                {
                    SelectedCategory.CategoryName = CategoryName;
                    await App.categoryService.UpdateCategoriesAsync(SelectedCategory);
                }
            }
            else if (TypeCommand == "Delete")
            {
                if (SelectedCategory != null)
                {
                    await App.categoryService.DeleteCategoriesAsync(SelectedCategory);
                }
            }
            IsViewDetail = false;
            TypeCommand = string.Empty;
            SelectedCategory = null;
            getCategories(); // Make sure to await the method call
        }    
        public async void getCategories()
        {        
            var categories = await App.categoryService.GetCategoriesAsync(); // Call GetCategoriesAsync()

            var observableCollection = new ObservableCollection<Category>(categories);

            CategoryList = observableCollection;
        }
        public void OnAppearing()
        {
            getCategories();
        }

       


    }
}
