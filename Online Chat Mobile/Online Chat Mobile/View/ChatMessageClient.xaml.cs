using SimpleTcp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Online_Chat_Mobile.Models;
using Online_Chat_Mobile.Services;

namespace Online_Chat_Mobile.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChatMessageClient : ContentPage
    {
        public ChatMessageClient(string IPServer, string PORTServer)
        {
            InitializeComponent();
            ActionUser(IPServer, PORTServer);
        }

        #region Function / Methode


        /// <summary>
        /// Manage back button
        /// </summary>
        /// <returns></returns>
        protected override bool OnBackButtonPressed()
        {
            try
            {
                App.client.client.Disconnect();
            }
            catch { }
            return base.OnBackButtonPressed();
        }

        /// <summary>
        /// Create Client and Connect to server
        /// </summary>
        /// <param name="IpServer">Ip server</param>
        /// <param name="PortServer">Port server</param>
        private void ActionUser(string IpServer, string PortServer)
        {
            try
            {
                App.client.client.Events.Connected += Events_Connected;
                App.client.client.Events.Disconnected += Events_Disconnected;
                App.client.client.Events.DataReceived += Events_DataReceived;
                App.client.client.Connect();
                lblTitle.Text = $"The server {IpServer}:{PortServer}";
            }
            catch(Exception e)
            {
                DisplayAlert("ERROR", e.Message, "OK");
            }
        }

        /// <summary>
        /// Data received
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Events_DataReceived(object sender, DataReceivedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                ChatMessageStackLayout($"{Encoding.UTF8.GetString(e.Data)}", e.IpPort, LayoutOptions.StartAndExpand, Color.FromHex("#0e2038"), Color.White);
                ScrollViewElement.ScrollToAsync(SctacklayoutMessage.X, SctacklayoutMessage.Y + SctacklayoutMessage.Height, true);
            });
        }

        /// <summary>
        /// Client Disconnect
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Events_Disconnected(object sender, ConnectionEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                SctacklayoutMessage.Children.Add(new Label
                {
                    Text = $"You are offline",
                    TextColor = Color.FromHex("#0e2038"),
                    HorizontalTextAlignment = TextAlignment.Center,
                    FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label))
                });
                ScrollViewElement.ScrollToAsync(SctacklayoutMessage.X, SctacklayoutMessage.Y + SctacklayoutMessage.Height, true);
            });
        }

        /// <summary>
        /// Client Connected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Events_Connected(object sender, ConnectionEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                SctacklayoutMessage.Children.Add(new Label
                {
                    Text = $"You have joined the server {DateTime.Now}",
                    TextColor = Color.FromHex("#0e2038"),
                    HorizontalTextAlignment = TextAlignment.Center,
                    FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label))
                });
                ScrollViewElement.ScrollToAsync(SctacklayoutMessage.X, SctacklayoutMessage.Y + SctacklayoutMessage.Height, true);
            });
        }

        // <summary>
        /// Create StackLayout Message Display
        /// </summary>
        /// <param name="message">string message</param>
        /// <param name="Name">string ip </param>
        /// <param name="layoutOptions">Position Right or Left</param>
        /// <param name="colorBack">Color back</param>
        /// <param name="colorFront">Color Text</param>
        private void ChatMessageStackLayout(string message, string Name, LayoutOptions layoutOptions, Color colorBack, Color colorFront)
        {
            ClassChat chat = new ClassChat();
            chat.Date = $"{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}";
            chat.Name = Name;
            chat.Message = message;
            SctacklayoutMessage.Children.Add(chat.StackLayoutChat(layoutOptions, colorBack, colorFront));
        }

        #endregion

        #region Click

        /// <summary>
        /// Send Message to server
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSend_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSendMessage.Text))
            {
                App.client.client.Send(txtSendMessage.Text);
                ChatMessageStackLayout(txtSendMessage.Text, "ME", LayoutOptions.EndAndExpand, Color.LightGray, Color.FromHex("#0e2038"));
                txtSendMessage.Text = "";
                ScrollViewElement.ScrollToAsync(SctacklayoutMessage.X, SctacklayoutMessage.Y + SctacklayoutMessage.Height, true);
            }
        }

        /// <summary>
        /// Button disconnect client
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnDisconnect_Clicked(object sender, EventArgs e)
        {
            try
            {
                App.client.client.Disconnect();
                await Navigation.PopAsync();
            }
            catch
            {
                await Navigation.PopAsync();
            }
        }
        #endregion
    }
}