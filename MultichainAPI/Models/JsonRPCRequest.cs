using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MultichainAPI.Models
{
    public class JsonRPCRequest
    {

        [JsonProperty("method")]
        public string Method { get; set; }

        [JsonProperty("params")]
        public object[] Params { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }
    }
}
