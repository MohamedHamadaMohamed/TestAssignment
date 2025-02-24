using TestAssignment.Repository.IRepository;

namespace TestAssignment.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public IBlockedAttemptRepository BlockedAttemptRepository {  get; private set; }

        public IBlockedCountryRepository BlockedCountryRepository {  get; private set; }

        public ITemporalBlockRepository TemporalBlockRepository {  get; private set; }


        public UnitOfWork()
        {
            BlockedAttemptRepository = new BlockedAttemptRepository();
            BlockedCountryRepository = new BlockedCountryRepository();
            TemporalBlockRepository = new TemporalBlockRepository();
        }
    }
}
