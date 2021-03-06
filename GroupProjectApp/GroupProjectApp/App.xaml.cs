﻿
using GroupProjectApp.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Newtonsoft.Json;
using System.Net.Http;
using GroupProjectApp.Models;
using Windows.Networking.Connectivity;

namespace GroupProjectApp
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        public static Frame RootFrame;
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        /// 

        public static string validAuthDetails;
        public static string validUserDetails;
        public static string userID;
        public static string userEmail;



        public static async Task<string> LoadDataFromAPI(string apiAddress)
        {
            //method to check if user is authenticated and has valid details
            // if invalid details, response message will return unauthorised

            var rawAuthInfo = App.validAuthDetails;
            var authDetails = JsonConvert.DeserializeObject<ValidAuth>(rawAuthInfo);
            var rawData = "";

            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(authDetails.token_type, authDetails.access_token);
            HttpResponseMessage ResponseMsg = await httpClient.GetAsync(apiAddress);

            // check to see if request was successful or not
            if (ResponseMsg.StatusCode == System.Net.HttpStatusCode.OK)
            {
                rawData = await ResponseMsg.Content.ReadAsStringAsync();

            }
            else if (ResponseMsg.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                rawData = "AuthInvalid";
            }
            return rawData;
        }

        public async static Task<List<Room>> GetWatchedRoomsList()
        {
            List<Room> _watchedRoomsList = new List<Room>();

            var rawData = await LoadDataFromAPI("https://signmeinwebapi.azurewebsites.net/api/WatchRooms");

            var watchedRooms = JsonConvert.DeserializeObject<Room[]>(rawData);

            foreach (var item in watchedRooms)
            {
                _watchedRoomsList.Add(item);
            }
            return _watchedRoomsList;
        }

        public static async Task<string> AddOrRemoveFromWatchList(int roomId)
        {

            var rawAuthInfo = App.validAuthDetails;
            var authDetails = JsonConvert.DeserializeObject<ValidAuth>(rawAuthInfo);

            try
            {
                HttpClient Client = new HttpClient();

                var values = new Dictionary<string, string> { { "RoomID", Convert.ToString(roomId) } };
                var content = new FormUrlEncodedContent(values);
                Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(authDetails.token_type, authDetails.access_token);
                var response = await Client.PostAsync("https://signmeinwebapi.azurewebsites.net/api/watchrooms", content);
                var responseString = await response.Content.ReadAsStringAsync();
                return responseString;
            }
            catch
            {
                return "Failed to add room";
            }
        }

        // check for internet connection on device
        public static bool InternetConnected()
        {
            var connectionProfile = NetworkInformation.GetInternetConnectionProfile();

            return (connectionProfile != null && connectionProfile.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess);
        }

        /*********************** App generated code below**************************/
        public App()
        {
            Microsoft.ApplicationInsights.WindowsAppInitializer.InitializeAsync(
                Microsoft.ApplicationInsights.WindowsCollectors.Metadata |
                Microsoft.ApplicationInsights.WindowsCollectors.Session);
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {

#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                this.DebugSettings.EnableFrameRateCounter = true;
            }
#endif

            RootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (RootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                RootFrame = new Frame();

                RootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = RootFrame;
            }

            if (RootFrame.Content == null)
            {
                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter
                RootFrame.Navigate(typeof(Login), e.Arguments);
            }
            // Ensure the current window is active
            Window.Current.Activate();
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }
    }
}
