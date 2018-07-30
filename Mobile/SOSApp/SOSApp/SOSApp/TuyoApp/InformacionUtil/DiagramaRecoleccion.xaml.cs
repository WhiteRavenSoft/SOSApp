using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SOSApp.TuyoApp.InformacionUtil
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DiagramaRecoleccion : ContentPage
	{
		public DiagramaRecoleccion ()
		{
			InitializeComponent ();
        }

        private void btnArea1_Clicked(object sender, EventArgs e)
        {
            MostrarArea1();
        }

        private void btnArea2_Clicked(object sender, EventArgs e)
        {
            MostrarArea2();
        }

        private void btnArea3_Clicked(object sender, EventArgs e)
        {
            MostrarArea3();
        }

        private void btnArea4_Clicked(object sender, EventArgs e)
        {
            MostrarArea4();
        }

        private void MostrarArea1()
        {
            lblOrganicosRecuperables.Text = "Lunes, miércoles y viernes";
            lblNoRecuperables.Text = "Martes y viernes";
            lblLimpiezaPatio.Text = "Lunes";
        }

        private void MostrarArea2()
        {
            lblOrganicosRecuperables.Text = "Lunes, miércoles y viernes";
            lblNoRecuperables.Text = "Martes y viernes";
            lblLimpiezaPatio.Text = "Martes";
        }

        private void MostrarArea3()
        {
            lblOrganicosRecuperables.Text = "Martes, jueves y sábados";
            lblNoRecuperables.Text = "Lunes y jueves";
            lblLimpiezaPatio.Text = "Miércoles";
        }

        private void MostrarArea4()
        {
            lblOrganicosRecuperables.Text = "Martes, jueves y sábados";
            lblNoRecuperables.Text = "Lunes y jueves";
            lblLimpiezaPatio.Text = "Jueves";
        }

        private void TapGestureRecognizer_OrganicosRecuperables(object sender, EventArgs e)
        {
            slMapa.IsVisible= false;
            slResiduosOrganicos.IsVisible = true;
            slLimpiezaPatio.IsVisible = false;
            slResiduosNoRecuperables.IsVisible = false;
        }
        private void TapGestureRecognizer_NoRecuperables(object sender, EventArgs e)
        {
            slMapa.IsVisible = false;
            slResiduosOrganicos.IsVisible = false;
            slLimpiezaPatio.IsVisible = false;
            slResiduosNoRecuperables.IsVisible = true;
        }
        private void TapGestureRecognizer_LimpiezaPatio(object sender, EventArgs e)
        {
            slMapa.IsVisible = false;
            slResiduosOrganicos.IsVisible = false;
            slLimpiezaPatio.IsVisible = true;
            slResiduosNoRecuperables.IsVisible = false;
        }
    }
}