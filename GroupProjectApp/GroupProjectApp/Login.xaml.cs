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
using System.Net.Http;
using Newtonsoft.Json;
using GroupProjectApp.Classes;
using GroupProjectApp.Models;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace GroupProjectApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Login : Page
    {
        public Login()
        {
            this.InitializeComponent();
        }

        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (App.InternetConnected() == true)
            {
                // Send entered details to be authenticated. 

                using (var client = new HttpClient())
                {
                    var values = new Dictionary<string, string> { { "grant_type", "password" }, { "username", "s00143284@mail.itsligo.ie" }, { "password", "chickensalad" } };
                    var content = new FormUrlEncodedContent(values);
                    var rawAuthResponse = await client.PostAsync("https://signmeinwebapi.azurewebsites.net/authenticate", content);
                    var responseString = await rawAuthResponse.Content.ReadAsStringAsync();
                    App.validAuthDetails = responseString;

                    try
                    {
                        var ValidResponse = JsonConvert.DeserializeObject<ValidAuth>(responseString);

                        string tokenType = string.Format(ValidResponse.token_type);
                        string accessToken = string.Format(ValidResponse.access_token);

                        var userDetailsRaw = await App.LoadDataFromAPI("https://signmeinwebapi.azurewebsites.net/api/users/UserInfo");

                        App.validUserDetails = userDetailsRaw;

                        var userDetailsProccessed = JsonConvert.DeserializeObject<UserDetails>(userDetailsRaw);

                        // save to app page for use in timetable sorting
                        App.userID = userDetailsProccessed.UserID;
                        App.userEmail = userDetailsProccessed.Email;

                        App.RootFrame.Navigate(typeof(MainPage));
                    }
                    catch
                    {
                        var InvalidResponse = JsonConvert.DeserializeObject<InvalidAuth>(responseString);
                        tbkInvalidDetails.Text = string.Format(InvalidResponse.error_description);
                    }

                }
            }
            else { tbkInvalidDetails.Text = "Not connected to the internet"; }
        }
    }
}