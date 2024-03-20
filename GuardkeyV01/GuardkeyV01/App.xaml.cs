
using GuardkeyV01.Services;
using GuardkeyV01.Views;
using SQLite;
using System;
using System.IO;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GuardkeyV01
{
    public partial class App : Application
    {
        public string databaseName = "GuardKey";
     
        public static DatabaseCreator _database;
        public static CategoryService categoryService;
        public static NoteService NoteService;

        public App()
        {
            InitializeComponent();


            _database = new DatabaseCreator(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), databaseName));
            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), databaseName);
            var connection = new SQLiteAsyncConnection(databasePath);
            categoryService = new CategoryService(connection);
            NoteService = new NoteService(connection);


            var pin = Preferences.Get("UserPIN", "");

            if (string.IsNullOrEmpty(pin))
            {
                MainPage = new NavigationPage(new RegistrationPage());
            }
            else
            {
                MainPage = new NavigationPage(new LoginPage());
            }
        }
        protected override async void OnStart()
        {
            await InitializeDatabase();
        }
        private async Task InitializeDatabase()
        {
            if (_database == null)
            {
                try
                {
                    _database = new DatabaseCreator(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), databaseName));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Database initialization error: {ex.Message}");
                }
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
