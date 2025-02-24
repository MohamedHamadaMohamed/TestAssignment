namespace TestAssignment.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IBlockedAttemptRepository BlockedAttemptRepository { get; }
        IBlockedCountryRepository BlockedCountryRepository { get; }
        ITemporalBlockRepository TemporalBlockRepository { get; }
    }
}
