using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TestAssignment.Data;
using TestAssignment.Models;
using TestAssignment.Repository.IRepository;

namespace TestAssignment.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private static readonly Dictionary<Type, object> _dataSources = new()
    {
        { typeof(BlockedCountry), InMemoryCollections.BlocledCountries },
        { typeof(BlockedAttempt), InMemoryCollections.BlockedAttempts },
        { typeof(TemporalBlock), InMemoryCollections.TemporalBlocks }
    };


        private List<T> _dbSet;

        public Repository()
        {
            if (_dataSources.TryGetValue(typeof(T), out var dataSource))
            {
                _dbSet = (List<T>)dataSource;
            }
            else
            {
                throw new InvalidOperationException($"No data source found for type {typeof(T).Name}");
            }
        }
        public void Create(T entity)
        {
            _dbSet.Add(entity);

        }
        public void Delete(T entity)
        {
            _dbSet.Remove(entity);

        }
        public List<T> Retrive(Expression<Func<T, bool>>? filter = null)
        {
            List<T> query = _dbSet;
            if (filter != null)
            {
                query = query.AsQueryable().Where(filter).ToList();
            }
            
            return query.ToList();
        }
        public T? RetriveItem(Expression<Func<T, bool>>? filter = null)
        {
            return Retrive(filter).FirstOrDefault();
        }
    }
}
