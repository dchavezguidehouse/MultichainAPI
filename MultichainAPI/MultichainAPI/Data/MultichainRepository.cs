using Microsoft.Extensions.Configuration;
using MultichainAPI.Common;
using MultichainAPI.Exceptions;
using MultichainAPI.Interfaces;
using MultichainAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MultichainAPI.Data
{
    public class MultichainRepository : IMultichainRepository
    {
        private readonly IConfiguration _config;

        public MultichainRepository(IConfiguration config)
        {
            _config = config;
        }

        public async Task<JsonRPCResponse<MultichainInfoModel>> GetInfo()
        {
            var ps = new JsonRPCRequest()
            {
                Method = "getinfo",
                Params = null,
                Id = 1
            };

            var jsonOut = JsonConvert.SerializeObject(ps.Values);

            try
            {
                string jsonRet = null;
                JsonRPCResponse<MultichainInfoModel> ret = null;

                using (var client = Util.GetHttpClientWithBasicAuth("http://192.168.99.105:7358", "multichainrpc", "EFELF94ofgXRbaCezUn1RSHLARnN5LTsEADFXEyFV7Gu"))
                {
                    var httpContent = new StringContent(jsonOut, Encoding.UTF8, "application/json-rpc");
                    var request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Post,
                        RequestUri = new Uri("http://192.168.99.105:7358"),
                        Content = httpContent,
                    };

                    var result = await client.SendAsync(request);
                    jsonRet = await result.Content.ReadAsStringAsync();
                }

                try
                {
                    ret = JsonConvert.DeserializeObject<JsonRPCResponse<MultichainInfoModel>>(jsonRet);
                }
                catch (Exception jsonEx)
                {
                    var errorResponse = JsonConvert.DeserializeObject<JsonRPCErrorResponse>(jsonRet);
                    throw new JsonRPCException(errorResponse.Error);
                }

                ret.Raw = jsonRet;

                if (ret.Error == null && !ret.Id.HasValue && ret.Result == null)
                {
                    ret = null;
                }

                return ret;
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
