using System;
using Newtonsoft.Json;

namespace MultichainAPI.Models
{
    public class JsonRPCResponse<T>
    {
        [JsonProperty("result")]
        public T Result { get; set; }

        [JsonProperty("error")]
        public string Error { get; set; }

        [JsonProperty("id")]
        public int? Id { get; set; }

        [JsonIgnore]
        public string Raw { get; internal set; }

        public void IsValidResponse()
        {
            if (!(string.IsNullOrEmpty(this.Error)))
                throw new InvalidOperationException("Error(s) occurred: " + this.Error);
        }
    }
}
