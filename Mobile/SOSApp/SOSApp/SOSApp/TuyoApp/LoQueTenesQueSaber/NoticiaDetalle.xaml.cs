using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace SOSApp.TuyoApp.LoQueTenesQueSaber
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NoticiaDetalle : ContentPage
    {
        //static bool banderaTamañoImagen = false;
        //static double tamañoImagen = -1;
        //double scrollAnterior = 0;

        public double totalScroll { get; set; }
        public NoticiaDetalle(Detalle noticia)
        {
            InitializeComponent();
            lblFecha.Text = noticia.Date.ToShortDateString();
            lblTitulo.Text = noticia.Title;
            lblCopete.Text = noticia.Important;
            lblContenido.Text = noticia.Body;
            imagenNoticia.Source = noticia.Image;
        }

        //private void ScrollView_Scrolled(object sender, ScrolledEventArgs e)
        //{
        //    if (!banderaTamañoImagen)
        //    {
        //        tamañoImagen = imagenNoticia.Height;
        //        banderaTamañoImagen = true;
        //    }
        //    if (scrollAnterior < e.ScrollY)
        //    {
        //        if (imagenNoticia.HeightRequest > 0)
        //        {
        //            imagenNoticia.HeightRequest = imagenNoticia.Height - 5;
        //        }
        //        if (imagenNoticia.HeightRequest < 50)
        //        {
        //            imagenNoticia.IsVisible = false;
        //            if (!noticiaCabecera.Children.Contains(lblTitulo))
        //            {
        //                noticiaBody.Children.Remove(lblTitulo);
        //                noticiaCabecera.Children.Add(lblTitulo);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        if (imagenNoticia.HeightRequest < tamañoImagen)
        //        {
        //            imagenNoticia.HeightRequest = imagenNoticia.Height + 10;
        //            imagenNoticia.IsVisible = true;
        //            if (noticiaCabecera.Children.Contains(lblTitulo))
        //            {
        //                noticiaCabecera.Children.Remove(lblTitulo);
        //                noticiaBody.Children.Insert(0, lblTitulo);
        //            }
        //        }
        //    }
        //    scrollAnterior = e.ScrollY;
        //}
    }
}