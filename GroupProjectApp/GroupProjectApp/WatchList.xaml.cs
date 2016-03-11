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
        public static List<Room> _watchedRoomsList = new List<Room>();
        public WatchList()
        {
            this.InitializeComponent();

            if (App.InternetConnected() == true)
            {
                GetWatchedRooms();
            }
            else
            {

                btnRefresh.Visibility = Visibility.Visible;
            }

        }

        public async void GetWatchedRooms()
        {
            _watchedRoomsList = await App.GetWatchedRoomsList();
            lbxWatchedRooms.ItemsSource = _watchedRoomsList;
        }

        private void lbxWatchedRooms_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Room selected = (Room)lbxWatchedRooms.SelectedItem;

            btnUnwatch.IsEnabled = true;
        }

        private async void btnUnwatch_Click(object sender, RoutedEventArgs e)
        {
            Room room = (Room)lbxWatchedRooms.SelectedItem;
            var response = await App.AddOrRemoveFromWatchList(room.Id);
            GetWatchedRooms();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            App.RootFrame.Navigate(typeof(WatchList), null);
        }

        #region nav

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

        //private void btnDayAndBlock_Click(object sender, RoutedEventArgs e)
        //{
        //    App.RootFrame.Navigate(typeof(SearchByDayAndBlock), null);
        //}

        #endregion

        private void btnMyDetails_Click(object sender, RoutedEventArgs e)
        {
            App.RootFrame.Navigate(typeof(ClientDetails), null);
        }
    }
}

