using Online_Chat_Mobile.View;
using SimpleTcp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Online_Chat_Mobile
{
    public partial class MainPage : ContentPage
    {
        #region Variable

        NetworkAccess connect = Connectivity.NetworkAccess;
        #endregion

        public MainPage()
        {
            InitializeComponent(); 
        }

        /// <summary>
        /// Navigation Page Join Server
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnJoin_Clicked(object sender, EventArgs e)
        {
            if (connect == NetworkAccess.Internet)
                await Navigation.PushAsync(new JoinServerPage());
            else
                await DisplayAlert("ERROR", "No internet connection found", "OK");
        }

        /// <summary>
        /// Navigation Page Create Server
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnCreate_Clicked(object sender, EventArgs e)
        {
            if (connect == NetworkAccess.Internet)
                await Navigation.PushAsync(new CreateServerPage());
            else
                await DisplayAlert("ERROR", "No internet connection found", "OK");
        }
    }
}
