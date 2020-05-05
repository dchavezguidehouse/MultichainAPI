using System;
using Newtonsoft.Json;

namespace MultichainAPI.Models
{
    public class MultichainInfoModel
    {
        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("nodeversion")]
        public long Nodeversion { get; set; }

        [JsonProperty("edition")]
        public string Edition { get; set; }

        [JsonProperty("protocolversion")]
        public long Protocolversion { get; set; }

        [JsonProperty("chainname")]
        public string Chainname { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("protocol")]
        public string Protocol { get; set; }

        [JsonProperty("port")]
        public long Port { get; set; }

        [JsonProperty("setupblocks")]
        public long Setupblocks { get; set; }

        [JsonProperty("nodeaddress")]
        public string Nodeaddress { get; set; }

        [JsonProperty("burnaddress")]
        public string Burnaddress { get; set; }

        [JsonProperty("incomingpaused")]
        public bool Incomingpaused { get; set; }

        [JsonProperty("miningpaused")]
        public bool Miningpaused { get; set; }

        [JsonProperty("offchainpaused")]
        public bool Offchainpaused { get; set; }

        [JsonProperty("walletversion")]
        public long Walletversion { get; set; }

        [JsonProperty("balance")]
        public long Balance { get; set; }

        [JsonProperty("walletdbversion")]
        public long Walletdbversion { get; set; }

        [JsonProperty("reindex")]
        public bool Reindex { get; set; }

        [JsonProperty("blocks")]
        public long Blocks { get; set; }

        [JsonProperty("timeoffset")]
        public long Timeoffset { get; set; }

        [JsonProperty("connections")]
        public long Connections { get; set; }

        [JsonProperty("proxy")]
        public string Proxy { get; set; }

        [JsonProperty("difficulty")]
        public double Difficulty { get; set; }

        [JsonProperty("testnet")]
        public bool Testnet { get; set; }

        [JsonProperty("keypoololdest")]
        public long Keypoololdest { get; set; }

        [JsonProperty("keypoolsize")]
        public long Keypoolsize { get; set; }

        [JsonProperty("paytxfee")]
        public long Paytxfee { get; set; }

        [JsonProperty("relayfee")]
        public long Relayfee { get; set; }

        [JsonProperty("errors")]
        public string Errors { get; set; }
        
    }
}
