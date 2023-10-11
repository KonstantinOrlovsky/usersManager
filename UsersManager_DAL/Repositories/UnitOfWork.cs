using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using UsersManager_DAL.Context;
using UsersManager_DAL.Contracts.Repositories;

namespace UsersManager_DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;
        private readonly Dictionary<Type, object> _repositories = new Dictionary<Type, object>();
        private readonly IServiceProvider _serviceProvider;

        public UnitOfWork(AppDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _serviceProvider = serviceProvider;
        }

        public Dictionary<Type, object> Repositories
        {
            get => _repositories;
            set => Repositories = value;
        }

        public T Repository<T>() where T : class
        {
            if (Repositories.Keys.Contains(typeof(T)))
            {
                return Repositories[typeof(T)] as T;
            }

            var instance = Assembly.GetExecutingAssembly().GetTypes()
                .FirstOrDefault(f => f.GetInterfaces().Contains(typeof(T)));


            T repo = (T)ActivatorUtilities.CreateInstance(_serviceProvider, instance);

            Repositories.Add(typeof(T), repo);

            return repo;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}