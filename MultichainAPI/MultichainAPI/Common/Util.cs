using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using MultichainAPI.Exceptions;
using MultichainAPI.Models;
using Newtonsoft.Json;

namespace MultichainAPI.Common
{
    public class Util
    {
        public static HttpClient GetHttpClientWithBasicAuth(string url, string username, string password)
        {
            var authValue = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}")));

            var baseAddress = new Uri(url);
            var client = new HttpClient()
            {
                BaseAddress = baseAddress,
                DefaultRequestHeaders = { Authorization = authValue, ConnectionClose = false }
            };
            ServicePointManager.FindServicePoint(baseAddress).ConnectionLeaseTimeout = 120 * 1000;

            return client;
        }

        public static async Task<JsonRPCResponse<T>> GetRPCResponse<T>(string method, object[] parameters)
        {
            var jsonRPCRequest = new JsonRPCRequest()
            {
                Method = method,
                Params = parameters is null ? new object[0] : parameters,
                Id = 1
            };

            var jsonRPCRequestString = JsonConvert.SerializeObject(jsonRPCRequest);

            try
            {
                string jsonRet = null;
                JsonRPCResponse<T> jsonRPCResponse = null;

                using (var client = Util.GetHttpClientWithBasicAuth("http://168.62.59.77:4364", "multichainrpc", "Fo2tQwo1ms6rC5RY3M1BHuuzE9a8yJsaZjHCaPA68BDD"))
                {
                    var request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Post,
                        RequestUri = new Uri("http://168.62.59.77:4364"),
                        Content = new StringContent(jsonRPCRequestString, Encoding.UTF8, "application/json-rpc")
                    };

                    var result = await client.SendAsync(request);
                    jsonRet = await result.Content.ReadAsStringAsync();
                }

                try
                {
                    jsonRPCResponse = JsonConvert.DeserializeObject<JsonRPCResponse<T>>(jsonRet);
                }
                catch (Exception jsonEx)
                {
                    Console.WriteLine(jsonEx.Message);
                    var errorResponse = JsonConvert.DeserializeObject<JsonRPCErrorResponse>(jsonRet);
                    throw new JsonRPCException(errorResponse.Error);
                }

                jsonRPCResponse.Raw = jsonRet;

                if (jsonRPCResponse.Error is null && !jsonRPCResponse.Id.HasValue && jsonRPCResponse.Result is null)
                {
                    jsonRPCResponse = null;
                }

                return jsonRPCResponse;
            }
            catch (Exception ex) when (!(ex is JsonRPCException))
            {
                var nEXt = ex;
                string errormsg = null;
                while (nEXt != null)
                {
                    if (nEXt is WebException)
                    {
                        var webEx = (WebException)nEXt;
                        if (webEx.Response != null)
                        {
                            HttpWebResponse resp = (HttpWebResponse)webEx.Response;
                            if (resp.StatusCode != HttpStatusCode.OK)
                            {
                                if (resp.ContentType != "application/json") throw ex;
                            }

                            using (var stream = webEx.Response.GetResponseStream())
                                errormsg = new StreamReader(stream).ReadToEnd();
                        }
                        if (errormsg == null) throw ex;
                        if (errormsg == "Forbidden") throw ex;

                        break;
                    }

                    nEXt = nEXt.InnerException;
                }

                var errorResponse = JsonConvert.DeserializeObject<JsonRPCErrorResponse>(errormsg);
                throw new JsonRPCException(errorResponse.Error);
            }
        }
    }
}