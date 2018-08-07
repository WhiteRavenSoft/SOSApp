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
            if (ListaNoticias == null)
            {
                ListaNoticias = new ObservableCollection<Detalle>();
            }

            Noticia not = ApiRest.GetAsyncFormData<Noticia>((string)(App.Current.Resources["APIRoot"])).Result;
            foreach (Detalle d in not.data)
            {
                ListaNoticias.Add(d);
            }

            lvNoticias.ItemsSource = ListaNoticias;
        }

        public Noticias(ObservableCollection<Detalle> listaNoticias)
        {
            InitializeComponent();
            lvNoticias.ItemsSource = listaNoticias;
            ListaNoticias = listaNoticias;
        }

        private void lvNoticias_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                Task<RootDetalle> taskNoticias = ApiRest.GetAsyncFormData<RootDetalle>((string)(App.Current.Resources["APIRoot"]) + "?id=" + ((Detalle)e.SelectedItem).ID);
                taskNoticias.ContinueWith((task) => {
                    Device.BeginInvokeOnMainThread(() => {
                        try
                        {
                            Navigation.PopModalAsync(true);
                            Navigation.PushAsync(new NoticiaDetalle(((RootDetalle)task.Result).data), true);
                        }
                        catch
                        {
                            Navigation.PopModalAsync();
                            Navigation.PushModalAsync(new Loading("Error al obtener los detalles de tu noticia."));
                        }
                    });
                });
                Navigation.PushModalAsync(new Loading("Cargando los detalles de  tu noticia..."), false);
                //RootDetalle not = ApiRest.GetAsyncFormData<RootDetalle>((string)(App.Current.Resources["APIRoot"]) + "?id=" + ((Detalle)e.SelectedItem).ID).Result;
                //Navigation.PushAsync(new NoticiaDetalle(not.data));
            }
            ((ListView)sender).SelectedItem = null;
        }
    }
}
