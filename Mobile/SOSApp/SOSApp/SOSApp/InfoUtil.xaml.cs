using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SOSApp.TuyoApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InfoUtil : ContentPage
    {
        public InfoUtil()
        {
            InitializeComponent();
        }

        private void OnTapShowNormativa(object sender, EventArgs e)
        {
            ContentPage normativa = new TuyoApp.InformacionUtil.Normativa();
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(normativa, false);
            Navigation.PushAsync(normativa);
        }
        private void OnTapShowDiagramaRecoleccion(object sender, EventArgs e)
        {
            ContentPage diagramaRecoleccion = new TuyoApp.InformacionUtil.DiagramaRecoleccion();
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(diagramaRecoleccion, false);
            Navigation.PushAsync(diagramaRecoleccion);
        }
    }
}