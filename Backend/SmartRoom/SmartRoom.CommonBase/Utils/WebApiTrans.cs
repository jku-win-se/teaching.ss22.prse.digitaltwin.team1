
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace SmartRoom.CommonBase.Utils
{
    public static class WebApiTrans
    {
        public static async Task<T> GetAPI<T>(string uri, string authtoken = "") where T : new()
        {
            HttpResponseMessage response;

            using (HttpClient client = new HttpClient())
            {
                if (!string.IsNullOrEmpty(authtoken))
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Add("ApiKey", authtoken);
                }
                response = await client.GetAsync(uri);
            }

            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(res);
            }
            return new T();
        }

        public static async Task<HttpResponseMessage> PostAPI(string uri, object contentparam, string authtoken = "")
        {
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            HttpResponseMessage response;

            using (HttpClient client = new HttpClient())
            {
                if (string.IsNullOrEmpty(authtoken))
                {
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Add("ApiKey", authtoken);
                }
                response = await client.PostAsJsonAsync(request.RequestUri, contentparam);
            }

            return response;
        }
    }
}
