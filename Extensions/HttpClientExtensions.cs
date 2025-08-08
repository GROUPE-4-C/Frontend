using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;

namespace AlumniConnect.Front.Extensions
{
    public static class HttpClientExtensions
    {
        public static Task<HttpResponseMessage> PatchAsJsonAsync<T>(this HttpClient httpClient, string requestUri, T value)
        {
            var content = JsonContent.Create(value);
            var request = new HttpRequestMessage(new HttpMethod("PATCH"), requestUri)
            {
                Content = content
            };

            return httpClient.SendAsync(request);
        }
    }
}
