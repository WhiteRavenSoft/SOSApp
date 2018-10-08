using SOSApp.Helpers;
using SOSApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace SOSApp.TuyoApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CuandoPasa : ContentPage
	{
        public CuandoPasa()
        {
            InitializeComponent();

            UserMobileLocation taskCuandoPasa = ApiRest.GetAsyncFormData<UserMobileLocation>((string)App.Current.Resources["URLUserLocation"] + App.PlayerId).Result;

            var url = new UrlWebViewSource
            {
                Url = (string)Application.Current.Resources["URLCuandoPasa"] + "?lat=" + taskCuandoPasa.Lat + "&long=" + taskCuandoPasa.Lon + "&fa=" + GetActualDateJsonFormated()
            };
            wAreas.Source = url;
        }

        private string GetActualDateJsonFormated()
        {
            var date = DateTime.Now;
            return $"{date.Year}-{date.Month}-{date.Day}T{date.Hour}:{date.Minute}";
        }

        private void wAreas_Navigated(object sender, WebNavigatedEventArgs e)
        {
            ((WebView)sender).EvaluateJavaScriptAsync("document.getElementsByTagName('body').style.backgroundColor = transparent");
            //((WebView)sender).Eval("document.body.style.backgroundColor = transparent");
            if (e.Url.Contains((string)App.Current.Resources["URLCuandoPasa"]))
            {
                //((WebView)sender).Eval("document.body.style.zoom = .5");
            }
        }
    }
}