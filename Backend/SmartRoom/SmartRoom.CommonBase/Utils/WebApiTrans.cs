
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
            else throw new HttpRequestException(response.StatusCode.ToString());
        }

        public static async Task<HttpResponseMessage> PostAPI<T>(string uri, T contentparam, string authtoken = "")
        {
            HttpResponseMessage response;

            using (HttpClient client = new HttpClient())
            {
                if (!string.IsNullOrEmpty(authtoken))
                {
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Add("ApiKey", authtoken);
                }
                response = await client.PostAsJsonAsync(uri, contentparam);
            }
            if(!response.IsSuccessStatusCode) throw new HttpRequestException(response.StatusCode.ToString());
            else return response;
        }
    }
}
