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
	public partial class Parking : ContentPage
	{
		public Parking ()
		{
			InitializeComponent ();
            wvBrowser.Navigated += WvBrowser_Navigated;
            wvBrowser.Navigating += WvBrowser_Navigating;
            //if (!App.Current.Properties.ContainsKey("user") && !App.Current.Properties.ContainsKey("password"))
            //{
            //    App.Current.Properties.Add("user", Session.Usuario.email);
            //    App.Current.Properties.Add("password", Session.Usuario.password);
            //}
            //else
            //{
            //    App.Current.Properties["user"] = Session.Usuario.email;
            //    App.Current.Properties["password"] = Session.Usuario.password;
            //}
        }

        private void WvBrowser_Navigating(object sender, WebNavigatingEventArgs e)
        {
            if (e.Url.Contains(App.Current.Resources["URLEstacionamientoMedido"].ToString()))
            { 
                ((WebView)sender).EvaluateJavaScriptAsync("document.getElementById('Identificacion').value")
                .ContinueWith(task =>
                {
                    if (task.Result != null)
                    {
                        if (!App.Current.Properties.ContainsKey("EstacionamientoUser"))
                        {
                            App.Current.Properties.Add("EstacionamientoUser", task.Result);
                        }
                        else
                        {
                            App.Current.Properties["EstacionamientoUser"] = task.Result;
                        }
                    }
                });

            ((WebView)sender).EvaluateJavaScriptAsync("document.getElementById('Password').value")
                .ContinueWith(task =>
                {
                    if (task.Result != null)
                    {
                        if (!App.Current.Properties.ContainsKey("EstacionamientoPass"))
                        {
                            App.Current.Properties.Add("EstacionamientoPass", task.Result);
                        }
                        else
                        {
                            App.Current.Properties["EstacionamientoPass"] = task.Result;
                        }
                    }
                });
            }
        }

        private void WvBrowser_Navigated(object sender, WebNavigatedEventArgs e)
        {
            if (e.Url.Contains(App.Current.Resources["URLEstacionamientoMedido"].ToString()))
            {
                if (App.Current.Properties.ContainsKey("EstacionamientoPass") && App.Current.Properties["EstacionamientoPass"] != null)
                {
                    ((WebView)sender).EvaluateJavaScriptAsync("document.getElementById('Password').value = " + App.Current.Properties["EstacionamientoPass"].ToString());
                }

                if (App.Current.Properties.ContainsKey("EstacionamientoUser") && App.Current.Properties["EstacionamientoUser"] != null)
                {
                    ((WebView)sender).EvaluateJavaScriptAsync("document.getElementById('Identificacion').value = " + App.Current.Properties["EstacionamientoUser"].ToString());
                }
            }
            Device.BeginInvokeOnMainThread(() => {
                Navigation.PopModalAsync(true);
            });
        }
    }
}