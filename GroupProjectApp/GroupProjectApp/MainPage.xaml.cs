using GroupProjectApp.Classes;
using GroupProjectApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
    public sealed partial class MainPage : Page
    {
        // lists to hold each days classes
        List<DailyClass> MondayClasses = new List<DailyClass>();
        List<DailyClass> TuesdayClasses = new List<DailyClass>();
        List<DailyClass> WednesdayClasses = new List<DailyClass>();
        List<DailyClass> ThursdayClasses = new List<DailyClass>();
        List<DailyClass> FridayClasses = new List<DailyClass>();

        public static List<Room> _AllRooms = new List<Room>();
        public static List<WatchedRoom> _WatchedRooms = new List<WatchedRoom>();

        private string rawJSON;

        public MainPage()
        {
            this.InitializeComponent();

            // Call to data methods to populate day lists
            // Day numbering set to match sql server format
            data(MondayClasses, 2);
            data(TuesdayClasses, 3);
            data(WednesdayClasses, 4);
            data(ThursdayClasses, 5);
            data(FridayClasses, 6);

            GetAllRooms();
            GetWatchedRooms();

        }

        private void GetWatchedRooms()
        {
            _WatchedRooms = WatchList._watchedRoomsList;
        }

        public async void GetAllRooms()
        {
            // get all rooms in database
            string AllRoomsAPI = string.Format("https://signmeinwebapi.azurewebsites.net/api/Rooms");

            var rawData = await App.LoadDataFromAPI(AllRoomsAPI);

            var DataArray = JsonConvert.DeserializeObject<Room[]>(rawData);

            foreach (var item in DataArray)
            {
                _AllRooms.Add(item);
            }
        }

        private async void data(List<DailyClass> dayClasses, int dayNumber)
        {
            string timetableAPI = string.Format("https://signmeinwebapi.azurewebsites.net/api/timetables/" + "{0}", App.userID);

            rawJSON = await App.LoadDataFromAPI(timetableAPI);

            dayClasses = App.ConvertJsonToArray(rawJSON, dayNumber);

            PopulateList(dayClasses, dayNumber);
        }

        private void PopulateList(List<DailyClass> daysClasses, int dayNumber)
        {
            // Sort which day list to populate depending on day number
            #region switch statement

            switch (dayNumber)
            {
                case 2:
                    lstMon.ItemsSource = daysClasses;
                    break;

                case 3:
                    lstTues.ItemsSource = daysClasses;
                    break;

                case 4:
                    lstWed.ItemsSource = daysClasses;
                    break;

                case 5:
                    lstThurs.ItemsSource = daysClasses;
                    break;

                case 6:
                    lstFri.ItemsSource = daysClasses;
                    break;
            }
            #endregion
        }


        #region Navigation
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
        #endregion
    }
}
