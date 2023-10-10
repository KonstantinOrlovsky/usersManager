using Microsoft.EntityFrameworkCore;
using UsersManager_DAL.Context;
using UsersManager_DAL.Contracts.Repositories;

namespace UsersManager_DAL.Repositories
{
    public class EfRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public EfRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = context.Set<TEntity>();
        }

        public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            var attachedEntity = Attach(entity);
            await _dbSet.AddAsync(attachedEntity, cancellationToken);

            return attachedEntity;
        }

        public void AddRangeWithoutSave(IEnumerable<TEntity> entities)
        {
            if (entities != default && entities.Any())
            {
                _dbSet.AddRange(entities);
            }
        }

        public TEntity Update(TEntity entity)
        {
            var attachedEntity = Attach(entity);
            _dbSet.Update(attachedEntity);

            return entity;
        }

        public void Delete(TEntity entity) => _dbSet.Remove(entity);

        public IQueryable<TEntity> Query() => _dbSet.AsQueryable();

        public void RemoveRangeWithoutSave(IEnumerable<TEntity> enities)
        {
            if (enities != default && enities.Any())
            {
                _dbSet.RemoveRange(enities);
            }
        }

        public void RemoveWithoutSave(TEntity enitiy)
        {
            _dbSet.RemoveRange(enitiy);
        }

        public void DetacheEntity(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Detached;
        }

        public TEntity? Attach(TEntity? entity)
        {
            if (entity == default)
            {
                return default;
            }

            var entry = _dbSet.Attach(entity);
           
            return entry.Entity;
        }

        public IQueryable<TEntity> GetAll()
        {
            return _dbSet.AsNoTracking();
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}