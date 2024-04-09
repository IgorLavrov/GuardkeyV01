using GuardkeyV01.Models;
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
            bool deleteConfirmed = await Application.Current.MainPage.DisplayAlert("Confirmation", "Are you sure you want to delete this record?", "Yes", "No");

            if (deleteConfirmed)
            {
                try
                {
                    
                    await App.NoteService.DeleteUserRecordAsync(record.NoteId);

                  
                    await ExecuteLoadUserRecordCommand();
                }
                catch (Exception ex)
                {
                   
                    await Application.Current.MainPage.DisplayAlert("Error", $"Failed to delete the record: {ex.Message}", "OK");
                }
            }
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
          
        }


        public void OnAppearing()
        {
            IsBusy = true;
        }



    }
}
