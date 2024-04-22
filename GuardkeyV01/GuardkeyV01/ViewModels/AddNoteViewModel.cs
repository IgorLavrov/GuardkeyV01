using GuardkeyV01.Models;
using GuardkeyV01.Views;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GuardkeyV01.ViewModels
{
    public class AddNoteViewModel : BaseViewModel
    {
        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private ObservableCollection<string> _filterOptions;
        public ObservableCollection<string> FilterOptions
        {
            get => _filterOptions;
            set => SetProperty(ref _filterOptions, value);
        }

        public AddNoteViewModel()
        {

            SaveCommand = new Command(OnSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged += (_, __) => SaveCommand.ChangeCanExecute();

            Note = new Note();


        }

        public async void InitializeFilterOptionsAsync()
        {

            var categories = await App.categoryService.GetAllByCategoriesNameAsync();


            FilterOptions = new ObservableCollection<string>(categories);
        }

        private void OnSave()
        {

            var record = Note;
            App.NoteService.AddUserRecordAsync(record);

            Note = new Note();
            InitializeFilterOptionsAsync();

            Application.Current.MainPage = new AppShell();




        }

        private void OnCancel()
        {
            Note = new Note();
            InitializeFilterOptionsAsync();

            Application.Current.MainPage = new AppShell();



        }
    }
}

