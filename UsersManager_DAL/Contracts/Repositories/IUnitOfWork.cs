namespace UsersManager_DAL.Contracts.Repositories
{
    public interface IUnitOfWork
    {
        T Repository<T>() where T : class;
        Task<int> SaveChangesAsync();
    }
}