using SimpleTcp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Online_Chat_Mobile.Services
{
    public class ClassServer
    {
        #region Variable
        public string IP;
        private string _PORT;
        public SimpleTcpServer server;

        #endregion

        public ClassServer(string PORT)
        {
            IP = App.classIP.RecoverLocalIp();
            _PORT = PORT;
        }

        /// <summary>
        /// Create Server
        /// </summary>
        public void CreateServer()
        {
            try
            {
                server = new SimpleTcpServer($"{IP}:{_PORT}");
            }
            catch (Exception e)
            {
                App.Current.MainPage.DisplayAlert("ERROR", e.Message, "OK");
            }
        }
    }
}
