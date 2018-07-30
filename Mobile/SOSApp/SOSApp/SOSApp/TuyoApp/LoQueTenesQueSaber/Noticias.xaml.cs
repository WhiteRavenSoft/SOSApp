using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SOSApp.Helpers;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SOSApp.TuyoApp.LoQueTenesQueSaber
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Noticias : ContentPage
	{
        public ObservableCollection<Detalle> ListaNoticias { get; set; }
        public Noticias ()
		{
            InitializeComponent ();

            ListaNoticias = new ObservableCollection<Detalle>();
            Noticia not = ApiRest.GetAsyncFormData<Noticia>((string)(App.Current.Resources["APIRoot"])).Result;
            foreach (Detalle d in not.data)
            {
                ListaNoticias.Add(d);
            }

            lvNoticias.ItemsSource = ListaNoticias;


        }

        private void lvNoticias_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                RootDetalle not = ApiRest.GetAsyncFormData<RootDetalle>((string)(App.Current.Resources["APIRoot"]) + "?id=" + ((Detalle)e.SelectedItem).ID).Result;
                Navigation.PushAsync(new NoticiaDetalle(not.data));
            }
            ((ListView)sender).SelectedItem = null;
        }
    }
}