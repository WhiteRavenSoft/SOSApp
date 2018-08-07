using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Com.OneSignal;
using Android.Util;
using Android.Widget;
using Android.Views;
//using Android.Gms.Common;

namespace SOSApp.Droid
{
    [Activity(Label = "SOSApp", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, WindowSoftInputMode = SoftInput.AdjustResize, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            base.OnCreate(bundle);
            SetStatusBarColor(new Android.Graphics.Color(107, 0, 125));
            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
            //OneSignal.Current.StartInit((string)(App.Current.Resources["OneSignalId"]))
            //      .EndInit();
            //StartService(new Intent(this, typeof(ServicioNotificaciones)));
        }


    }
}

