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
            //Record= viewModel.Note; 
        }
        public AddNote(Note record) : this() // Call the default constructor to initialize the view model
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
                // Update the Categories property of the Note object
                viewModel.Note.Categories = filterOptionsPicker.SelectedItem.ToString();
            }
        }

        //public AddNote(Note record)
        //{
        //    InitializeComponent();
        //    viewModel = new AddNoteViewModel();

        //    if (record != null)
        //    {
        //        viewModel.Note = record;
        //    }

        //    BindingContext = viewModel;
        //    record= viewModel.Note;
        //}

        //private void OnFilterOptionsPickerSelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (filterOptionsPicker.SelectedItem != null)
        //    {
        //        if (Record == null)
        //        {
        //            Record = new Note();
        //        }

        //        // Update the Categories property of the Note object
        //        Record.Categories = filterOptionsPicker.SelectedItem.ToString();
        //    }
        //}


        //private void OnFilterOptionsPickerSelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (filterOptionsPicker.SelectedItem != null)
        //    {

        //        if (Record == null)
        //        {
        //            Record = new Note();
        //        }
        //        if (Record.Categories == null)
        //        {
        //            Record.Categories = new List<Category>();
        //        }


        //        Category selectedCategory = new Category { CategoryName = filterOptionsPicker.SelectedItem.ToString() };

        //        Record.Categories.Add(selectedCategory);
        //    }
        //}
    }
}