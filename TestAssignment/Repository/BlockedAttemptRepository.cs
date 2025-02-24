using System.Numerics;
using TestAssignment.Models;
using TestAssignment.Repository.IRepository;

namespace TestAssignment.Repository
{
    public class BlockedAttemptRepository : Repository<BlockedAttempt>, IBlockedAttemptRepository
    {
    }
}
