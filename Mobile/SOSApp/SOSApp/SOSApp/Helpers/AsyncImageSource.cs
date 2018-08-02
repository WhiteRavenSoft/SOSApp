using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace SOSApp.Helpers
{
    public static class AsyncImageSource
    {
        public static NotifyTaskCompletion<ImageSource> FromTask(Task<ImageSource> task)
        {
            return new NotifyTaskCompletion<ImageSource>(task);
        }

        public static NotifyTaskCompletion<ImageSource> FromUriAndResource(string uri)
        {
            var u = new Uri(uri);
            return FromUriAndResource(u);
        }

        public static NotifyTaskCompletion<ImageSource> FromUriAndResource(Uri uri)
        {
            var t = Task.Run(() => ImageSource.FromUri(uri));
            
            return FromTask(t);
        }
    }
}
