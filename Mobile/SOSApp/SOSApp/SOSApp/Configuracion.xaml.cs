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
	public partial class Configuracion : ContentPage
	{
		public Configuracion ()
		{
			InitializeComponent ();
            int[] zonas = (int[])Newtonsoft.Json.JsonConvert.DeserializeObject(App.Current.Properties["Zona"].ToString(), typeof(int[]));
            StringBuilder valor = new StringBuilder(string.Join(", ", zonas));
            lblDireccion.Text = App.Current.Properties["Direccion"].ToString();
            lblZona.Text = valor.ToString();
        }

        private void CambiarDireccion_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new ConfiguracionInicial(), true);
        }
    }
}