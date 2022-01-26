using Online_Chat_Mobile.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Online_Chat_Mobile.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class JoinServerPage : ContentPage
    {
        public JoinServerPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Navigation Page and Join server
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnJoin_Clicked(object sender, EventArgs e)
        {
            if (txtPort.Text != "" && txtIP.Text != "")
            {
                try
                {
                    App.client = new ClassClient(txtIP.Text, txtPort.Text);
                    await Navigation.PushAsync(new ChatMessageClient(txtIP.Text, txtPort.Text));
                }
                catch(Exception ex)
                {
                    await DisplayAlert("ERROR", ex.Message, "OK");
                }
            }
            else
                await DisplayAlert("ERROR", "Please complete all information", "OK");
        }
    }
}