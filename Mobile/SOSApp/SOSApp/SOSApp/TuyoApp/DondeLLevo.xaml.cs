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
	public partial class DondeLLevo : ContentPage
	{
		public DondeLLevo ()
		{
			InitializeComponent ();
		}

        private void OnTapShowDondeLlevoPlastico(object sender, EventArgs e)
        {
            ContentPage plastico = new TuyoApp.DondeLlevo.Plastico();
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(plastico, false);
            Navigation.PushAsync(plastico);
        }
        private void OnTapShowDondeLlevoPuntosLimpios(object sender, EventArgs e)
        {
            ContentPage puntosLimpios = new TuyoApp.DondeLlevo.PuntosLimpios();
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(puntosLimpios, false);
            Navigation.PushAsync(puntosLimpios);
        }

        private void OnTapShowDondeLlevoBasuraElectronica(object sender, EventArgs e)
        {
            ContentPage basuraElectronica = new TuyoApp.DondeLlevo.BasuraElectronica();
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(basuraElectronica, false);
            Navigation.PushAsync(basuraElectronica);
        }

        private void OnTapShowDondeLlevoPilas(object sender, EventArgs e)
        {
            ContentPage pilas = new TuyoApp.DondeLlevo.Pilas();
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(pilas, false);
            Navigation.PushAsync(pilas);
        }

        private void OnTapShowDondeLlevoPapel(object sender, EventArgs e)
        {
            ContentPage papel = new TuyoApp.DondeLlevo.Papel();
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(papel, false);
            Navigation.PushAsync(papel);
        }

    }
}