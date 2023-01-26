using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace HelpDesk.Server.Utils
{
	public class Network
	{
		 public static async Task<string> GetPublicIPAsync()
         {
            try
            {
                HttpClient client = new HttpClient();
                var ipTask = client.GetStringAsync("https://api.ipify.org");
                var ipAddress = await ipTask;

                if(ipAddress == null)
                {
                    using var httpClient = new HttpClient();
                    var request = new HttpRequestMessage(HttpMethod.Get, "http://checkip.dyndns.org");
                    var response = httpClient.Send(request);
                    StreamReader reader = new(response.Content.ReadAsStream());
                    string responseString = reader.ReadToEnd().Trim();
                    string[] a = responseString.Split(':');
                    string a2 = a[1].Substring(1);
                    string[] a3 = a2.Split('<');
                    string resultString = a3[0];
                    return resultString;
                }
                else
                {
                    return ipAddress;
                }
                
            }
            catch (Exception)
            {
                return null;
            }
            
         }
	}
}

