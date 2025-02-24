using System.Linq.Expressions;

namespace TestAssignment.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        public void Create(T entity);
        public void Delete(T entity);
        public List<T> Retrive(Expression<Func<T, bool>>? filter = null);
       public T? RetriveItem(Expression<Func<T, bool>>? filter = null);
    }
}
