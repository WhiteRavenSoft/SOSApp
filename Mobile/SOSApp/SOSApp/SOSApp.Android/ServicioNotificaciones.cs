using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Com.OneSignal;
using SOSApp.Helpers;
using SOSApp.TuyoApp.LoQueTenesQueSaber;
using Xamarin.Forms;

namespace SOSApp.Droid
{
    [Service]
    class ServicioNotificaciones : Service
    {
        public override IBinder OnBind(Intent intent)
        {
            throw new NotImplementedException();
        }

        [return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            try
            {
                OneSignal.Current.StartInit((string)(App.Current.Resources["OneSignalId"])).HandleNotificationOpened(result=> {
                });
                OneSignal.Current.StartInit((string)(App.Current.Resources["OneSignalId"])).EndInit();
                return base.OnStartCommand(intent, flags, startId);
            }
            catch
            {
                return base.OnStartCommand(intent, flags, startId);
            }
        }
    }
}