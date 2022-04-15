
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace SmartRoom.CommonBase.Utils
{
    public static class WebApiTrans
    {
        public static async Task<HttpResponseMessage> GetAPI(string uri, string authtoken = null)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            HttpResponseMessage response;

            using (HttpClient client = new HttpClient())
            {
                if (authtoken != null)
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(
                      new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization =
                      new AuthenticationHeaderValue("ApiKey", authtoken);
                }

                response = await client.SendAsync(request);
            }

            return response;
        }

        public static async Task<HttpResponseMessage> PostAPI(string uri, object contentparam, string authtoken = null)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            HttpResponseMessage response;

            var content = JsonConvert.SerializeObject(contentparam);
            StringContent stringContent = new StringContent(content, System.Text.Encoding.UTF8, "application/json");

            using (HttpClient client = new HttpClient())
            {
                if (authtoken != null)
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(
                      new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("ApiKey", authtoken);
                }
                response = await client.PostAsync(request.RequestUri, stringContent);
            }

            return response;
        }
    }
}
