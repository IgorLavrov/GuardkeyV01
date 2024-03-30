using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GuardkeyV01.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {

        public LoginPage()
        {
            InitializeComponent();
            Shell.SetNavBarIsVisible(this, false);
            Wire1UpEvents();

        }
        private void Wire1UpEvents()
        {
            Digit1.TextChanged += (s, e) => MoveToNextEntry(Digit1, Digit2);
            Digit2.TextChanged += (s, e) => MoveToNextEntry(Digit2, Digit3);
            Digit3.TextChanged += (s, e) => MoveToNextEntry(Digit3, Digit4);
            Digit4.TextChanged += (s, e) => HandlePinEntryComplete();
        }

        private void MoveToNextEntry(Entry currentEntry, Entry nextEntry)
        {
            if (!string.IsNullOrEmpty(currentEntry.Text))
                nextEntry.Focus();
        }



        private void HandlePinEntryComplete()
        {
            string pin = $"{Digit1.Text}{Digit2.Text}{Digit3.Text}{Digit4.Text}";


            var correctpin = Preferences.Get("UserPIN", "");

            if (pin == correctpin)
            {
                Application.Current.MainPage = new NavigationPage(new SplashPage());

            }
            else
            {

                DisplayAlert("Incorrect PIN", $"You entered an incorrect PIN: {pin}", "OK");
                Application.Current.MainPage = new NavigationPage(new LoginPage());
                Digit1.Focus();

            }



        }

    }
}