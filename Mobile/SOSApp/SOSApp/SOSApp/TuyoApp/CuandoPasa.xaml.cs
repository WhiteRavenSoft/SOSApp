using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SOSApp.TuyoApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CuandoPasa : ContentPage
	{
		public CuandoPasa ()
		{
			InitializeComponent ();
            
		}

        private void wAreas_Navigated(object sender, WebNavigatedEventArgs e)
        {
            if (e.Url.Contains((string)App.Current.Resources["URLCuandoPasa"]))
            {
                ((WebView)sender).Eval("document.body.style.zoom = .5");
            }
        }
    }
}