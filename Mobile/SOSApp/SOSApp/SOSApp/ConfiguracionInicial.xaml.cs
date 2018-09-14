using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Geolocator;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading.Tasks;
using SOSApp.Helpers;
using System.Linq;
using System.Globalization;

namespace SOSApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ConfiguracionInicial : ContentPage
	{
		public ConfiguracionInicial ()
		{
			InitializeComponent ();
            if (App.Current.Properties.ContainsKey("Direccion"))
            {
                txtDireccion.Text = App.Current.Properties["Direccion"].ToString();
            }
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

                List<KeyValuePair<string, string>> listaParams = new List<KeyValuePair<string, string>>();
                listaParams.Add(new KeyValuePair<string, string>("PlayerID", App.PlayerId));
                listaParams.Add(new KeyValuePair<string, string>("Address", txtDireccion.Text));

                Task<string> taskRegistro = ApiRest.PostFormData<string>((string)(App.Current.Resources["APIRegistro"]), listaParams);

                taskRegistro.ContinueWith((task) =>
                {
                    //Deserializo el resultado (JSON) a un array de int
                    int[] result = (int[])Newtonsoft.Json.JsonConvert.DeserializeObject(task.Result, typeof(int[]));
                    //Si el array contiene sólo un valor quiere decir que el resultado no pertenece a una región de la cuidad
                    if (result.Length == 1)
                    {
                        if (result[0] == 0)
                        {
                            Navigation.PopModalAsync();
                            lblError.Text = "Tu dirección no pertenece a ninguna zona.";
                            return;
                        }
                        else if (result[0] == -1)
                        {
                            Navigation.PopModalAsync();
                            lblError.Text = "La dirección no pertenece a la cuidad de Sunchales.";
                            return;
                        }
                    }
                    else
                    {
                        if (!App.Current.Properties.ContainsKey("Zona"))
                        {
                            App.Current.Properties.Add("Zona", task.Result);
                        }
                        else
                        {
                            App.Current.Properties["Zona"] = task.Result;
                        }

                        CultureInfo cultureInfo = CultureInfo.CurrentCulture;
                        TextInfo textInfo = cultureInfo.TextInfo;

                        if (!App.Current.Properties.ContainsKey("Direccion"))
                        {
                            App.Current.Properties.Add("Direccion", textInfo.ToTitleCase(txtDireccion.Text));
                        }
                        else
                        {
                            App.Current.Properties["Direccion"] = textInfo.ToTitleCase(txtDireccion.Text);
                        }
                        Navigation.PopModalAsync();
                        NavigationPage np = new NavigationPage(new MainPage());
                        Application.Current.MainPage = np;
                    }
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