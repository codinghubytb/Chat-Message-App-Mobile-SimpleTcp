using Online_Chat_Mobile.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Online_Chat_Mobile.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateServerPage : ContentPage
    {
        public CreateServerPage()
        {
            InitializeComponent();
            txtIp.Text = App.classIP.RecoverLocalIp();
        }

        /// <summary>
        /// Navigation Page Chat Server and Create Server
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnCreate_Clicked(object sender, EventArgs e)
        {
            if (txtPort.Text != "")
                try
                {
                    App.server = new ClassServer(txtPort.Text);
                    App.server.CreateServer();
                    await Navigation.PushAsync(new ChatMessageServer(txtPort.Text));
                }
                catch(Exception ex)
                {
                    await DisplayAlert("ERROR",ex.Message, "OK");
                }
            else
                await DisplayAlert("ERROR", "Please complete all information", "OK");
        }
    }
}