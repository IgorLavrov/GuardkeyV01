using GuardkeyV01.Models;
using GuardkeyV01.ViewModels;
using System;
using System.Linq;
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
           
        }
        public ViewPage(Note record) : this() 
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


        private  void OnBackButtonClicked(object sender, EventArgs e)
        {
           

            Shell.Current.GoToAsync("//ListOfCategories");
        }
    }
}