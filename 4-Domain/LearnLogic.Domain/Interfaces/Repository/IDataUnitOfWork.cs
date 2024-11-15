namespace LearnLogic.Domain.Interfaces.Repository
{
    public interface IDataUnitOfWork
    {
        IDataContextSolution Context { get; }
        void Commit();
        void Rollbak();
    }
}
