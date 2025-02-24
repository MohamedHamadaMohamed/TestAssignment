using TestAssignment.Models;
using TestAssignment.Repository.IRepository;

namespace TestAssignment.Repository
{
    public class BlockedCountryRepository :  Repository<BlockedCountry>, IBlockedCountryRepository
    {
    }
}
