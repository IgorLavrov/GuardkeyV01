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



        string selectedFilter = "All";
        public string SelectedFilter
        {
            get => selectedFilter;
            set
            {
                if (SetProperty(ref selectedFilter, value))
                    FilterItemsAsync();
            }
        }




        private async void InitializeFilterOptionsAsync()
        {
            // GetCategoriesAsync is asynchronous, so use await
            var categories = await App.categoryService.GetAllByCategoriesNameAsync();

            // Initialize the FilterOptions once the categories are retrieved
            FilterOptions = new ObservableCollection<string>(categories);
        }




        public NoteViewModel(INavigation _navigation)
        {
            InitializeFilterOptionsAsync();
            LoadUserRecordCommand = new Command(async () => await ExecuteLoadUserRecordCommand());
            Notes = new ObservableCollection<Note>();
            AddUserRecordCommand = new Command(OnAddUserRecord);
            //UserRecordTappedEdit = new Command<Note>(OnEditUserRecord);
            //GroupedUserRecords = new ObservableCollection<GroupedUserRecord>();
            UserRecordTappedDelete = new Command<Note>(OnDeleteUserRecord);
            ClearRecordCommand = new Command(ClearRecord);
            SearchCommand = new Command(ExecuteSearch);
            Navigation = _navigation;



        }

        public NoteViewModel()
        {

        }

        public async Task RefreshFilterOptionsAsync()
        {
            var categories = await App.categoryService.GetAllByCategoriesNameAsync();
            FilterOptions = new ObservableCollection<string>(categories);
            OnPropertyChanged(nameof(FilterOptions));
        }

        async Task FilterItemsAsync()
        {


            try
            {
                IsBusy = true;

                if (SelectedFilter == "All")
                {
                    // Load all records
                    await ExecuteLoadUserRecordCommand();
                }
                else
                {
                    IEnumerable<Note> filteredRecords;
                    // Filter records based on the selected option
                    filteredRecords = await App.NoteService.SortRecordByPicker(SelectedFilter);

                    // Clear the existing records and add the filtered ones
                    Notes.Clear();
                    foreach (var record in filteredRecords)
                    {
                        Notes.Add(record);
                    }

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

        //private async void OnEditUserRecord(Note record)
        //{

        //    await Navigation.PushAsync(new AddNote(record));
        //}


        private async void OnAddUserRecord(object obj)
        {
            Shell.Current.GoToAsync(nameof(AddNote));
            //await Application.Current.MainPage.Navigation.PushAsync(new AddUserRecordPage());
        }

        public void OnAppearing()
        {
            IsBusy = true;
        }


        async Task ExecuteLoadUserRecordCommand()
        {
            IsBusy = true;
            try
            {
                var prodlist = await App.NoteService.GetUserRecordsAsync();

                // Update UserRecords with the loaded records
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





    }
}
