using System.Net;

namespace EddyNewHome.Controllers
{
    public class Commons
    {
        public static string GetIpAddress()
        {
            string strHostName = Dns.GetHostName();
            IPHostEntry ipEntry= Dns.GetHostEntry(strHostName);
            IPAddress[] addrs = ipEntry.AddressList;

            return addrs[7].ToString();
        }
    }
}