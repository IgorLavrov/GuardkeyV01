using GuardkeyV01.Models;
using GuardkeyV01.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GuardkeyV01.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddNote : ContentPage
    {
        public Note Record { get; set; }
        private AddNoteViewModel viewModel;
        public AddNote()
        {
            InitializeComponent();
            viewModel = new AddNoteViewModel();
            BindingContext = viewModel;
            
        }
        public AddNote(Note record) : this() 
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

        private void OnFilterOptionsPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            if (filterOptionsPicker.SelectedItem != null)
            {
               
                viewModel.Note.Categories = filterOptionsPicker.SelectedItem.ToString();
            }
        }
        private void OnShowPasswordChanged(object sender, CheckedChangedEventArgs e)
        {
            PasswordEntry.IsPassword = !e.Value;
        }

       
    }
}