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
    public sealed partial class Login : Page
    {
        public int studentnum = 0;

        public Login()
        {
            this.InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            // Oauth code to be implemented
            //  database will return student db id 

            studentnum = 8;
            // pass student number got back from the Oauth and db calls to the API on the main page          

            App.RootFrame.Navigate(typeof(MainPage), studentnum);
        }
    }
}
