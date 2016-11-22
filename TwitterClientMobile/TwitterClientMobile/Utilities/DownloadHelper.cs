using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace TwitterClientMobile.Utilities
{
    public static class DownloadHelper
    {
        public static async Task<string> DownloadStringAsync(string strUrl)
        {
            HttpClient httpClient = new HttpClient();

            string strResultData = "";
            try
            {
                strResultData = await httpClient.GetStringAsync(new Uri(strUrl));
                Debug.WriteLine(strResultData);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("StackTrace: ", ex.StackTrace);
                Debug.WriteLine("Exception Message: ", ex.Message);
            }
            finally
            {
                httpClient.Dispose();
                httpClient = null;
            }

            return strResultData;
        }
    }
}
