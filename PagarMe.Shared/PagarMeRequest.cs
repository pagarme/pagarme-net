//
// PagarMeRequest.cs
//
// Author:
//       Jonathan Lima <jonathan@pagar.me>
//
// Copyright (c) 2014 Pagar.me
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Reflection;
using System.Net;
using System.Configuration;
using System.Linq;

#if HAS_ASYNC
using System.Threading.Tasks;
#endif

namespace PagarMe
{
    public class PagarMeRequest
    {
        internal static string BuildQueryString(IEnumerable<Tuple<string, string>> query)
        {
            return query.Select((t) => Uri.EscapeUriString(t.Item1) + "=" + Uri.EscapeUriString(t.Item2)).Aggregate((c, n) => c + "&" + n);
        }

        private PagarMeService _service;
        public string Method { get; private set; }
        public string Path { get; private set; }

        public bool UseEncryptionKey { get; set; }
        public List<Tuple<string, string>> Query { get; internal set; }

        public string Body { get; set; }

        internal PagarMeRequest(string method, string path)
            : this(null, method, path) {}

        internal PagarMeRequest(PagarMeService service, string method, string path)
        {
            if (service == null)
                service = PagarMeService.GetDefaultService();

            _service = service;

            Method = method;
            Path = path;

            Query = new List<Tuple<string, string>>();
            Body = "{}";
        }

        #if !PCL
        public PagarMeResponse Execute()
        {
			return TLS.Instance.UseTLS12IfAvailable(() =>
			{
				HttpWebResponse response;
				var request = GetRequest();
				bool isError = false;

				if (Body != null && (Method == "POST" || Method == "PUT"))
				{
					var encoding = new UTF8Encoding(false);

					request.ContentLength = encoding.GetByteCount(Body);

					using (var stream = request.GetRequestStream())
					using (var writer = new StreamWriter(stream, encoding))
						writer.Write(Body);
				}

				try
				{
					response = (HttpWebResponse)request.GetResponse();
				}
				catch (WebException e)
				{
					isError = true;
					response = (HttpWebResponse)e.Response;

					if (response == null)
						throw e;
				}

				var body = "";

				using (var stream = response.GetResponseStream())
				using (var reader = new StreamReader(stream, Encoding.UTF8))
					body = reader.ReadToEnd();

				if (isError)
					throw new PagarMeException(new PagarMeError(response.StatusCode, body));
				else
					return new PagarMeResponse(response.StatusCode, body);
			});
        }
        #else
        public PagarMeResponse Execute()
        {
            var task = ExecuteAsync();

            task.Wait();

            if (task.IsFaulted)
                throw task.Exception;
            else
                return task.Result;
        }
        #endif

        public async Task<PagarMeResponse> ExecuteAsync()
        {
	        return await TLS.Instance.UseTLS12IfAvailable(async () =>
	        {
		        HttpWebResponse response;
		        var request = GetRequest();
		        bool isError = false;

		        if (Body != null)
		        {
			        var encoding = new UTF8Encoding(false);

					#if !PCL
			        request.ContentLength = encoding.GetByteCount(Body);
					#else
					request.Headers["Content-Length"] = encoding.GetByteCount(Body).ToString();
					#endif

			        using (var stream = await Task<Stream>.Factory.FromAsync(request.BeginGetRequestStream, request.EndGetRequestStream, null))
			        using (var writer = new StreamWriter(stream, encoding))
				        writer.Write(Body);
		        }

		        try
		        {
			        response = (HttpWebResponse)(await Task<WebResponse>.Factory.FromAsync(request.BeginGetResponse, request.EndGetResponse, null));
		        }
		        catch (WebException e)
		        {
			        isError = true;
			        response = (HttpWebResponse)e.Response;

			        if (response == null)
				        throw e;
		        }

		        var body = "";

		        using (var stream = response.GetResponseStream())
		        using (var reader = new StreamReader(stream, Encoding.UTF8))
			        body = await reader.ReadToEndAsync();

		        if (isError)
			        throw new PagarMeException(new PagarMeError(response.StatusCode, body));
		        else
			        return new PagarMeResponse(response.StatusCode, body);
	        });
        }

        private HttpWebRequest GetRequest()
        {
            HttpWebRequest request = WebRequest.CreateHttp(GetRequestUri());

            #if !PCL
            request.UserAgent = "pagarme-net/" + typeof(PagarMeRequest).Assembly.GetName().Version.ToString();
            #else
            request.Headers["User-Agent"] = "pagarme-net/" + typeof(PagarMeRequest).GetTypeInfo().Assembly.GetName().Version.ToString();
            #endif

            request.ContentType = "application/json";
            request.Method = Method;
            return request;
        }

        private Uri GetRequestUri()
        {
            UriBuilder uriBuilder = new UriBuilder(new Uri(_service.ApiEndpoint + Path));
            Tuple<string, string> authKey;

            if (UseEncryptionKey)
                authKey = new Tuple<string, string>("encryption_key", _service.EncryptionKey);
            else
                authKey = new Tuple<string, string>("api_key", _service.ApiKey);

            uriBuilder.Query = BuildQueryString(Query.Concat(new[] { authKey }));

            return uriBuilder.Uri;
        }

        private string GetKey(string key){
            switch(key){
                case "api_key":
                    return GetApiKey();
                    break;
                case "encryption_key":
                    return GetEncryptionKey();
                    break;
                default:
                    return null;
            }
        }

        private string GetApiKey(){
            return _service.ApiKey == null ? (string)new AppSettingsReader().GetValue("api_key", typeof(string)) : _service.ApiKey;
        }
    }
}

