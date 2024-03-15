
using GuardkeyV01.Services;
using GuardkeyV01.Views;
using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GuardkeyV01
{
    public partial class App : Application
    {
        public string databaseName = "GuardKey";
        public DatabaseCreator _database;

        public App()
        {
            InitializeComponent();

            var pin = Preferences.Get("UserPIN", "");

            if (string.IsNullOrEmpty(pin))
            {

                //Shell.Current.GoToAsync(nameof(RegistrarionPage));

                MainPage = new NavigationPage(new RegistrationPage());
            }
            else
            {

                //Shell.Current.GoToAsync(nameof(LoginPage));

                MainPage = new NavigationPage(new LoginPage());
            }
        }

        protected override void OnStart()
        {
            InitializeDatabase();

        }

        private async Task InitializeDatabase()
        {
           
                if (_database == null)
                {
                    _database = new DatabaseCreator(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), databaseName));
                }

        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
