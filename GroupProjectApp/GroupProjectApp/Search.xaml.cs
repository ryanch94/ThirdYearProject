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
    /// Search Page - Holds all the methods  for using the search functions
    /// Note: Most APIs are sent to a method on the App page for processing - Cuts down on duplicate code
    /// </summary>

    public sealed partial class Search : Page
    {
        #region API Urls

        // Api addresses to use for each search function
        // These ones don't require parameters sent with them except be authenticated
        static string currentFreeRoomsAPI = "https://signmeinwebapi.azurewebsites.net/api/FreeRooms";
        static string roomTypeAPI = "https://signmeinwebapi.azurewebsites.net/api/RoomTypes";
        static string programTypeAPI = "https://signmeinwebapi.azurewebsites.net/api/Programs";
        static string allRoomsAPI = "https://signmeinwebapi.azurewebsites.net/api/Rooms";

        #endregion

        #region Lists declared

        // lists to populate comboboxs 
        List<FreeRoom> _lstRoomsCurrentlyFree = new List<FreeRoom>();
        List<RoomType> _lstRoomTypes = new List<RoomType>();
        List<ProgramType> _lstProgramTypes = new List<ProgramType>();
        List<Room> _lstAllRooms = new List<Room>();

        // list to compare off of
        List<Room> _lstWatchedRooms = new List<Room>();

        #endregion

        public Search()
        {
            this.InitializeComponent();

            #region method calls to populate combobox data

            //Get and proccess data to populate comboboxs 
            //GetAllRooms();
            GetWatchedRooms();
            PopulateCurrentFreeLst();
            PopulateRoomTypeCbx();
            PopulateProgramTypeCbx();
            //PopulateDayCbx();
            //PopulateBlockTimesCbx();

            #endregion
        }

        #region Methods to populate comboboxes and lists

        public async void GetAllRooms()
        {
            // get all rooms in database


            var rawData = await App.LoadDataFromAPI(allRoomsAPI);

            if (rawData != "AuthInvalid")
            {
                var DataArray = JsonConvert.DeserializeObject<Room[]>(rawData);

                foreach (var item in DataArray)
                {
                    _lstAllRooms.Add(item);
                }
            }
            else if (rawData == "AuthInvalid") { DisableButtons(); }
        }

        public async void GetWatchedRooms()
        {
            var s = await App.GetWatchedRoomsList();

            foreach (var item in s)
            {
                _lstWatchedRooms.Add(item);
            }
        }

        private async void PopulateCurrentFreeLst()
        {
            var rawData = await App.LoadDataFromAPI(currentFreeRoomsAPI);

            if (rawData != "AuthInvalid")
            {
                var DataArray = JsonConvert.DeserializeObject<FreeRoom[]>(rawData);

                foreach (var item in DataArray)
                {

                    _lstRoomsCurrentlyFree.Add(item);
                }

                lbxCurrentlyFreeRooms.ItemsSource = _lstRoomsCurrentlyFree;
            }
            else if (rawData == "AuthInvalid")
            {
                DisableButtons();
            }
        }

        private async void PopulateRoomTypeCbx()
        {
            var rawData = await App.LoadDataFromAPI(roomTypeAPI);

            if (rawData != "AuthInvalid")
            {
                var DataArray = JsonConvert.DeserializeObject<RoomType[]>(rawData);

                foreach (var item in DataArray)
                {
                    _lstRoomTypes.Add(new RoomType { Id = item.Id, Type = item.Type });
                }
                cbxRoomType.ItemsSource = _lstRoomTypes;

                if (_lstRoomTypes.Count() != 0)
                {
                    cbxRoomType.SelectedIndex = 0;
                }
            }
            else if (rawData == "AuthInvalid")
            {
                DisableButtons();
            }
        }

        private async void PopulateProgramTypeCbx()
        {
            var rawData = await App.LoadDataFromAPI(programTypeAPI);
            if (rawData != "AuthInvalid")
            {
                if (rawData != null)
                {
                    var DataArray = JsonConvert.DeserializeObject<ProgramType[]>(rawData);

                    foreach (var item in DataArray)
                    {
                        _lstProgramTypes.Add(item);
                    }

                    cbxProgramTypes.ItemsSource = _lstProgramTypes;

                    if (_lstProgramTypes.Count() != 0)
                    {
                        cbxProgramTypes.SelectedIndex = 0;
                    }
                }
            }
            else if (rawData == "AuthInvalid")
            {
                DisableButtons();
            }
        }

        #endregion

        #region currently free methods (listbox selection, watch roomtype button)

        private void lbxCurrentlyFreeRooms_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GetWatchedRooms();
            FreeRoom selected = (FreeRoom)lbxCurrentlyFreeRooms.SelectedItem;

            foreach (var room in _lstWatchedRooms)
            {
                if (room.Id == selected.Id)
                {
                    btnWatchCF.IsEnabled = false;
                }
                else { btnWatchCF.IsEnabled = true; }
                GetWatchedRooms();
            }
        }

        private void btnWatchCF_Click(object sender, RoutedEventArgs e)
        {
            GetWatchedRooms();
            FreeRoom room = (FreeRoom)lbxCurrentlyFreeRooms.SelectedItem;
            var rawData = App.AddOrRemoveFromWatchList(room.Id);
            btnWatchCF.IsEnabled = false;
            GetWatchedRooms();
        }

        #endregion

        #region Search by Room type methods (combobox selection change, listbox selection, watch roomtype button)

        private void cbxRoomType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GetWatchedRooms();
            List<FreeRoom> _RoomTypeAvail = new List<FreeRoom>();

            RoomType selected = (RoomType)cbxRoomType.SelectedValue;

            foreach (var freeRoom in _lstRoomsCurrentlyFree)
            {

                if (freeRoom.Type == selected.Type)
                {
                    _RoomTypeAvail.Add(freeRoom);
                }
            }

            lbxRoomType.ItemsSource = _RoomTypeAvail;
        }

        private void lbxRoomType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GetWatchedRooms();
            FreeRoom selected = (FreeRoom)lbxRoomType.SelectedItem;

            foreach (var room in _lstRoomTypes)
            {
                if (room.Id == selected.Id)
                {
                    btnWatchRT.IsEnabled = false;
                }
                else { btnWatchRT.IsEnabled = true; }
                GetWatchedRooms();
            }
        }

        private void btnWatchRT_Click(object sender, RoutedEventArgs e)
        {
            GetWatchedRooms();
            FreeRoom room = (FreeRoom)lbxRoomType.SelectedItem;
            var rawData = App.AddOrRemoveFromWatchList(room.Id);
            btnWatchRT.IsEnabled = false;
            GetWatchedRooms();
        }

        #endregion

        #region Search by Program type methods (combobox selection change, listbox selection, watch roomtype button)

        private void cbxProgramTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GetWatchedRooms();
            // List created to populate the program type listbox
            List<FreeRoom> _ProgramRoomAvail = new List<FreeRoom>();

            ProgramType program = (ProgramType)cbxProgramTypes.SelectedItem;

            // If room description is not empty(null)
            // check each room description to check if it has the required program installed 
            // add to availablity list if it does
            foreach (var freeRoom in _lstRoomsCurrentlyFree)
            {
                if (freeRoom.Description != null && freeRoom.Description.Contains(program.Description) == true)
                {
                    _ProgramRoomAvail.Add(freeRoom);
                }
            }
            lbxPrograms.ItemsSource = _ProgramRoomAvail;
        }

        private void lbxPrograms_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GetWatchedRooms();
            FreeRoom selected = (FreeRoom)lbxPrograms.SelectedItem;
            foreach (var room in _lstWatchedRooms)
            {
                if (room.Id == selected.Id)
                {
                    btnWatchPT.IsEnabled = false;
                }
                else { btnWatchPT.IsEnabled = true; }
            }
        }

        private void btnWatchPT_Click(object sender, RoutedEventArgs e)
        {
            GetWatchedRooms();
            FreeRoom room = (FreeRoom)lbxPrograms.SelectedItem;
            var rawData = App.AddOrRemoveFromWatchList(room.Id);
            btnWatchPT.IsEnabled = false;
            GetWatchedRooms();
        }

        #endregion

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

        private void DisableButtons()
        {
            btnHome.IsEnabled = false;
            btnWatch.IsEnabled = false;
            btnSearch.IsEnabled = false;
            btnMyDetails.IsEnabled = false;

        }

        private void btnMyDetails_Click(object sender, RoutedEventArgs e)
        {
            App.RootFrame.Navigate(typeof(ClientDetails), null);
        }
    }
}

