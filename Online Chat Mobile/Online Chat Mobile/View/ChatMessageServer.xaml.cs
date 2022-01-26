using Online_Chat_Mobile.Services;
using SimpleTcp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Online_Chat_Mobile.Models;

namespace Online_Chat_Mobile.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChatMessageServer : ContentPage
    {

        #region Variable

        List<string> ListClientIp;
        #endregion

        public ChatMessageServer(string PORTServer)
        {
            InitializeComponent();
            ListClientIp = new List<string>();
            ActionUser(PORTServer);
            Console.WriteLine($"scroll y : {ScrollViewElement.ScaleY} /// height : {ScrollViewElement.Height}");
        }

        #region Function / Method

        /// <summary>
        /// Manage Back Button Pressed
        /// </summary>
        /// <returns></returns>
        protected override bool OnBackButtonPressed()
        {
            try
            {
                App.server.server.Stop();
            }
            catch { }
            return base.OnBackButtonPressed();
        }

        /// <summary>
        /// Initialize Server
        /// </summary>
        /// <param name="port">Port</param>
        private void ActionUser(string port)
        {
            try
            {
                App.server.server.Events.DataReceived += Events_DataReceived;
                App.server.server.Events.ClientDisconnected += Events_ClientDisconnected;
                App.server.server.Events.ClientConnected += Events_ClientConnected;
                App.server.server.Start();
                SctacklayoutMessage.Children.Add(new Label
                {
                    Text = $"Server created {DateTime.Now}",
                    TextColor = Color.FromHex("#0e2038"),
                    HorizontalTextAlignment = TextAlignment.Center,
                    FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label))
                });
                lblTitle.Text = $"The server {App.classIP.RecoverLocalIp()}:{port}";
            }
            catch(Exception e)
            {
                lblTitle.Text = $"Server not create";
                DisplayAlert("ERROR", e.Message, "OK");
            }
        }

        /// <summary>
        /// Client Connected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Events_ClientConnected(object sender, ConnectionEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                ListClientIp.Add(e.IpPort);
                SctacklayoutMessage.Children.Add(new Label
                {
                    Text = $"{e.IpPort} Connected !!!",
                    TextColor = Color.Red,
                    HorizontalTextAlignment = TextAlignment.Center,
                    FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label))
                });

                ScrollViewElement.ScrollToAsync(SctacklayoutMessage.X, SctacklayoutMessage.Y + SctacklayoutMessage.Height, true);
            });
        }

        /// <summary>
        /// Client Disconnect
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Events_ClientDisconnected(object sender, ConnectionEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                SctacklayoutMessage.Children.Add(new Label
                {
                    Text = $"{e.IpPort} Deconnected !!!",
                    TextColor = Color.Red,
                    HorizontalTextAlignment = TextAlignment.Center,
                    FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label))
                });

                ScrollViewElement.ScrollToAsync(SctacklayoutMessage.X, SctacklayoutMessage.Y + SctacklayoutMessage.Height, true);
            });
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
        /// Add Message
        /// </summary>
        /// <param name="message">string message</param>
        /// <param name="Name">string ip</param>
        /// <param name="layoutOptions">Position right or left</param>
        /// <param name="colorBack">Color back</param>
        /// <param name="colorFront">Color Front</param>
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
        /// Button send message to all client
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSend_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSendMessage.Text))
            {
                foreach(var ip in ListClientIp)
                {
                    try
                    {
                        App.server.server.Send(ip, txtSendMessage.Text);
                    }
                    catch(Exception ex)
                    {
                        DisplayAlert("ERROR",ex.Message,"OK");
                    }
                }
                ChatMessageStackLayout(txtSendMessage.Text, "ME", LayoutOptions.EndAndExpand, Color.LightGray, Color.FromHex("#0e2038") );
                txtSendMessage.Text = "";
                ScrollViewElement.ScrollToAsync(SctacklayoutMessage.X, SctacklayoutMessage.Y + SctacklayoutMessage.Height, true);
            }
            
        }

       
        /// <summary>
        /// Button Stop Server
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnDisconnect_Clicked(object sender, EventArgs e)
        {
            try
            {
                App.server.server.Stop();
                await Navigation.PopAsync();
            }
            catch
            {
                await Navigation.PopAsync();
            }
        }

        /// <summary>
        /// Display people connected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPeopleConnected(object sender, EventArgs e)
        {
            string peopleCo = "";
            foreach (var people in ListClientIp)
                peopleCo += $"¤ {people}\n";
            DisplayAlert("People Connected", peopleCo, "OK");
        }
        #endregion
    }
}