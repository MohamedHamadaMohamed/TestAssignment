using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TestAssignment.Data;
using TestAssignment.Services;

namespace TestAssignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IPController : ControllerBase
    {
        private readonly IPGeolocationService _iPGeolocationService;

        public IPController(IPGeolocationService iPGeolocationService)
        {
            _iPGeolocationService = iPGeolocationService;
        }
        [HttpGet("lookup")]

        public async Task<IActionResult> lookup(string ipAddress)
        {
            var countryCode = await _iPGeolocationService.GetCountryByIPAsync(ipAddress);
            return Ok(new { ipAddress, countryCode });
        }
        [HttpGet("check-block")]
        public async Task<IActionResult> CheckIPBlocked()
        {
            var userIp = "8.8.8.8";// HttpContext.Connection.RemoteIpAddress?.ToString();
            if (string.IsNullOrEmpty(userIp))
                return BadRequest("Unable to determine IP address.");

            var countryCode = await _iPGeolocationService.GetCountryByIPAsync(userIp);
            if (string.IsNullOrEmpty(countryCode))
                return BadRequest("Unable to determine country Code");

            bool isBlocked = BlockedCounty.IsBlocked(countryCode);

            if (isBlocked)
                BlockedCounty.AddToBlockedAttempts(userIp, countryCode, Request.Headers["User-Agent"].ToString());

            return Ok(new { userIp, countryCode, isBlocked });
        }

    }
}
