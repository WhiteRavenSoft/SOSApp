using Xamarin.Forms;
using Com.OneSignal;
using System.Threading.Tasks;
using SOSApp.TuyoApp.LoQueTenesQueSaber;
using SOSApp.Helpers;
using System.Threading;

namespace SOSApp
{
    public partial class App : Application
    {
        public static string PlayerId = "";
        public static string DireccionUsuario = "";
        public static RootDetalle notificacionNoticia = null;
		public App ()
		{
			InitializeComponent();
            if (!App.Current.Properties.ContainsKey("DireccionUsuario"))
            {
                App.Current.Properties.Add("DireccionUsuario", App.DireccionUsuario);
            }
            else
            {
                DireccionUsuario = App.Current.Properties["DireccionUsuario"].ToString();
            }
            if (DireccionUsuario != string.Empty)
            {
                MainPage = new NavigationPage(new MainPage());
            }
            else
            {
                MainPage = new NavigationPage(new ConfiguracionInicial());
            }

            MainPage.Appearing += MainPage_Appearing;

            ThreadStart tsNot = new ThreadStart(() => {
                try
                {
                    OneSignal.Current.StartInit((string)(App.Current.Resources["OneSignalId"])).HandleNotificationReceived(async (result) =>
                    {
                        if (result.payload.additionalData.Count > 0 && result.payload.additionalData.ContainsKey("noticia"))
                        {
                            try
                            {
                                await ApiRest.GetAsyncFormData<RootDetalle>((string)(App.Current.Resources["APIRoot"]) + "?id=" + result.payload.additionalData["noticia"])
                                .ContinueWith((task) =>
                                {
                                    try
                                    {
                                        App.notificacionNoticia = ((RootDetalle)task.Result);
                                    }
                                    catch
                                    {
                                    }
                                });
                            }
                            catch
                            { }

                        }
                    }).InFocusDisplaying(Com.OneSignal.Abstractions.OSInFocusDisplayOption.Notification)
                .EndInit();
                }
                catch
                {
                }
            });
            Thread tNotificaciones = new Thread(tsNot);
            tNotificaciones.Start();

            OneSignal.Current.IdsAvailable((id, token) => {
                PlayerId = id;
                System.Diagnostics.Debug.Write("One Signal Player ID: " + id);
            });
        }

        private void MainPage_Appearing(object sender, System.EventArgs e)
        {

            if (App.notificacionNoticia != null)
            {
                MainPage.Navigation.PushAsync(new NoticiaDetalle(App.notificacionNoticia.data));
                App.notificacionNoticia = null;
            }
        }

        protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
            // Handle when your app sleeps

            if (!App.Current.Properties.ContainsKey("DireccionUsuario") )
            {
                App.Current.Properties.Add("DireccionUsuario", App.DireccionUsuario);
            }
            else
            {
                App.Current.Properties["DireccionUsuario"] = App.DireccionUsuario;
            }
        }

		protected override void OnResume ()
		{
            // Handle when your app resumes
        }






        //.HandleNotificationOpened((result) => {
        //    if (result.notification.payload.additionalData.Count > 0)
        //    {
        //        if (result.notification.payload.additionalData.ContainsKey("noticia"))
        //        {
        //            if (noticiaNotificacion != null && noticiaNotificacion.Code == int.Parse(result.notification.payload.additionalData["noticia"].ToString()))
        //            {
        //                MainPage.Navigation.PopModalAsync(true);
        //                MainPage.Navigation.PushAsync(new NoticiaDetalle(noticiaNotificacion.data), true);
        //            }
        //            else
        //            {
        //                MainPage.Navigation.PushModalAsync(new Loading("Cargando los detalles de  tu noticia..."), false);

        //                ApiRest.GetAsyncFormData<RootDetalle>((string)(App.Current.Resources["APIRoot"]) + "?id=" + result.notification.payload.additionalData["noticia"])
        //                .ContinueWith((task) => {
        //                    try
        //                    {
        //                        while (MainPage.IsBusy)
        //                        {
        //                            Thread.Sleep(200);
        //                        }
        //                        MainPage.Navigation.PopModalAsync(true);
        //                        MainPage.Navigation.PushAsync(new NoticiaDetalle(((RootDetalle)task.Result).data), true);
        //                    }
        //                    catch
        //                    {
        //                        MainPage.Navigation.PopModalAsync();
        //                        MainPage.Navigation.PushModalAsync(new Loading("Error al obtener los detalles de tu noticia."));
        //                    }
        //                });
        //            }
        //        }
        //    }
        //})
	}
}
