using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestAssignment.Data;
using TestAssignment.Models;
using TestAssignment.Repository;
using TestAssignment.Repository.IRepository;

namespace TestAssignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemporalBlockController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public TemporalBlockController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;

        }
        [HttpPost("temporal-block")]
        public IActionResult TemporyBlock(string countryCode, int DurationMinutes)
        {


            if (string.IsNullOrWhiteSpace(countryCode) || DurationMinutes < 1 || DurationMinutes > 1440)
            {
                return BadRequest("Invalid country code or duration.");
            }
            if (_unitOfWork.BlockedCountryRepository.RetriveItem(e => e.code == countryCode) != null)
            {
                return BadRequest($"Country {countryCode} is already blocked");

            }

            var temp = _unitOfWork.TemporalBlockRepository.RetriveItem(filter: e => e.CountryCode == countryCode);
            if (temp == null)
            {
                _unitOfWork.TemporalBlockRepository.Create(new TemporalBlock
                {
                    CountryCode = countryCode,
                    dateTime = DateTime.UtcNow.AddMinutes(DurationMinutes)
                });
                return Ok($"Country {countryCode} temporarily blocked for {DurationMinutes} minutes.");

            }
            return BadRequest($"Country {countryCode} is already blocked");



        }
        [HttpGet("TemporyBlocked")]
        public IActionResult TemporyBlocked(int currentPage = 1, string? searchInput = null)
        {
            if (currentPage < 1) currentPage = 1;
            var TemporalBlocks = new List<TemporalBlock>();
            if (searchInput != null)
            {
                TemporalBlocks = _unitOfWork.TemporalBlockRepository.Retrive(e => e.CountryCode.Contains(searchInput)).Skip((currentPage - 1) * 5).Take(5).ToList();
            }
            else
            {
                TemporalBlocks = _unitOfWork.TemporalBlockRepository.Retrive().Skip((currentPage - 1) * 5).Take(5).ToList();

            }
            return Ok(TemporalBlocks);
        }







    }
}
