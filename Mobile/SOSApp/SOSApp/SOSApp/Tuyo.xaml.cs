using SOSApp.Helpers;
using SOSApp.TuyoApp.LoQueTenesQueSaber;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            Task<Noticia> taskNoticias = ApiRest.GetFormData<Noticia>((string)(App.Current.Resources["APIRoot"]));
            
            taskNoticias.ContinueWith((task) => {
                try
                {
                    ObservableCollection<Detalle> ListaNoticias = new ObservableCollection<Detalle>();
                    foreach (Detalle d in ((Noticia)task.Result).data)
                    {
                        ListaNoticias.Add(d);
                    }
                    Device.BeginInvokeOnMainThread(() => {
                        Navigation.PopModalAsync(true);
                        Navigation.PushAsync(new Noticias(ListaNoticias), true);
                    });
                }
                catch
                {
                    Device.BeginInvokeOnMainThread(new Action(() => {
                        Navigation.PopModalAsync(false);
                    Navigation.PushModalAsync(new Loading("No se pudieron cargar tus noticias"), false);
                    }));
                }
            });
            Navigation.PushModalAsync(new Loading("Cargando noticias..."), false);
        }
    }
}