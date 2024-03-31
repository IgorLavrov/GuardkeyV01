using GuardkeyV01.Models;
using GuardkeyV01.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GuardkeyV01.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewPage : ContentPage
    {
        public Note Record { get; set; }
        private AddNoteViewModel viewModel;
        public ViewPage()
        {
            InitializeComponent();
            viewModel = new AddNoteViewModel();
            BindingContext = viewModel;
            //Record= viewModel.Note; 
        }
        public ViewPage(Note record) : this() // Call the default constructor to initialize the view model
        {
            if (record != null)
            {
                viewModel.Note = record;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            (BindingContext as AddNoteViewModel)?.InitializeFilterOptionsAsync();
        }


        private async void OnBackButtonClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//ListOfCategories");
        }
    }
}