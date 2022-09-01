namespace Core.Integration.Base
{
    public interface IBaseRestClient
    {
        Task Post(string url,
         object data = null,
         IDictionary<string, string> headers = null,
         Ref<HttpResponseMessage> outResponse = null);

        Task<T> Post<T>(string url,
         object data = null,
         IDictionary<string, string> headers = null,
         Ref<HttpResponseMessage> outResponse = null);

        Task Patch(string url,
         object data = null,
         IDictionary<string, string> headers = null,
         Ref<HttpResponseMessage> outResponse = null);

        Task Delete(string url,
         object data = null,
         IDictionary<string, string> headers = null,
         Ref<HttpResponseMessage> outResponse = null);

        Task<T> Delete<T>(string url,
         object data = null,
         IDictionary<string, string> headers = null,
         Ref<HttpResponseMessage> outResponse = null);

        Task<T> Put<T>(string url,
         object data = null,
         IDictionary<string, string> headers = null,
         Ref<HttpResponseMessage> outResponse = null);

        Task Put(string url,
         object data = null,
         IDictionary<string, string> headers = null,
         Ref<HttpResponseMessage> outResponse = null);

        Task<T> Get<T>(string url,
         IDictionary<string, string> headers = null,
         Ref<HttpResponseMessage> outResponse = null);

        Task Get(string url,
         IDictionary<string, string> headers = null,
         Ref<HttpResponseMessage> response = null);

        Task<Stream> GetStream(string url);
    }
}