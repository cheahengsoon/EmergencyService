using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
using System.Collections.ObjectModel;
using Bing.Maps;
using System.Globalization;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace Emergency_Services
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class Hastane : Emergency_Services.Common.LayoutAwarePage
    {
        ObservableCollection<string> hastaneIDListe;
        ObservableCollection<string> hastaneAdListe;
        ObservableCollection<string> hastaneAdresListe;
        ObservableCollection<string> hastaneLocationLatListe;
        ObservableCollection<string> hastaneLocationLongListe;
        Location hastaneLocation;

        ObservableCollection<string> olayIDListe;
        ObservableCollection<string> olayTurListe;
        ObservableCollection<string> olayKisiSayiListe;
        ObservableCollection<string> olayAdresListe;
        ObservableCollection<string> olayLocationLatListe;
        ObservableCollection<string> olayLocationLongListe;
        
        public Hastane()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            base.GoBack(sender, e);
        }

        private void btn_HastaneGoster_Click(object sender, RoutedEventArgs e)
        {

            label_Donut.Text = hastaneIDListe[combo_Hastaneler.SelectedIndex];
            label_Donut2.Text = hastaneAdresListe[combo_Hastaneler.SelectedIndex];
            label_Donut3.Text = hastaneLocationLatListe[combo_Hastaneler.SelectedIndex];
            label_Donut4.Text = hastaneLocationLongListe[combo_Hastaneler.SelectedIndex];

            double lat = Double.Parse(hastaneLocationLatListe[combo_Hastaneler.SelectedIndex], new CultureInfo("tr-TR"));
            double longt = Double.Parse(hastaneLocationLongListe[combo_Hastaneler.SelectedIndex], new CultureInfo("tr-TR"));

            hastaneLocation = new Location(lat, longt);
            MapLayer.SetPosition(pushPin_Hastane, hastaneLocation);
            hastaneMap.SetView(hastaneLocation, 15);
            


        }

        private async void combo_Hastaneler_Loaded(object sender, RoutedEventArgs e)
        {
            Service1Client dbService = new Service1Client();
            Tuple<ObservableCollection<string>, ObservableCollection<string>, ObservableCollection<string>, ObservableCollection<string>, ObservableCollection<string>> hastaneBilgi = await dbService.HastaneGetirAsync();
            hastaneIDListe = hastaneBilgi.Item1;
            hastaneAdListe = hastaneBilgi.Item2;
            hastaneAdresListe = hastaneBilgi.Item3;
            hastaneLocationLatListe = hastaneBilgi.Item4;
            hastaneLocationLongListe = hastaneBilgi.Item5;

            for (int i = 0; i < hastaneAdListe.Count; i++)
            {
                combo_Hastaneler.Items.Add(hastaneAdListe[i]);
            }

           
       }

        private async void btn_OlayGoruntule_Click(object sender, RoutedEventArgs e)
        {
            listOlayBilgi.Items.Clear();
            double lat = Double.Parse(hastaneLocationLatListe[combo_Hastaneler.SelectedIndex], new CultureInfo("tr-TR"));
            double longt = Double.Parse(hastaneLocationLongListe[combo_Hastaneler.SelectedIndex], new CultureInfo("tr-TR"));
            Service1Client olayService = new Service1Client();
            Tuple<ObservableCollection<string>, ObservableCollection<string>, ObservableCollection<string>, ObservableCollection<string>, ObservableCollection<string>, ObservableCollection<string>> olayBilgi = await olayService.olayGosterAsync(lat, longt);
            olayIDListe = olayBilgi.Item1;
            olayTurListe = olayBilgi.Item2;
            olayKisiSayiListe = olayBilgi.Item3;
            olayAdresListe = olayBilgi.Item4;
            olayLocationLatListe = olayBilgi.Item5;
            olayLocationLongListe = olayBilgi.Item6;

            foreach (var item in olayAdresListe)
            {
                listOlayBilgi.Items.Add(item);
            }
        }

   }
}
