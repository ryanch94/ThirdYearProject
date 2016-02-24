using GroupProjectApp.Classes;
using GroupProjectApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
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
    public sealed partial class FreeRooms : Page
    {
        public FreeRooms()
        {
            AddUserID();
            this.InitializeComponent();
        }

        private async void AddUserID()
        {
            var rawAuthInfo = App.validAuthDetails;
            var authDetails = JsonConvert.DeserializeObject<ValidAuth>(rawAuthInfo);

            #region UserIdsHttpClient 
            HttpClient userIdClient = new HttpClient();
            userIdClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(authDetails.token_type, authDetails.access_token);
            HttpResponseMessage freeRoomsResponseMsg = await userIdClient.GetAsync("https://signmeinwebapi.azurewebsites.net/api/FreeRooms");
            var freeRoomsRaw = await freeRoomsResponseMsg.Content.ReadAsStringAsync();
            #endregion

            var cfreeRooms = JsonConvert.DeserializeObject<FreeRoom[]>(freeRoomsRaw);
            List<FreeRoom> _roomsCurrentlyFree = new List<FreeRoom>();

            foreach (var item in cfreeRooms)
            {
                lbxCurrentlyFreeRooms.Items.Add(cfreeRooms);
            }
    }


    #region Navigation
    private void btnHome_Click(object sender, RoutedEventArgs e)
    {
        App.RootFrame.Navigate(typeof(MainPage), null);
    }
    private void btnFreeRooms_Click(object sender, RoutedEventArgs e)
    {
        App.RootFrame.Navigate(typeof(FreeRooms), null);
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
        #endregion
    }
}