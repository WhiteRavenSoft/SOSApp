using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SOSApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Tuyo : ContentPage
	{
		public Tuyo ()
		{
			InitializeComponent ();
		}

        void OnTapShowCuandoPasa(object sender, EventArgs args)
        {
            var imageSender = (Image)sender;
            Navigation.PushAsync(new TuyoApp.CuandoPasa());
        }

        void OnTapShowDondeLLevo(object sender, EventArgs args)
        {
            var imageSender = (Image)sender;
            Navigation.PushAsync(new TuyoApp.DondeLLevo());
        }

        void OnTapShowInfoUtil(object sender, EventArgs args)
        {
            var imageSender = (Image)sender;
            Navigation.PushAsync(new TuyoApp.InfoUtil());
        }

        private void OnTapLoQueTenesQueSaber(object sender, EventArgs e)
        {
            Navigation.PushAsync(new TuyoApp.LoQueTenesQueSaber.Noticias());
        }
    }
}