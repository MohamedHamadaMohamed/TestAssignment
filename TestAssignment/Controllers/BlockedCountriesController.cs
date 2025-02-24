using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestAssignment.Data;
using TestAssignment.Models;
using TestAssignment.Services;

namespace TestAssignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlockedCountriesController : ControllerBase
    {
        

        [HttpGet("blocked")]
        public IActionResult blocked(int currentPage =1 , string? searchInput=null)
        {
            if (currentPage < 1) currentPage = 1;
            var blockedCoutries = new List<string>();
            if(searchInput !=null)
            {
                blockedCoutries = BlockedCounty.BlocledCountries.Where(e =>e.Contains(searchInput)).Skip((currentPage - 1) * 5).Take(5).ToList();
            }
            else
            {
                blockedCoutries = BlockedCounty.BlocledCountries.Skip((currentPage - 1) * 5).Take(5).ToList();

            }


            return Ok(blockedCoutries);
        }
        [HttpPost("block")]
        public IActionResult block(string code)
        {
            // Prevent duplicates

            if (!BlockedCounty.IsBlocked(code))
            {
                BlockedCounty.Add(code);
            }
            return Ok($"Country {code} blocked successfully.");
        }
        [HttpDelete("block/{countryCode}")]
        public IActionResult UnBlock(string code)
        {
            if (BlockedCounty.IsBlocked(code))
            {
                BlockedCounty.Remove(code);
                return Ok($"Country {code} unblocked successfully.");
            }
            return BadRequest("the country is not blocked.");
        }
        [HttpPost("temporal-block")]
        public IActionResult TemporyBlock(string countryCode, int DurationMinutes)
        {
            BlockedCounty.AddToTemporalBlocks(countryCode, DurationMinutes);
            return Ok($"Country {countryCode} temporarily blocked for {DurationMinutes} minutes.");
        }
        [HttpGet("TemporyBlocked")]
        public IActionResult TemporyBlocked(int currentPage = 1, string? searchInput = null)
        {
            if (currentPage < 1) currentPage = 1;
            var TemporalBlocks = new List<TemporalBlock>();
            if (searchInput != null)
            {
                TemporalBlocks = BlockedCounty.TemporalBlocks.Where(e => e.CountryCode.Contains(searchInput)).Skip((currentPage - 1) * 5).Take(5).ToList();
            }
            else
            {
                TemporalBlocks = BlockedCounty.TemporalBlocks.Skip((currentPage - 1) * 5).Take(5).ToList();

            }
            return Ok(TemporalBlocks);
        }
    }
}
