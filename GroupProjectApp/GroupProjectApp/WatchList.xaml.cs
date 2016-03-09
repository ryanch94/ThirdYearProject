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
        public static List<WatchedRoom> _watchedRoomsList = new List<WatchedRoom>();
        public WatchList()
        {
            this.InitializeComponent();
            init();

        }

        public async void init()
        {
            var rawData = await App.LoadDataFromAPI("https://signmeinwebapi.azurewebsites.net/api/WatchRooms");

            var watchedRooms = JsonConvert.DeserializeObject<string[]>(rawData);

            _watchedRoomsList.Clear();

            foreach (var item in watchedRooms)
            {
                WatchedRoom room = new WatchedRoom();
                room.Code = item;

                _watchedRoomsList.Add(room);
            }

            lbxWatchedRooms.ItemsSource = _watchedRoomsList;

        }

        private void lbxWatchedRooms_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            WatchedRoom selected = (WatchedRoom)lbxWatchedRooms.SelectedItem;

            btnUnwatch.IsEnabled = true;
        }

        private void btnUnwatch_Click(object sender, RoutedEventArgs e)
        {
            WatchedRoom room = (WatchedRoom)lbxWatchedRooms.SelectedItem;
            App.AddOrRemoveFromWatchList(room.Code);
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

