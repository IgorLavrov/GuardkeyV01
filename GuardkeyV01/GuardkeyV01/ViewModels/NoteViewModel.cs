﻿using GuardkeyV01.Models;
using GuardkeyV01.Services;
using GuardkeyV01.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GuardkeyV01.ViewModels
{
    public class NoteViewModel:BaseViewModel
    {

        
        public Command LoadUserRecordCommand { get; }
        public Command AddUserRecordCommand { get; }
        public Command UserRecordTappedEdit { get; }
        public Command UserRecordTappedDelete { get; }
        public Command ViewRecordTapped { get; }
        public Command ViewDetailTapped { get; }
      
      
        public Command ClearRecordCommand { get; }

        public Command SearchCommand { get; }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                if (_searchText != value)
                {
                    _searchText = value;
                    OnPropertyChanged(nameof(SearchText));
                }
            }
        }

        public ObservableCollection<string> FilterOptions { get; set; }
        //public string Key { get; set; }
       

        //private ObservableCollection<GroupedUserRecord> _groupedUserRecords;
        //public ObservableCollection<GroupedUserRecord> GroupedUserRecords
        //{
        //    get { return _groupedUserRecords; }
        //    set
        //    {
        //        _groupedUserRecords = value;
        //        OnPropertyChanged(nameof(GroupedUserRecords));
        //    }
        //}

        private ObservableCollection<Note> _note;
        public ObservableCollection<Note> Notes
        {
            get { return _note; }
            set
            {
                _note = value;
                OnPropertyChanged(nameof(Notes));
            }
        }

        private Category selectedCategory;
        public Category SelectedCategory
        {
            get => selectedCategory;
            set
            {
                if (selectedCategory != value)
                {
                    selectedCategory = value;
                    OnPropertyChanged(nameof(SelectedCategory));
                    FilterItemsAsync(value);
                    
                    
                }
            }
        }


        public async Task FilterItemsAsync(Category selectedCategory)
        {
            try
            {
                IsBusy = true;

                SelectedCategory = selectedCategory; 

               
                await ExecuteLoadUserRecordCommand();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        async Task ExecuteLoadUserRecordCommand()
        {
            IsBusy = true;
            try
            {
                IEnumerable<Note> prodlist;

                if (SelectedCategory == null || SelectedCategory.CategoryName == "ALL")
                {
                    
                    prodlist = await App.NoteService.GetUserRecordsAsync();
                }
                else
                {
                   
                    prodlist = await App.NoteService.SortRecordByPicker(SelectedCategory.CategoryName);
                }

               
                Notes.Clear();
                foreach (var prod in prodlist)
                {
                    Notes.Add(prod);
                }

                // Group the user records
                //var groupedRecords = Notes
                //    .GroupBy(record => record.SourceGroupName)
                //    .Select(group => new GroupedUserRecord(group.Key, group.ToList()));

                //// Update GroupedUserRecords with the grouped records
                //GroupedUserRecords = new ObservableCollection<GroupedUserRecord>(groupedRecords);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }




        private async void InitializeFilterOptionsAsync()
        {
            
            var categories = await App.categoryService.GetAllByCategoriesNameAsync();

           
            FilterOptions = new ObservableCollection<string>(categories);
        }




        public NoteViewModel(INavigation _navigation)
        {
            InitializeFilterOptionsAsync();
            LoadUserRecordCommand = new Command(async () => await ExecuteLoadUserRecordCommand());
            Notes = new ObservableCollection<Note>();
            AddUserRecordCommand = new Command(OnAddUserRecord);
            UserRecordTappedEdit = new Command<Note>(OnEditUserRecord);
            ViewRecordTapped = new Command<Note>(ViewUserRecord);
            ViewDetailTapped = new Command<Note>(ViewDetailRecord);
            
            //GroupedUserRecords = new ObservableCollection<GroupedUserRecord>();
            UserRecordTappedDelete = new Command<Note>(OnDeleteUserRecord);
            ClearRecordCommand = new Command(ClearRecord);
            SearchCommand = new Command(ExecuteSearch);
            Navigation = _navigation;



        }

      

        public async Task RefreshFilterOptionsAsync()
        {
            var categories = await App.categoryService.GetAllByCategoriesNameAsync();
            FilterOptions = new ObservableCollection<string>(categories);
            OnPropertyChanged(nameof(FilterOptions));
        }




        private async void ExecuteSearch()
        {
            string searchText = SearchText;

            IEnumerable<Note> prodlist;


            prodlist = await App.NoteService.SortRecord(searchText);


            UpdateUserRecords(prodlist);
        }
        private void UpdateUserRecords(IEnumerable<Note> records)
        {
            Notes.Clear();
            foreach (var prod in records)
            {
                Notes.Add(prod);
            }

            OnPropertyChanged(nameof(Notes));
        }





        public void ClearRecord()
        {
            Notes.Clear();
        }

        private async void OnDeleteUserRecord(Note record)
        {
            if (record == null)
            {
                return;
            }
            await App.NoteService.DeleteUserRecordAsync(record.NoteId);
            await ExecuteLoadUserRecordCommand();
        }

        private async void OnEditUserRecord(Note record)
        {

            await Navigation.PushAsync(new AddNote(record));
        }

        private async void ViewUserRecord(Note record)
        {

            await Navigation.PushAsync(new ViewPage(record));
        }

        private async void ViewDetailRecord(Note record)
        {

            await Navigation.PushAsync(new DetailPage(record));
        }

        private async void OnAddUserRecord(object obj)
        {
            Shell.Current.GoToAsync(nameof(AddNote));
            //await Application.Current.MainPage.Navigation.PushAsync(new AddUserRecordPage());
        }


        public void OnAppearing()
        {
            IsBusy = true;
        }


        //async Task ExecuteLoadUserRecordCommand()
        //{
        //    IsBusy = true;
        //    try
        //    {
        //        var prodlist = await App.NoteService.GetUserRecordsAsync();

        //        // Update UserRecords with the loaded records
        //        Notes.Clear();
        //        foreach (var prod in prodlist)
        //        {
        //            Notes.Add(prod);
        //        }

        //        // Group the user records
        //        //var groupedRecords = Notes
        //        //    .GroupBy(record => record.SourceGroupName)
        //        //    .Select(group => new GroupedUserRecord(group.Key, group.ToList()));

        //        //// Update GroupedUserRecords with the grouped records
        //        //GroupedUserRecords = new ObservableCollection<GroupedUserRecord>(groupedRecords);
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine(ex);
        //    }
        //    finally
        //    {
        //        IsBusy = false;
        //    }
        //}





    }
}
