using Bing.Maps;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Emergency_Services.WebServiceDB;
using Windows.Media.Capture;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Storage;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Emergency_Services
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        
        Location location;
        BitmapImage bitmap;
        public MainPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }



        private async void btn_OlayYeri_Click(object sender, RoutedEventArgs e)
        {
            Geolocator geolocator = new Geolocator();
            var pos = await geolocator.GetGeopositionAsync();
            location = new Location(pos.Coordinate.Latitude, pos.Coordinate.Longitude);
            MapLayer.SetPosition(pushPin_Olay, location);            
            myMap.SetView(location, 12);
        }


        private async void btn_OlayBildirim_Click(object sender, RoutedEventArgs e)
        {
            

            label_Olay.Text = combo_OlayTürü.SelectionBoxItem.ToString();
            label_kisi.Text = combo_KisiSayi.SelectionBoxItem.ToString();
            label_adres.Text = txt_Adres.Text;
            label_loc.Text = location.Latitude.ToString() + " " + location.Longitude.ToString();
            
            string olayTürü = combo_OlayTürü.SelectionBoxItem.ToString();
            string olayKisiSayi = combo_KisiSayi.SelectionBoxItem.ToString();
            string adres = txt_Adres.Text;
            double lat = location.Latitude;
            double lon = location.Longitude;

            Service1Client myService = new Service1Client();
            string a = await myService.olayEkleAsync(olayTürü, olayKisiSayi, adres, lat, lon);
            label_OlayDonut.Text = a;

            
            
            
        }

        async private void btnCamera_Click(object sender, RoutedEventArgs e)
        {
           var ui = new CameraCaptureUI();
           ui.PhotoSettings.CroppedAspectRatio = new Size(16,9); // 4,3 ya da benzer boyutları ekleyebilirisiniz
           var file = await ui.CaptureFileAsync(CameraCaptureUIMode.Photo); // Photo ,Video ya da PhotoVideo
           if (file != null)
           {
               bitmap = new BitmapImage();
               bitmap.SetSource(await file.OpenAsync(FileAccessMode.Read));
               imgVideo.Source = bitmap;
           }
        }

        private void btnHastane_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Hastane), null);
        }

        private void btn_FotoKayit_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
