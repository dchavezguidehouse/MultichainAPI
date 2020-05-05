using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MultichainAPI.Models
{
    public class JsonRPCRequest
    {
        public Dictionary<string, object> Values { get; private set; }

        
        public JsonRPCRequest()
        {
            this.Values = new Dictionary<string, object>();
        }

        [JsonProperty("method")]
        public string Method
        {
            get
            {
                return this.GetValue<string>("method");
            }
            set
            {
                this.SetValue("method", value);
            }
        }

        
        [JsonProperty("params")]
        public object[] Params
        {
            get
            {
                return this.GetValue<object[]>("params");
            }
            set
            {
                this.SetValue("params", value);
            }
        }

        [JsonProperty("id")]
        public int Id
        {
            get
            {
                return this.GetValue<int>("id");
            }
            set
            {
                this.SetValue("id", value);
            }
        }


        private void SetValue(string name, object value)
        {
            this.Values[name] = value;
        }

        public T GetValue<T>(string name)
        {
            if (this.Values.ContainsKey(name))
                return (T)this.Values[name];
            else
                return default(T);
        }
    }
}
