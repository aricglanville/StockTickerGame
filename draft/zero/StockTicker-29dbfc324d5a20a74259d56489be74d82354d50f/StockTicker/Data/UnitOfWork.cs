using StockTicker.Interfaces;

namespace StockTicker.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDBContext _dbContext;

        public UnitOfWork(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        //private IGenericRepository<ApplicationUser> _ApplicationUser;
        //private IGenericRepository<Transaction> _Transaction;

        //public IGenericRepository<ApplicationUser> ApplicationUser
        //{
        //    get
        //    {
        //        if (_ApplicationUser == null)
        //        {
        //            _ApplicationUser = new GenericRepository<ApplicationUser>(_dbContext);
        //        }
        //        return _ApplicationUser;
        //    }
        //}
        //public IGenericRepository<Transaction> Transaction
        //{
        //    get
        //    {
        //        if (_Transaction == null)
        //        {
        //            _Transaction = new GenericRepository<Transaction>(_dbContext);
        //        }
        //        return _Transaction;
        //    }
        //}

        public int Commit()
        {
            return _dbContext.SaveChanges();
        }

        public async Task<int> CommitAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
