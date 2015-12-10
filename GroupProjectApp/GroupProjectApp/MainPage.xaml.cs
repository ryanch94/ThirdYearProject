using GroupProjectApp.Classes;
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
    public sealed partial class MainPage : Page
    {
        List<DailyClass> MonClasses = new List<DailyClass>();
        List<DailyClass> TueClasses = new List<DailyClass>();
        List<DailyClass> WedClasses = new List<DailyClass>();
        List<DailyClass> ThuClasses = new List<DailyClass>();
        List<DailyClass> FriClasses = new List<DailyClass>();

        private string rawJSON;

        public MainPage()
        {
            this.InitializeComponent();

            #region 
            data(MonClasses, 2);
            data(TueClasses, 3);
            data(WedClasses, 4);
            data(ThuClasses, 5);
            data(FriClasses, 6);
        }

        private async void data(List<DailyClass> dayClasses, int dayNum)
        {
            rawJSON = await App.LoadDataFromAPI();

            dayClasses = App.ConvertJsonToRoom(rawJSON);

            PopulateList(dayClasses, dayNum);
        }

        private void PopulateList(List<DailyClass> daysClasses, int dayNum)
        {
            switch (dayNum)
            {
                case 2:
                    lstMon.ItemsSource = daysClasses;
                    break;

                case 3:
                    lstTues.ItemsSource = daysClasses;
                    break;

                case 4:
                    lstWed.ItemsSource = daysClasses;
                    break;

                case 5:
                    lstThurs.ItemsSource = daysClasses;
                    break;

                case 6:
                    lstFri.ItemsSource = daysClasses;
                    break;

                default:
                    break;
            }
        }

        #region Navigation
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
