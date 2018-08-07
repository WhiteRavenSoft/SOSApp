using SOSApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SOSApp
{
	public partial class MainPage : ContentPage
	{
        
		public MainPage()
		{
			InitializeComponent();
        }

        void OnTapShowTuyo(object sender, EventArgs args)
        {
            var imageSender = (Image)sender;
            Navigation.PushAsync(new Tuyo());
        }

        void OnTapShowParking(object sender, EventArgs args)
        {
            var imageSender = (Image)sender;
            Navigation.PushModalAsync(new Loading(), false);
            Navigation.PushAsync(new Parking(), true);
        }

        void OnTapShowSiac(object sender, EventArgs args)
        {
            Device.OpenUri(new Uri("tel:" + (string)(App.Current.Resources["NumeroSiac"])));
        }
    }
}
