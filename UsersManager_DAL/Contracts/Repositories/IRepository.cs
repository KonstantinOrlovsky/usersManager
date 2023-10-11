namespace UsersManager_DAL.Contracts.Repositories
{
    public interface IRepository<TEntity>
    {
        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
        void AddRangeWithoutSave(IEnumerable<TEntity> entities);
        TEntity Update(TEntity entity);
        void Delete(TEntity entity);
        void RemoveRangeWithoutSave(IEnumerable<TEntity> enities);
        void RemoveWithoutSave(TEntity enitiy);
        IQueryable<TEntity> Query();
        TEntity? Attach(TEntity? entity);
        void DetacheEntity(TEntity entity);
        IQueryable<TEntity> GetAll();
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}