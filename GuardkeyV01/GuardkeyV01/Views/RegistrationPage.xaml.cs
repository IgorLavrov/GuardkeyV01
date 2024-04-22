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
    public partial class RegistrationPage : ContentPage
    {


        public RegistrationPage()
        {
            InitializeComponent();
            WireUpEvents();

        }

        private void WireUpEvents()
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

           
            Preferences.Set("UserPIN", pin);

            
            DisplayAlert("PIN Entered", $"You entered PIN: {pin}", "OK");
            Application.Current.MainPage = new NavigationPage(new SplashPage());
        }

    }
}