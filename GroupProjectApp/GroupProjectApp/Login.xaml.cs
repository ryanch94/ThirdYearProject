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
        public int studentnum = 0;

        public Login()
        {
            this.InitializeComponent();
        }

        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            // Send entered details to be authenticated. 

            using (var client = new HttpClient())
            {
                var values = new Dictionary<string, string> { { "grant_type", "password" }, { "username", tbxEmail.Text }, { "password", tbxPassword.Text } };
                var content = new FormUrlEncodedContent(values);
                var rawAuthResponse = await client.PostAsync("https://signmeinwebapi.azurewebsites.net/authenticate", content);
                var responseString = await rawAuthResponse.Content.ReadAsStringAsync();
                App.validAuthDetails = responseString;

                try
                {
                    var ValidResponse = JsonConvert.DeserializeObject<ValidAuth>(responseString);

                    // Send authentication details and return user details
                    #region UserDetailsHttpClient 
                    HttpClient userDetailsClient = new HttpClient();
                    userDetailsClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(ValidResponse.token_type, ValidResponse.access_token);
                    HttpResponseMessage userDetailsResponseMsg = await client.GetAsync("https://signmeinwebapi.azurewebsites.net/api/users/userinfo");
                    var userDetailsRaw = await userDetailsResponseMsg.Content.ReadAsStringAsync();
                    #endregion

                    App.validUserDetails = userDetailsRaw;

                    var userDetailArray = JsonConvert.DeserializeObject<UserDetails>(userDetailsRaw);                 

                    // pass student number got back from the Oauth and db calls to the API on the main page 
                    studentnum = 8;
                    App.RootFrame.Navigate(typeof(MainPage), studentnum);
                }
                catch
                {
                    var InvalidResponse = JsonConvert.DeserializeObject<InvalidAuth>(responseString);
                    tbkInvalidDetails.Text = string.Format(InvalidResponse.error_description);
                }
            }
        }
    }
}
