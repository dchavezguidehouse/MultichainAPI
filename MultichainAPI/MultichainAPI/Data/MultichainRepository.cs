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

        public async Task<JsonRPCResponse<MultichainInfoModel>> GetInfo()
        {
            return await Util.GetRPCResponse<MultichainInfoModel>("getinfo", new object[0]);
        }
    }
}
