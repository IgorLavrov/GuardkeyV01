
using GuardkeyV01.ViewModels;
using GuardkeyV01.Views;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace GuardkeyV01
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            BindingContext = this;

            Routing.RegisterRoute(nameof(ListOfCategories), typeof(ListOfCategories));
            Routing.RegisterRoute(nameof(AddCategory), typeof(AddCategory));
            Routing.RegisterRoute(nameof(AddNote), typeof(AddNote));
            Routing.RegisterRoute(nameof(NotePage), typeof(NotePage));
            Routing.RegisterRoute(nameof(ContactList), typeof(ContactList));
            Routing.RegisterRoute(nameof(ViewPage), typeof(ViewPage));
            Routing.RegisterRoute(nameof(ListOfNotes), typeof(ListOfNotes));
            Routing.RegisterRoute(nameof(Instructions), typeof(Instructions));


        }




        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }


        public ICommand HelpCommand => new Command(async () =>
            {
                var url = "https://cubixedu.com/";
                await Browser.OpenAsync(url,BrowserLaunchMode.SystemPreferred);
            });


        



    }
}
