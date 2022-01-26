using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Online_Chat_Mobile.Services
{
    public class ClassIP
    {
        /// <summary>
        /// Find Ip
        /// </summary>
        /// <returns>string ip</returns>
        public String RecoverLocalIp()
        {
            IPHostEntry host;
            string localIP = "?";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily.ToString() == "InterNetwork")
                {
                    localIP = ip.ToString();
                }
            }
            return localIP;
        }
    }
}
