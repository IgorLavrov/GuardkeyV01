
using GuardkeyV01.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace GuardkeyV01
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();


            Routing.RegisterRoute(nameof(ListOfCategories), typeof(ListOfCategories));
            Routing.RegisterRoute(nameof(AddCategory), typeof(AddCategory));

        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }


    }
}
