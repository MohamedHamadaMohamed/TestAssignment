using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TestAssignment.Data;
using TestAssignment.Models;
using TestAssignment.Repository;
using TestAssignment.Repository.IRepository;
using TestAssignment.Services;

namespace TestAssignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IPController : ControllerBase
    {
        private readonly IPGeolocationService _iPGeolocationService;
        private readonly IUnitOfWork _unitOfWork;

        public IPController(IPGeolocationService iPGeolocationService , IUnitOfWork unitOfWork)
        {
            _iPGeolocationService = iPGeolocationService;
            this._unitOfWork = unitOfWork;

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

            var blocked = _unitOfWork.BlockedCountryRepository.RetriveItem(filter: e => e.code == countryCode);
            var temp = _unitOfWork.TemporalBlockRepository.RetriveItem(filter: e => e.CountryCode == countryCode);

            bool isBlocked = blocked !=null || temp != null;

            if (isBlocked)
            {
                _unitOfWork.BlockedAttemptRepository.Create(new BlockedAttempt
                {
                    CountryCode = countryCode,
                    IP = userIp,
                    UserAgent = Request.Headers["User-Agent"].ToString(),
                    Timestamp = DateTime.UtcNow

                });
            }



            return Ok(new { userIp, countryCode, isBlocked });
        }

    }
}
