using Core.Integration.Dto;
using Core.Integration.Serialization;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace Core.Integration.Base
{
    public class BaseRestClient : IBaseRestClient
    {
        private readonly ApiSession ApiSession;
        private readonly IJsonSerializer _jsonSerializer;

        public BaseRestClient(ApiSession apiSession, IJsonSerializer jsonSerializer)
        {
            ApiSession = apiSession;
            _jsonSerializer = jsonSerializer;
        }

        public Task Post(string url,
            object data = null,
            IDictionary<string, string> headers = null,
            Ref<HttpResponseMessage> outResponse = null)
        {
            return MakeRequest(HttpMethod.Post, url, data, headers, outResponse);
        }

        public async Task<T> Post<T>(string url,
            object data = null,
            IDictionary<string, string> headers = null,
            Ref<HttpResponseMessage> outResponse = null)
        {
            var result = await MakeRequest(HttpMethod.Post, url, data, headers, outResponse).ConfigureAwait(false);
            return await DeserializeOrDefault<T>(result).ConfigureAwait(false);
        }

        public Task Patch(string url,
            object data = null,
            IDictionary<string, string> headers = null,
            Ref<HttpResponseMessage> outResponse = null)
        {
            return MakeRequest(new HttpMethod("PATCH"), url, data, headers, outResponse);
        }

        public Task Delete(string url,
            object data = null,
            IDictionary<string, string> headers = null,
            Ref<HttpResponseMessage> outResponse = null)
        {
            return MakeRequest(HttpMethod.Delete, url, data, headers, outResponse);
        }

        public async Task<T> Delete<T>(string url,
            object data = null,
            IDictionary<string, string> headers = null,
            Ref<HttpResponseMessage> outResponse = null)
        {
            var result = await MakeRequest(HttpMethod.Delete, url, data, headers, outResponse).ConfigureAwait(false);
            return await DeserializeOrDefault<T>(result).ConfigureAwait(false);
        }

        public async Task<T> Put<T>(string url,
            object data = null,
            IDictionary<string, string> headers = null,
            Ref<HttpResponseMessage> outResponse = null)
        {
            var result = await MakeRequest(HttpMethod.Put, url, data, headers, outResponse).ConfigureAwait(false);
            return await DeserializeOrDefault<T>(result).ConfigureAwait(false);
        }

        public Task Put(string url,
            object data = null,
            IDictionary<string, string> headers = null,
            Ref<HttpResponseMessage> outResponse = null)
        {
            return MakeRequest(HttpMethod.Put, url, data, headers, outResponse);
        }

        public async Task<T> Get<T>(string url,
            IDictionary<string, string> headers = null,
            Ref<HttpResponseMessage> outResponse = null)
        {
            var result = await MakeRequest(HttpMethod.Get, url, null, headers, outResponse).ConfigureAwait(false);
            return await DeserializeOrDefault<T>(result).ConfigureAwait(false);
        }

        public Task Get(string url,
            IDictionary<string, string> headers = null,
            Ref<HttpResponseMessage> response = null)
        {
            return MakeRequest(HttpMethod.Get, url, null, headers, response);
        }

        public async Task<Stream> GetStream(string url)
        {
            using (var client = GetClient())
            {
                var result = await client.SendAsync(GetHttpMessage(HttpMethod.Get, url)).
                    ConfigureAwait(false);

                await ThrowIfError(result).ConfigureAwait(false);

                return await result.Content.ReadAsStreamAsync().ConfigureAwait(false);
            }
        }

        private async Task<HttpResponseMessage> MakeRequest(
            HttpMethod method,
            string url,
            object data = null,
            IDictionary<string, string> headers = null,
            Ref<HttpResponseMessage> outResponse = null)
        {
            var message = GetHttpMessage(method, url, headers);
            HttpResponseMessage result;

            using (message.Content = GetMessageContent(data))
            {
                using (var client = GetClient())
                {
                    result = await client.SendAsync(message);
                    await ThrowIfError(result);
                }
            }

            if (outResponse != null)
            {
                outResponse.Value = result;
            }
            return result;
        }

        private static HttpRequestMessage GetHttpMessage(HttpMethod method,
            string resource,
            IDictionary<string, string> headers = null)
        {
            var message = new HttpRequestMessage(method, resource);
            message.Headers.Accept.Clear();
            message.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (headers != null)
            {
                foreach (var kvp in headers)
                {
                    message.Headers.Add(kvp.Key, kvp.Value);
                }
            }
            return message;
        }

        private HttpClient GetClient()
        {
            var handler = new HttpClientHandler();
            if (handler.SupportsAutomaticDecompression)
            {
                handler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            }

            if (ApiSession.Proxy != null)
            {
                handler.Proxy = ApiSession.Proxy;
                handler.UseProxy = true;
            }

            handler.UseCookies = true;
            handler.Credentials = new NetworkCredential(ApiSession.Credentials.Username, ApiSession.Credentials.Password);

            var client = new HttpClient(handler, true) { Timeout = TimeSpan.FromSeconds(600) };
            client.DefaultRequestHeaders.Add("User-Agent", ApiSession.UserAgent);
            client.DefaultRequestHeaders.ExpectContinue = false;
            return client;
        }

        private HttpContent GetMessageContent(object data)
        {
            if (data == null) return null;

            return new StringContent(Serialize(data), Encoding.UTF8, "application/json");
        }

        private async Task<T> DeserializeOrDefault<T>(HttpResponseMessage result)
        {
            var content = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
            return !string.IsNullOrEmpty(content) ? _jsonSerializer.Deserialize<T>(content) : default(T);
        }

        private string Serialize(object data)
        {
            return _jsonSerializer.Serialize(data);
        }

        private static async Task ThrowIfError(HttpResponseMessage result)
        {
            if (!result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
                var errorMessage = new ErrorMessage();

                try
                {
                    errorMessage = JsonConvert.DeserializeObject<ErrorMessage>(content);
                    var error = errorMessage.ErrorMessages[0];
                }
                catch (Exception)
                {
                    errorMessage.ErrorMessages = new[] { content };
                }

                throw new ApiException(
                    $"Error when calling {result.RequestMessage.Method.ToString().ToUpperInvariant()} " +
                    $"{result.RequestMessage.RequestUri}.",
                    result.StatusCode,
                    errorMessage,
                    null);
            }
        }
    }
}