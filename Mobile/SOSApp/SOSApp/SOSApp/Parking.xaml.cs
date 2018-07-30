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
	}
}