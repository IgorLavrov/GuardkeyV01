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
	public partial class ContactList : ContentPage
	{
        NoteViewModel userRecordViewModel;
        //private Category selectedCategory;

        public ContactList()
        {

            InitializeComponent();
            BindingContext = userRecordViewModel = new NoteViewModel(Navigation);


            myCollectionView.SelectionChanged += OnItemSelected;

        }
        private async void OnItemSelected(object sender, SelectionChangedEventArgs e)
        {
            myCollectionView.SelectionChanged -= OnItemSelected;
            // Check if any item is selected
            if (e.CurrentSelection.FirstOrDefault() is Note selectedNote)
            {
                var message = $"Find your password \n" +
                              $"For: {selectedNote.ResourceName}\n" +
                              $"Username: {selectedNote.UserName}\n" +
                              $"Password: {selectedNote.Password}" +
                              $"Description: {selectedNote.Description}\n" ;

                // Display action sheet and get the selected action
                var action = await DisplayActionSheet("Send via", "Cancel", null, "SMS", "Email");

                switch (action)
                {
                    case "SMS":
                        string phoneNumber = await DisplayPromptAsync("Phone number", "Enter Phone number", "OK", "Cancel");
                        if (!string.IsNullOrEmpty(phoneNumber))
                            await SendSMS(phoneNumber, message);
                        break;
                    case "Email":
                        string emailAddress = await DisplayPromptAsync("Email Address", "Enter email address", "OK", "Cancel");
                        if (!string.IsNullOrEmpty(emailAddress))
                            await SendEmail(emailAddress, message);
                        break;
                }

                // Deselect the item after handling
                //((CollectionView)sender).SelectedItem = null;
                Device.BeginInvokeOnMainThread(() =>
                {
                    ((CollectionView)sender).SelectedItem = null; // Deselect the item
                });


            }
            myCollectionView.SelectionChanged += OnItemSelected;
        }

       

        private async Task SendSMS(string phoneNumber, string message)
        {
            string smsPhoneNumber = phoneNumber;
            string smsText = message;

            if (string.IsNullOrEmpty(smsPhoneNumber))
            {
                return;
            }

            if (Device.RuntimePlatform == Device.iOS)
            {
                Device.OpenUri(new Uri($"sms:{smsPhoneNumber}&body={smsText}"));
            }
            else if (Device.RuntimePlatform == Device.Android)
            {
                Device.OpenUri(new Uri($"sms:{smsPhoneNumber}?body={smsText}"));
            }
            //await DisplayAlert("SMS Sent", $"Sending SMS to {phoneNumber}:\n\n{message}", "OK");
        }

        private async Task SendEmail(string emailAddress, string message)
        {
            string toEmail = emailAddress;
            string emailSubject = "Password";
            string emailBody = message;

            if (string.IsNullOrEmpty(toEmail))
            {
                return;
            }

            Device.OpenUri(new Uri($"mailto:{toEmail}?subject={emailSubject}&body={emailBody}"));
        }
        //await DisplayAlert("Email Sent", $"Sending email to {emailAddress}:\n\n{message}", "OK");
    



        protected override void OnAppearing()
        {
            base.OnAppearing();
          
            userRecordViewModel.RefreshFilterOptionsAsync();
            userRecordViewModel.OnAppearing();

        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            //myCollectionView.SelectionChanged = OnItemSelected;
        }
    }
}