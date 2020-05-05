using Microsoft.Extensions.Configuration;
using MultichainAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
        //public async Task<int> GetInfo()
        //{
        //    return Task<int>;
        //}
    }
}
