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
	public partial class DetailPage : ContentPage
	{
        public Note Record { get; set; }
        private AddNoteViewModel viewModel;
        public DetailPage()
        {
            InitializeComponent();
            viewModel = new AddNoteViewModel();
            BindingContext = viewModel;
          
        }
        public DetailPage(Note record) : this() 
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


     
    }
}
