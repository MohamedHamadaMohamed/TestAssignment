using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestAssignment.Data;
using TestAssignment.Models;
using TestAssignment.Repository.IRepository;
using TestAssignment.Services;

namespace TestAssignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlockedCountriesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public BlockedCountriesController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }


        [HttpGet("blocked")]
        public IActionResult blocked(int currentPage =1 , string? searchInput=null)
        {
            if (currentPage < 1) currentPage = 1;
            var blockedCoutries = new List<BlockedCountry>();
            if(searchInput !=null)
            {
                blockedCoutries = _unitOfWork.BlockedCountryRepository.Retrive(e => e.code.Contains(searchInput)).Skip((currentPage - 1) * 5).Take(5).ToList(); //InMemoryCollections.BlocledCountries.Where(e =>e.Contains(searchInput)).Skip((currentPage - 1) * 5).Take(5).ToList();
            }
            else
            {
                blockedCoutries = _unitOfWork.BlockedCountryRepository.Retrive().Skip((currentPage - 1) * 5).Take(5).ToList(); //InMemoryCollections.BlocledCountries.Where(e =>e.Contains(searchInput)).Skip((currentPage - 1) * 5).Take(5).ToList();

            }


            return Ok(blockedCoutries);
        }

        [HttpPost("block")]
        public IActionResult block(string code)
        {
            // Prevent duplicates

            var coutry = _unitOfWork.BlockedCountryRepository.RetriveItem(filter: e => e.code == code);


            if (coutry == null)
            {
                _unitOfWork.BlockedCountryRepository.Create(new BlockedCountry { code = code });
                return Ok($"Country {code} blocked successfully.");

            }
            return Ok($"Country {code} have already  blocked .");

        }
        [HttpDelete("block/{countryCode}")]
        public IActionResult UnBlock(string code)
        {
            var coutry = _unitOfWork.BlockedCountryRepository.RetriveItem(filter: e => e.code == code);

            if (coutry != null)
            {
                _unitOfWork.BlockedCountryRepository.Delete(coutry);
                return Ok($"Country {code} unblocked successfully.");

            }
            return BadRequest("the country is not blocked.");

        }
       
    }
}
