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
    public partial class NotePage : ContentPage
    {

        NoteViewModel userRecordViewModel;
        public NotePage()
        {

            InitializeComponent();
            BindingContext = userRecordViewModel = new NoteViewModel(Navigation);

        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
         
            userRecordViewModel.RefreshFilterOptionsAsync();
            userRecordViewModel.OnAppearing();
        }

    }
}