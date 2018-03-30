using System.IO;
using System.Net;

namespace HubSpotApi.Controllers
{
    class RequestController
    {
        public static string GetResponse(string url)
        {
            string json = string.Empty;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                json = reader.ReadToEnd();
            }
            return json;
        }
    }
}
