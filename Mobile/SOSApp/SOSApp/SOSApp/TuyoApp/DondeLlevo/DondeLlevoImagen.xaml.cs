using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SOSApp.TuyoApp.DondeLlevo
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DondeLlevoImagen : ContentPage
	{
		public DondeLlevoImagen (string source)
		{
			InitializeComponent ();
            img.Source = source;
		}
	}
}