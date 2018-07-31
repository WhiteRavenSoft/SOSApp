using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Geolocator;
using System;
using System.Diagnostics;

namespace SOSApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ConfiguracionInicial : ContentPage
	{
		public ConfiguracionInicial ()
		{
			InitializeComponent ();
        }

        public async void test()
        {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;

            var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(5));
        }

        private void Button_Clicked(object sender, System.EventArgs e)
        {
            
            test();
        }
    }
}