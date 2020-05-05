using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MultichainAPI.Models;

namespace MultichainAPI.Interfaces
{
    public interface IMultichainRepository
    {
        Task<JsonRPCResponse<MultichainInfoModel>> GetInfo();
    }
}
