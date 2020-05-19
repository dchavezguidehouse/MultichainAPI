using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MultichainAPI.Interfaces;
using MultichainAPI.Models;

namespace MultichainAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MultichainController : ControllerBase
    {
        private readonly IMultichainRepository _multichainRepository;

        public MultichainController(IMultichainRepository multichainRepository)
        {
            _multichainRepository = multichainRepository;
        }

        [HttpGet("Info", Name = "Get_Info")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(JsonRPCResponse<MultichainInfoReturnModel>))]
        public async Task<ActionResult> GetInfo()
        {
            var result = await _multichainRepository.GetInfo();
            return Ok(result);
        }

        [HttpPost("IssueAsset", Name = "Issue_Asset")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(string))]
        public async Task<ActionResult> QueryTransactionData([FromBody] IssueAssetRequestModel body)
        {
            var result = await _multichainRepository.IssueAsset(body);
            return Ok(result);
        }
    }
}
