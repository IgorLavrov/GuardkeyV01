using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;

namespace GuardkeyV01.Droid
{
    //[Activity(Label = "GuardkeyV01", Icon = "@drawable/test",  Theme = "@style/MainTheme.Launcher", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    //[Activity(Label = "GuardkeyV01", Icon = "@drawable/test", Theme = "@style/MainTheme.Splash", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.ScreenLayout)]
    // [Activity(Label = "GuardkeyV01", Theme = "@style/MainTheme", MainLauncher =true)]

    [Activity(Label = "GuardkeyV01", Icon = "@drawable/test", MainLauncher = true, Theme = "@style/MainTheme.Launcher")]
    
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}