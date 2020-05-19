using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MultichainAPI.Common;
using MultichainAPI.Interfaces;
using MultichainAPI.Models;

namespace MultichainAPI.Data
{
    public class MultichainRepository : IMultichainRepository
    {
        private readonly IConfiguration _config;

        public MultichainRepository(IConfiguration config)
        {
            _config = config;
        }

        public async Task<JsonRPCResponse<MultichainInfoReturnModel>> GetInfo()
        {
            return await Util.GetRPCResponse<MultichainInfoReturnModel>("getinfo", new object[0]);
        }

        public async Task<JsonRPCResponse<string>> IssueAsset(IssueAssetRequestModel issueAssetRequestModel)
        {
            return await Util.GetRPCResponse<string>("issue", new object[] 
            {
                issueAssetRequestModel.Address,
                issueAssetRequestModel.AssetName,
                issueAssetRequestModel.Quantity
            });
        }
    }
}
