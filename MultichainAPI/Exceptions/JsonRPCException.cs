using System;
using Newtonsoft.Json;

namespace MultichainAPI.Exceptions
{
    [Serializable]
    public class JsonRPCException : Exception
    {
        public JsonRPCException() { }
        public JsonRPCException(string message) : base(message) { }
        public JsonRPCException(JsonRPCError error) : this($"({error.Code}) {error.Message}", error) { }
        public JsonRPCException(string message, JsonRPCError response) : base(message) { Error = response; }
        public JsonRPCException(string message, JsonRPCError response, Exception inner) : base(message, inner) { Error = response; }
        public JsonRPCException(string message, Exception inner) : base(message, inner) { }

        protected JsonRPCException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

        public JsonRPCError Error { get; set; }
    }

    public class JsonRPCError
    {
        [JsonProperty("code")]
        public int Code { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
    }

    public class JsonRPCErrorResponse
    {
        [JsonProperty("error")]
        public JsonRPCError Error { get; set; }
    }
}
