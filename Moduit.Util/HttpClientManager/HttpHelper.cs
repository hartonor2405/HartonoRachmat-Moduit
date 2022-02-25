using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;

namespace Moduit.Util.HttpClientManager
{
    public static class HttpHelper
    {
        public static async Task<T> GetRequest<T>(string BaseUrl, string APIPath, Dictionary<string, object>? dict, Dictionary<string, string>? RequestHeaderObj = null)
        {
            using (var client = new HttpClient())
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                
                if (RequestHeaderObj != null && RequestHeaderObj.Any())
                {
                    foreach (var item in RequestHeaderObj)
                    {
                        client.DefaultRequestHeaders.TryAddWithoutValidation(item.Key, item.Value);
                    }
                }

                string urlFormat = APIPath;
                if (dict != null && dict.Any())
                {
                    urlFormat = urlFormat + "?" + QueryString(dict);
                }
                var response = await client.GetAsync(urlFormat);
                response.Headers.Add("X-Content-Type-Options", "nosniff");
                response.Headers.Add("Cache-Control", "no-cache, no-store, must-revalidate");
                response.Headers.Add("Pragma", "no-cache");
                response.Headers.Add("x-frame-options", "SAMEORIGIN");
                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        var content = await response.Content.ReadAsStringAsync(); 
                        return JsonConvert.DeserializeObject<T>(content);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("An Error Occurred From Web Service " + BaseUrl + APIPath + " With Error Message : " + ex.Message);
                    }
                }
                else
                {
                    var content = await response.Content.ReadAsStringAsync();
                    switch (response.StatusCode)
                    {
                        case HttpStatusCode.BadRequest:
                            throw new Exception("No Response From Web Service " + urlFormat);
                        case HttpStatusCode.NotFound:
                            throw new Exception("URL " + BaseUrl + APIPath + " Not Found");
                        default:
                            throw new Exception("An Error Occurred From Web Service " + BaseUrl + APIPath);
                    }
                }
            }
        }
        private static string QueryString(IDictionary<string, object> dict)
        {
            var list = new List<string>();
            foreach (var item in dict)
            {
                list.Add(item.Key + "=" + item.Value);
            }
            return string.Join("&", list);
        }
    }
}
