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

namespace GroupProjectApp.Models
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ClientDetails : Page
    {
        public ClientDetails()
        {
            this.InitializeComponent();

            FillInDetails();
        }

        private void FillInDetails()
        {
            var DataArray = JsonConvert.DeserializeObject<UserDetails>(App.validUserDetails);

            tbkName.Text = string.Format("{0}" + " " + "{1}", DataArray.FirstName, DataArray.LastName);
            tbkCollegeID.Text = Convert.ToString(DataArray.StudentID);
            tbkEmail.Text = Convert.ToString(DataArray.Email);
            if (DataArray.Notifications == true) { tbkNotifications.Text = "On"; } else { tbkNotifications.Text = "Off"; }

            string roles = "";
            foreach (var role in DataArray.Roles)
            {
                roles += role + ", ";
            }
            tbkRoles.Text = roles.Remove(roles.Length - 2);
        }

        #region Sidebar navigation

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

    }
}
