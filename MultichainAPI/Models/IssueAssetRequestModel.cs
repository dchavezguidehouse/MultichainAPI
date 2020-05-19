using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultichainAPI.Models
{
    public class IssueAssetRequestModel
    {
        public string Address { get; set; }
        public string AssetName { get; set; }
        public AssetParameters AssetParams { get; set; }
        public class AssetParameters
        {
            public string Name { get; set; }
            public bool Open { get; set; }
            public string Restrict { get; set; }
        }
        public int Quantity { get; set; }
        public int SmallestUnit { get; set; }
        public int NativeAmount { get; set; }
        public IDictionary<string, string> CustomParams { get; set; }
    }
}


//1. "address"                        (string, required) The address to send newly created asset to.
//2. "asset-name"                     (string, required) Asset name, if not "" should be unique.
// or
//2. asset-params                     (object, required) A json object of with asset params
//    {
//      "name" : "asset-name"         (string, optional) Asset name
//      "open" : true|false           (boolean, optional, default false) True if follow-on issues are allowed
//      "restrict" : "restrictions"   (string, optional) Permission strings, comma delimited.Possible values: send, receive
//    }
//3. quantity(numeric, required) The asset total amount in display units.eg. 1234.56
//4. smallest-unit                    (numeric, optional, default=1) Number of raw units in one displayed unit, eg 0.01 for cents
//5. native-amount(numeric, optional) native currency amount to send.eg 0.1, Default: minimum-per-output.
//6  custom-fields(object, optional)  a json object with custom fields
//{
//      "param-name": "param-value"   (strings, required) The key is the parameter name, the value is parameter value
//      ,...
//    }