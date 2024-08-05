using System.Net;
using UnityEngine;

namespace DG
{
    public class NetUtil
    {
        private const string InterNetwork_String = "InterNetwork";

        public static string GetLocalIP()
        {
            //获取本地的IP地址
            string ipAddressString = StringConst.STRING_EMPTY;
            var list = Dns.GetHostEntry(Dns.GetHostName()).AddressList;
            for (var i = 0; i < list.Length; i++)
            {
                IPAddress ipAddress = list[i];
                if (InterNetwork_String.Equals(ipAddress.AddressFamily.ToString()))
                    ipAddressString = ipAddress.ToString();
            }

            return ipAddressString;
        }

        public static bool IsWifi()
        {
            return Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork;
        }
    }
}