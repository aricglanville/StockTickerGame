using System.Linq.Expressions;

namespace StockTicker.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        T GetById(int id);

        T Get(Expression<Func<T, bool>> predicate, bool asNoTracking = false, string includes = null);
        Task<T> GetAsync(Expression<Func<T, bool>> predicate, bool asNoTracking = false, string includes = null);
        IEnumerable<T> List();
        IEnumerable<T> List(Expression<Func<T, bool>> predicate, Expression<Func<T, int>> orderBy = null, string includes = null);
        Task<IEnumerable<T>> ListAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, int>> orderBy = null, string includes = null);
        void Add(T entity);
        void Delete(T entity);
        void Delete(IEnumerable<T> entities);
        IEnumerable<T> GetAll(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null
            );
        T GetFirstOrDefault(
            Expression<Func<T, bool>> filter = null,
            string includeProperties = null
            );
        void Update(T entity);
    }
}
