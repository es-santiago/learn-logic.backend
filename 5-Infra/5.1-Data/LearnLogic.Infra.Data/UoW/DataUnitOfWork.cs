using LearnLogic.Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore.Storage;

namespace LearnLogic.Infra.Data.UoW
{
    public class DataUnitOfWork : IDataUnitOfWork
    {
        private readonly IDataContextSolution DataContext;

        private readonly IDbContextTransaction Transaction;

        IDataContextSolution IDataUnitOfWork.Context
            => DataContext;


        public DataUnitOfWork(IDataContextSolution context)
            => DataContext = context;

        public void Commit()
        {
            Transaction?.Commit();
        }

        public void Rollbak()
        {
            Transaction?.Rollback();
        }
    }
}
