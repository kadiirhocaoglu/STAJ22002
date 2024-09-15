using HST.DataAccess.Concrete;
using HST.DataAccess.Repositories.Abstract;
using HST.DataAccess.Repositories.Concrete;
using HST.DataAccess.UnitOfWork.Abstact;
using System;


namespace HST.DataAccess.UnitOfWork.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Context dbContext;

        public UnitOfWork(Context dbContext)
        {
            this.dbContext = dbContext;
        }
        public async ValueTask DisposeAsync()
        {
            await dbContext.DisposeAsync();
        }

        public int Save()
        {
            return dbContext.SaveChanges();
        }

        public async Task<int> SaveAsync()
        {
            return await dbContext.SaveChangesAsync();
        }

        IRepository<T> IUnitOfWork.GetRepository<T>()
        {
            return new Repository<T>(dbContext);
        }
    }
}
