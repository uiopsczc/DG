namespace DG
{
    public class IPAddressUtil
    {
        public static string GetLocalIP()
        {
            return NetUtil.GetLocalIP();
        }
    }
}