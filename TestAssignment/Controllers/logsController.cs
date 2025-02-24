using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestAssignment.Data;

namespace TestAssignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class logsController : ControllerBase
    {
        [HttpGet("blocked-attempts")]
        public IActionResult Index(int currentPage=1)
        {
            if (currentPage < 1) currentPage = 1;
            var BlockedAttempts = BlockedCounty.BlockedAttempts.Skip(currentPage-1).Take(5).ToList();

            return Ok(BlockedAttempts);


        }
    }
}
