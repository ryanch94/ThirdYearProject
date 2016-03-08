using GroupProjectApp.Classes;
using GroupProjectApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
    public sealed partial class Search : Page
    {
        // Api addresses to use 
        static string FreeRoomsApi = "https://signmeinwebapi.azurewebsites.net/api/FreeRooms";
        static string RoomTypeApi = "https://signmeinwebapi.azurewebsites.net/api/RoomTypes";
        static string ProgramTypeApi = "https://signmeinwebapi.azurewebsites.net/api/Programs";


        // list for searching 
        List<FreeRoom> _roomsCurrentlyFree = new List<FreeRoom>();
        List<RoomType> _roomTypesList = new List<RoomType>();
        List<ProgramType> _programList = new List<ProgramType>();


        public Search()
        {
            this.InitializeComponent();
            //Get and proccess data to populate currently free rooms listbox - send freeRooms for sort
            PopulateFreeRoomsList();
            PopulateProgramsCombo();
            PropulateRoomType();
        }

        private async void PropulateRoomType()
        {
            var rawData = await App.LoadDataFromAPI(RoomTypeApi);
            var DataArray = JsonConvert.DeserializeObject<RoomType[]>(rawData);

            foreach (var item in DataArray)
            {
                _roomTypesList.Add(new RoomType { Id = item.Id, Type = item.Type });
            }
            cbxRoomType.ItemsSource = _roomTypesList;
        }

        private async void PopulateProgramsCombo()
        {
            var rawData = await App.LoadDataFromAPI(ProgramTypeApi);

            if (rawData != null)
            {
                var DataArray = JsonConvert.DeserializeObject<ProgramType[]>(rawData);

                foreach (var item in DataArray)
                {

                    _programList.Add(item);
                }

                cbxPrograms.ItemsSource = _programList;
            }
            else { }
        }
        private async void PopulateFreeRoomsList()
        {

            var rawData = await App.LoadDataFromAPI(FreeRoomsApi);
            var DataArray = JsonConvert.DeserializeObject<FreeRoom[]>(rawData);

            foreach (var item in DataArray)
            {

                _roomsCurrentlyFree.Add(item);
            }

            lbxCurrentlyFreeRooms.ItemsSource = _roomsCurrentlyFree;
        }

        private void cbxRoomType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            List<FreeRoom> _RoomTypeAvail = new List<FreeRoom>();

            RoomType selected = (RoomType)cbxRoomType.SelectedValue;

            foreach (var freeRoom in _roomsCurrentlyFree)
            {

                if (freeRoom.Type == selected.Type)
                {
                    _RoomTypeAvail.Add(freeRoom);
                }
            }

            lbxRoomType.ItemsSource = _RoomTypeAvail;
        }

        // BUTTON CLICK ACTIONS
        private void btnWatchCF_Click(object sender, RoutedEventArgs e)
        {
            FreeRoom room = (FreeRoom)lbxCurrentlyFreeRooms.SelectedItem;
            App.AddOrRemoveFromWatchList(room.Code);
        }

        private void btnWatchRT_Click(object sender, RoutedEventArgs e)
        {
            FreeRoom room = (FreeRoom)lbxRoomType.SelectedItem;
            App.AddOrRemoveFromWatchList(room.Code);
        }

        private void btnWatchPT_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnWDayAndTime_Click(object sender, RoutedEventArgs e)
        {

        }
        private void lbxCurrentlyFreeRooms_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FreeRoom selected = (FreeRoom)lbxCurrentlyFreeRooms.SelectedItem;


            foreach (var room in MainPage._WatchedRooms)
            {
                if (room.Code == selected.Code)
                {
                    btnWatchCF.IsEnabled = false;
                }
                else { btnWatchCF.IsEnabled = true; }
            }
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




        #endregion


    }
}

