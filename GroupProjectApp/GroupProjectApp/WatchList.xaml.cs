using GroupProjectApp.Classes;
using GroupProjectApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace GroupProjectApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class WatchList : Page
    {
        public WatchList()
        {
            init();
        }

        private async void init()
        {
            this.InitializeComponent();
            var rawData = await GetCurrentlyWatched();

            var watchedRooms = JsonConvert.DeserializeObject<string[]>(rawData);

            List<string> _watchedRoomsList = new List<string>();

            foreach (var item in watchedRooms)
            {
                //WatchedRoom room = new WatchedRoom();
                // room.Code = item;
                // watchedRooms 

                _watchedRoomsList.Add(item);
            }

            lbxWatchedRooms.ItemsSource = _watchedRoomsList;

        }

        private async Task<string> GetCurrentlyWatched()
        {
            var rawAuthInfo = App.validAuthDetails;
            var authDetails = JsonConvert.DeserializeObject<ValidAuth>(rawAuthInfo);
            var rawData = "";

            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(authDetails.token_type, authDetails.access_token);
            HttpResponseMessage ResponseMsg = await httpClient.GetAsync("https://signmeinwebapi.azurewebsites.net/api/WatchRooms");

            if (ResponseMsg.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return rawData = await ResponseMsg.Content.ReadAsStringAsync();

            }
            else if (ResponseMsg.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                rawData = null;
                return rawData;
            }

            return null;

        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            App.RootFrame.Navigate(typeof(MainPage), null);
        }

        private void btnWatch_Click(object sender, RoutedEventArgs e)
        {
            App.RootFrame.Navigate(typeof(WatchList), null);
        }
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            App.RootFrame.Navigate(typeof(Search), null);
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            App.RootFrame.Navigate(typeof(Login), null);
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
        }
    }
}
