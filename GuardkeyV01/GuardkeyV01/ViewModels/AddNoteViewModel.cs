using GalaSoft.MvvmLight.Views;
using GuardkeyV01.Models;
using GuardkeyV01.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace GuardkeyV01.ViewModels
{
    public class AddNoteViewModel:BaseViewModel
    {
        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private ObservableCollection<string> _filterOptions;

        public ObservableCollection<string> FilterOptions
        {
            get => _filterOptions;
            set => SetProperty(ref _filterOptions, value);
        }


        private async void InitializeFilterOptionsAsync()
        {
            // GetCategoriesAsync is asynchronous, so use await
            var categories = await App.categoryService.GetAllByCategoriesNameAsync();

            // Initialize the FilterOptions once the categories are retrieved
            FilterOptions = new ObservableCollection<string>(categories);
        }
        public AddNoteViewModel()
        {
            InitializeFilterOptionsAsync();

            SaveCommand = new Command(OnSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();

            Note = new Note();
        }


        private async void OnSave()
        {
            var record = Note;
            await App.NoteService.AddUserRecordAsync(record);

           
            
                await Shell.Current.GoToAsync($"//{nameof(NotePage)}");

        }

        private async void OnCancel()
        {
          
            await Shell.Current.GoToAsync($"//{nameof(ListOfCategories)}");









        }
    }
}
