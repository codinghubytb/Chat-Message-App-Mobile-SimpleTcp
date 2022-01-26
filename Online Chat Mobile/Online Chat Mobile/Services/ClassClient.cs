using SimpleTcp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Online_Chat_Mobile.Services
{
    public class ClassClient
    {
        #region Variable

        public SimpleTcpClient client;

        #endregion

        public ClassClient(string IpServeur, string PortServer)
        {
            try
            {
                client = new SimpleTcpClient($"{IpServeur}:{PortServer}");
            }
            catch(Exception e)
            {
                App.Current.MainPage.DisplayAlert("ERROR", e.Message, "OK");
            }
        }
    }
}
