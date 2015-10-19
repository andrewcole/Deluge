using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Illallangi
{
    public sealed class JsonRpcClient
    {
        public JsonRpcClient(string uri)
        {
            this.Uri = new Uri(uri);
        }

        public JsonRpcClient(Uri uri)
        {
            this.Uri = uri;
        }

        public T InvokeMethod<T>(string method, string session, params object[] parameters)
            where T : class
        {
            var cookieContainer = new CookieContainer();
            cookieContainer.Add(this.Uri, new Cookie("_session_id", session));
            return this.InvokeMethod<T>(method, cookieContainer, parameters);
        }

        public T InvokeMethod<T>(string method, CookieContainer cookieContainer, params object[] parameters) where T: class
        {
            var id = Guid.NewGuid().ToString();

            var result = this.InvokeMethod(method, id, cookieContainer, parameters).ToObject<JsonRpcResponse<T>>();

            if (!id.Equals(result.Id))
            {
                throw new JsonRpcIdMismatchException();
            }

            if (null != result.Error)
            {
                throw new JsonRpcException(this, result.Error);
            }

            return result.Result;
        }

        public JObject InvokeMethod(string method, string id, CookieContainer cookieContainer, params object[] parameters)
        {
            var webRequest = (HttpWebRequest)WebRequest.Create(this.Uri);
            webRequest.ContentType = @"application/json-rpc";
            webRequest.Method = @"POST";
            webRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            webRequest.CookieContainer = cookieContainer;
            webRequest.UserAgent = @"Illallangi.Deluge/ALPHA";
            
            var request = new JObject
            {
                [@"jsonrpc"] = @"1.0",
                [@"id"] = id,
                [@"method"] = method,
            };

            var props = new JArray();
            if (parameters?.Length > 0)
            {
                foreach (var parameter in parameters)
                {
                    props.Add(JToken.FromObject(parameter));
                }
            }

            request.Add(new JProperty(@"params", props));

            var requestString = JsonConvert.SerializeObject(request);
            var byteArray = Encoding.UTF8.GetBytes(requestString);
            webRequest.ContentLength = byteArray.Length;

            using (var dataStream = webRequest.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
            }

            try
            {
                WebResponse webResponse = null;
                using (webResponse = webRequest.GetResponse())
                {
                    using (var str = webResponse.GetResponseStream())
                    {
                        if (str != null)
                        {
                            using (var sr = new StreamReader(str))
                            {
                                var readToEnd = sr.ReadToEnd();
                                return JsonConvert.DeserializeObject<JObject>(readToEnd);
                            }
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            catch (WebException webex)
            {
                using (var str = webex.Response.GetResponseStream())
                {
                    if (str != null)
                    {
                        using (var sr = new StreamReader(str))
                        {
                            return JsonConvert.DeserializeObject<JObject>(sr.ReadToEnd());
                        }
                    }
                    else
                    {
                        throw;
                    }
                }
            }
        }
        
        public Uri Uri { get; }
    }
}
