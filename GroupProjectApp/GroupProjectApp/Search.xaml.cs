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
            //Get and proccess data to populate currently free rooms listbox - send freeRooms for sort
            PopulateFreeRoomsList();
            PopulateProgramsCombo();
            PropulateRoomType();
            #region 
            //PopulateRoomTypeCombo();
            //PopulateProgramTypeCombo();

            ////AddUserID();
            ////GetFreeCurrentRooms();
            ////GetPrograms();
            #endregion

            this.InitializeComponent();
        }

        private async void PropulateRoomType()
        {
            var rawData = await GetRawHttpData(RoomTypeApi);
            var DataArray = JsonConvert.DeserializeObject<RoomType[]>(rawData);

            foreach (var item in DataArray)
            {
                _roomTypesList.Add(new RoomType { Id = item.Id, Type = item.Type });
            }
            cbxRoomType.ItemsSource = _roomTypesList;
        }

        private async void PopulateProgramsCombo()
        {
            var rawData = await GetRawHttpData(ProgramTypeApi);

            if (rawData != null)
            {
                var DataArray = JsonConvert.DeserializeObject<ProgramType[]>(rawData);

                foreach (var item in DataArray)
                {

                    _programList.Add(item);
                }

                cbxPrograms.ItemsSource = _programList;
            }
        }
        private async void PopulateFreeRoomsList()
        {

            var rawData = await GetRawHttpData(FreeRoomsApi);
            var DataArray = JsonConvert.DeserializeObject<FreeRoom[]>(rawData);

            foreach (var item in DataArray)
            {

                _roomsCurrentlyFree.Add(item);
            }

            lbxCurrentlyFreeRooms.ItemsSource = _roomsCurrentlyFree;
        }

        public async Task<string> GetRawHttpData(string apiAddress)
        {
            var rawAuthInfo = App.validAuthDetails;
            var authDetails = JsonConvert.DeserializeObject<ValidAuth>(rawAuthInfo);
            var rawData = "";

            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(authDetails.token_type, authDetails.access_token);
            HttpResponseMessage ResponseMsg = await httpClient.GetAsync(apiAddress);

            if (ResponseMsg.StatusCode == System.Net.HttpStatusCode.OK)
            {
                rawData = await ResponseMsg.Content.ReadAsStringAsync();

            }
            else if (ResponseMsg.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                rawData = null;
            }

            return rawData;
        }

        //private async void AddUserID()
        //{


        //    #region UserIdsHttpClient 
        //    HttpClient userIdClient = new HttpClient();
        //    userIdClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(authDetails.token_type, authDetails.access_token);
        //    HttpResponseMessage freeRoomsResponseMsg = await userIdClient.GetAsync("https://signmeinwebapi.azurewebsites.net/api/FreeRooms");



        //    var freeRoomsRaw = await freeRoomsResponseMsg.Content.ReadAsStringAsync();
        //    #endregion

        //    var cfreeRooms = JsonConvert.DeserializeObject<FreeRoom[]>(freeRoomsRaw);
        //    List<FreeRoom> _roomsCurrentlyFree = new List<FreeRoom>();

        //    foreach (var item in cfreeRooms)
        //    {
        //        _roomsCurrentlyFree.Add(item);

        //    }

        //    lbxCurrentlyFreeRooms.ItemsSource = _roomsCurrentlyFree;
        //}

        //private async void GetPrograms()
        //{
        //    try
        //    {
        //        // get valid authentication details from app page and deserialise them
        //        var rawAuthInfo = App.validAuthDetails;


        //        // 




        //        var authDetails = JsonConvert.DeserializeObject<ValidAuth>(rawAuthInfo);

        //        #region ProgramsHttpClient 
        //        HttpClient userIdClient = new HttpClient();
        //        userIdClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(authDetails.token_type, authDetails.access_token);

        //        HttpResponseMessage freeRoomsResponseMsg = await userIdClient.GetAsync("https://signmeinwebapi.azurewebsites.net/api/FreeRooms");
        //        var freeRoomsRaw = await freeRoomsResponseMsg.Content.ReadAsStringAsync();

        //        HttpResponseMessage programsResponseMessage = await userIdClient.GetAsync("https://signmeinwebapi.azurewebsites.net/api/FreeRooms");
        //        var programsRaw = await programsResponseMessage.Content.ReadAsStringAsync();

        //        HttpResponseMessage RoomTypeResponseMsg = await userIdClient.GetAsync("https://signmeinwebapi.azurewebsites.net/api/RoomTypes");
        //        var RoomTypeRaw = await RoomTypeResponseMsg.Content.ReadAsStringAsync();
        //        #endregion


        //        var freeRoomsList = JsonConvert.DeserializeObject<FreeRoom[]>(freeRoomsRaw);
        //        var programsList = JsonConvert.DeserializeObject<ProgramType[]>(programsRaw);
        //        var roomTypeList = JsonConvert.DeserializeObject<RoomType[]>(RoomTypeRaw);

        //        foreach (var item in freeRoomsList)
        //        {
        //            _roomsCurrentlyFree.Add(item);
        //        }

        //        List<DropDownItem> _programsList = new List<DropDownItem>();

        //        foreach (var item in programsList)
        //        {
        //            _programsList.Add(new DropDownItem { Text = item.f, Value = Convert.ToString(item.ID) });
        //        }





        //        #region method required here or somewhere to filter through rooms



        //        #endregion



        //        foreach (var item in _roomsCurrentlyFree)
        //        {
        //            if (cbxRoomType.SelectedValue.ToString() == item.Type) { lbxRoomType.Items.Add(item); }
        //        }




        //        List<RoomType> _roomtypes = new List<RoomType>();



        //        foreach (var item in roomTypeList)
        //        {

        //            _roomtypes.Add(item);
        //        }

        //        cbxRoomType.ItemsSource = _roomtypes;
        //    }


        //    catch
        //    {

        //    }
        //}

        //private async void GetFreeCurrentRooms()
        //{
        //    try
        //    {
        //        // get valid authentication details from app page and deserialise them
        //        var rawAuthInfo = App.validAuthDetails;
        //        var authDetails = JsonConvert.DeserializeObject<ValidAuth>(rawAuthInfo);

        //        #region ProgramsHttpClient 
        //        HttpClient userIdClient = new HttpClient();
        //        userIdClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(authDetails.token_type, authDetails.access_token);
        //        HttpResponseMessage freeRoomsResponseMsg = await userIdClient.GetAsync("https://signmeinwebapi.azurewebsites.net/api/FreeRooms");
        //        var freeRoomsRaw = await freeRoomsResponseMsg.Content.ReadAsStringAsync();
        //        #endregion

        //        var cfreeRooms = JsonConvert.DeserializeObject<FreeRoom[]>(freeRoomsRaw);

        //        foreach (var item in cfreeRooms)
        //        {
        //            _roomsCurrentlyFree.Add(item);
        //        }
        //    }
        //    catch
        //    {

        //    }

        //}

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
    }
}

