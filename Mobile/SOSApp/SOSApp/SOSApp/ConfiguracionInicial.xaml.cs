using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Geolocator;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading.Tasks;
using SOSApp.Helpers;
using System.Linq;

namespace SOSApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ConfiguracionInicial : ContentPage
	{
		public ConfiguracionInicial ()
		{
			InitializeComponent ();
        }

        private void Aceptar_Clicked(object sender, EventArgs e)
        {
            if (txtDireccion.Text != "" && txtDireccion.Text != string.Empty && txtDireccion.Text != null)
            {
                if (!txtDireccion.Text.Any(char.IsDigit))
                {
                    lblError.Text = "Por favor ingrese una dirección válida.";
                    return;
                }
                App.DireccionUsuario = txtDireccion.Text;
                //App.PlayerId;//Id de dispositivo en onesignal

                Task<object> taskRegistro = ApiRest.GetFormData<object>((string)(App.Current.Resources["APIRegistro"]));

                taskRegistro.ContinueWith((task) =>
                {

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Navigation.PopModalAsync(true);
                        NavigationPage np = new NavigationPage(new MainPage());
                        Application.Current.MainPage = np;
                    });
                });
                Navigation.PushModalAsync(new Loading("Obteniendo tu ubicación..."), false);


                //var response = Helpers.ApiRest.PostFormData(App.Current.Properties["APIRegistro"].ToString(), new List<KeyValuePair<string, string>>() { new KeyValuePair<string, string>("direccion", txtDireccion.Text), new KeyValuePair<string, string>("playerid", App.PlayerId) });
                //Application.Current.MainPage = new Tuyo();
            }
            else
            {
                lblError.Text = "Por favor ingrese su dirección para continuar";
            }
        }
    }
}