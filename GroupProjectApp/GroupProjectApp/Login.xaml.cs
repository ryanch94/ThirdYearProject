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

                   
                        // Send authentication details and return user details
                        #region UserDetailsHttpClient 
                        HttpClient userDetailsClient = new HttpClient();
                        userDetailsClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(ValidResponse.token_type, ValidResponse.access_token);
                        HttpResponseMessage userDetailsResponseMsg = await userDetailsClient.GetAsync("https://signmeinwebapi.azurewebsites.net/api/users/UserInfo");
                        var userDetailsRaw = await userDetailsResponseMsg.Content.ReadAsStringAsync();
                        #endregion

                        App.validUserDetails = userDetailsRaw;

                        var userDetailArray = JsonConvert.DeserializeObject<UserDetails>(userDetailsRaw);

                        // pass student number got back from the Oauth and db calls to the API on the main page 
                        App.userID = userDetailArray.UserID;

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
