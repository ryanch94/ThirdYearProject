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
    /// This page filters through all rooms available on a chosen day and time
    /// </summary>
    public sealed partial class SearchByDayAndBlock : Page
    {
        public static List<Room> _lstWatchedRooms = new List<Room>();

        public SearchByDayAndBlock()
        {
            if (App.InternetConnected() == true)
            {
                this.InitializeComponent();

                GetWatchedRooms();
                PopulateDayCbx();
                PopulateBlockTimesCbx();
                SetComboSelections();
            }
            else
            {
                tbkErrorMessage.Text = "No internet connection";
                btnRefresh.Visibility = Visibility.Visible;
            }
        }

        private void SetComboSelections()
        {
            // both trigger onclick event so validation done before sending data to api
           // cbxDayTimes.SelectedIndex = 0;
           // cbxWeekdays.SelectedIndex = 0;
        }

        #region Search by day and block methods (combobox selection change, listbox selection, watch roomtype button)

        private void cbxWeekdays_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           // FilterDayBockTimes();
        }

        private void cbxDayTimes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
          //  FilterDayBockTimes();
        }

        private async void FilterDayBockTimes()
        {
            // avoid sending null values to api by checking both combobox selections are not null
            {
                List<FreeRoom> _dayAndBlockLst = new List<FreeRoom>();
                WeekDay day = (WeekDay)cbxWeekdays.SelectedItem;
                DayBlockTime dayBlock = (DayBlockTime)cbxDayTimes.SelectedItem;

                if (day.DayName != null && dayBlock.DayTime != null)
                {
                    var rawData = await App.LoadDataFromAPI(string.Format("https://signmeinwebapi.azurewebsites.net/api/FreeRooms?weekDayNumber=" + "{0}" + "&dayBlock=" + "{1}", day.DayNumber, dayBlock.BlockNum));

                    if (rawData != "AuthInvalid")
                    {
                        var DataArray = JsonConvert.DeserializeObject<FreeRoom[]>(rawData);

                        foreach (var item in DataArray)
                        {
                            _dayAndBlockLst.Add(item);
                        }

                        lbxDayTimes.ItemsSource = _dayAndBlockLst;
                    }

                    else if (rawData == "AuthInvalid")
                    {
                        DisableButtons();
                    }
                }

            }
        }

        private void DisableButtons()
        {
            tbkErrorMessage.Text = "Not Authorised. Logout and re - enter details.";
            btnHome.IsEnabled = false;
            btnWatch.IsEnabled = false;
            btnSearch.IsEnabled = false;
            btnDayAndBlock.IsEnabled = false;
            btnMyDetails.IsEnabled = false;
        }

        private void lbxDayTimes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GetWatchedRooms();
            FreeRoom selected = (FreeRoom)lbxDayTimes.SelectedItem;

            foreach (var room in _lstWatchedRooms)
            {
                if (room.Id == selected.Id)
                {
                    btnWdayAndTime.IsEnabled = false;
                }
                else { btnWdayAndTime.IsEnabled = true; }
            }
        }

        private void btnWdayAndTime_Click(object sender, RoutedEventArgs e)
        {
            FreeRoom room = (FreeRoom)lbxDayTimes.SelectedItem;
            var rawData = App.AddOrRemoveFromWatchList(room.Id);
            btnWdayAndTime.IsEnabled = false;
            //update watched rooms
            GetWatchedRooms();
        }

        public async void GetWatchedRooms()
        {
            List<Room> _lstWatchedRooms = await App.GetWatchedRoomsList();
        }
        #endregion

        private void PopulateDayCbx()
        {
            List<WeekDay> _weekDays = new List<WeekDay>() {
                new WeekDay { DayName = "Monday", DayNumber = 2 },
                new WeekDay { DayName = "Tuesday", DayNumber = 3 },
                new WeekDay { DayName = "Wednesday", DayNumber = 4 },
                new WeekDay { DayName = "Thursday", DayNumber = 5 },
                new WeekDay { DayName = "Friday", DayNumber = 6 }};

            cbxWeekdays.ItemsSource = _weekDays;
        }

        public void PopulateBlockTimesCbx()
        {
            List<DayBlockTime> _dayBlockTimes = new List<DayBlockTime>(){
            new DayBlockTime { DayTime = "9am - 10am", BlockNum = 1},
            new DayBlockTime { DayTime = "10am - 11am", BlockNum = 2},
            new DayBlockTime { DayTime = "11am - 12am", BlockNum = 3},
            new DayBlockTime { DayTime = "12am - 1pm", BlockNum = 4},
            new DayBlockTime { DayTime = "1pm - 2pm", BlockNum = 5},
            new DayBlockTime { DayTime = "2pm - 3pm", BlockNum = 6},
            new DayBlockTime { DayTime = "3pm - 4pm", BlockNum = 7},
            new DayBlockTime { DayTime = "4pm - 5pm", BlockNum = 1},
            new DayBlockTime { DayTime = "5pm - 6pm", BlockNum = 1}};

            cbxDayTimes.ItemsSource = _dayBlockTimes;
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            App.RootFrame.Navigate(typeof(SearchByDayAndBlock), null);
        }


        #region Sidebar Navigation buttons
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

        private void btnDayAndBlock_Click(object sender, RoutedEventArgs e)
        {
            App.RootFrame.Navigate(typeof(SearchByDayAndBlock), null);
        }

        #endregion

        private void btnMyDetails_Click(object sender, RoutedEventArgs e)
        {
            App.RootFrame.Navigate(typeof(ClientDetails), null);
        }
    }
}
