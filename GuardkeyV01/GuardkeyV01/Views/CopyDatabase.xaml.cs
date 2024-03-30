using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using System;
using System.IO;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GuardkeyV01.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CopyDatabase : ContentPage
    {
        public CopyDatabase()
        {
            InitializeComponent();
        }

        private async void OnCopyDatabaseClicked(object sender, EventArgs e)
        {
            try
            {
                // Request permission to read external storage if not already granted
                var status = await Permissions.RequestAsync<Permissions.StorageRead>();
                if (status != PermissionStatus.Granted)
                {
                    // Permission not granted, handle accordingly
                    return;
                }

                // Use FilePicker to select the database file
                FileData fileData = await CrossFilePicker.Current.PickFile();

                if (fileData == null)
                {
                    // User canceled the file picking operation
                    return;
                }

                // Get the selected file path
                string sourcePath = fileData.FilePath;

                // Set the destination file path in the Downloads directory
                string destinationFilePath = Path.Combine(FileSystem.CacheDirectory, "GuardKey");

                // Copy the database file to the destination path
                File.Copy(sourcePath, destinationFilePath, true);

                // Display success message
                await DisplayAlert("Success", "Database copied and downloaded successfully.", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }

    }
}
