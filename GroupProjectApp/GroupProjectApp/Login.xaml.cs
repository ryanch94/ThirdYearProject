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
            // Oauth code to be implemented
            using (var client = new HttpClient())
            {
                var values = new Dictionary<string, string> { { "grant_type", "password" }, { "username", tbxEmail.Text }, { "password", tbxPassword.Text } };

                var content = new FormUrlEncodedContent(values);

                var response = await client.PostAsync("http://signmeinwebapi.azurewebsites.net/authenticate", content);

                var responseString = await response.Content.ReadAsStringAsync();

                // check for typeof class the data has - valid class or invalid                     
                if (responseString.GetType() == typeof(ValidAuth))
                {
                    var ValidResponse = JsonConvert.DeserializeObject<ValidAuth[]>(responseString);

                    // code for user id api call required here when available

                    // sample number needs replacing
                    studentnum = 8;
                    App.RootFrame.Navigate(typeof(MainPage), studentnum);

                }
                else
                {
                    var InvalidResponse = JsonConvert.DeserializeObject<InvalidAuth>(responseString);
                    tbkInvalidDetails.Text = string.Format(InvalidResponse.error_description);
                }

            }

            //  database will return student db id 

            // studentnum = 8;
            // pass student number got back from the Oauth and db calls to the API on the main page          


        }
    }
}
