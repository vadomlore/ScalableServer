namespace ScalableServer.HttpRequest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using DotNetty.Common.Concurrency;
    using System.Net.Http;
    using System.Net.Http.Headers;

    public class HttpRequestExecuteTaskScheduler
    {
        IEventExecutor executor;
        public HttpRequestExecuteTaskScheduler(IEventExecutor executor)
        {
            this.executor = executor;
        }

        public Task<HttpResponseMessage> Execute(string requestUrl)
        {
            Task<HttpResponseMessage> result = null;
            executor.Execute(() => {
                var client = new HttpClient();
                result = client.GetAsync(requestUrl);
            });
            return result;
        }

        public Task<HttpResponseMessage> Execute(string requestUrl, byte[] postData, string mediaType)
        {
            Task<HttpResponseMessage> result = null;
            executor.Execute(() => {
                var client = new HttpClient();
                var byteContent = new ByteArrayContent(postData);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue(mediaType);
                result = client.PostAsync(requestUrl, byteContent);
            });
            return result;
        }

        public Task<HttpResponseMessage> Execute(string requestUrl, byte[] postData)
        {
            return Execute(requestUrl, postData, "application/json");
        }
    }
}
