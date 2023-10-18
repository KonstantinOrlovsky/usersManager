using UsersManager_DAL.Contracts.Repositories;

namespace UsersManager_DAL.Helpers
{
    public static class MergeHelper
    {
        public static async Task MergeCollection<TFrom, TTo>(
           IEnumerable<TFrom> fromCollection,
           IEnumerable<TTo> toCollection,
           IRepository<TTo> repo,
           Func<TFrom, TTo, bool> predicate,
           Action<TFrom, TTo> extraProps = default) where TTo : class, new()
        {
            var entitiesToAdd = fromCollection.Where(f => !toCollection.Any(t => predicate(f, t))).ToList();
            var entitiesToRemove = toCollection.Where(t => !fromCollection.Any(f => predicate(f, t))).ToList();

            if (entitiesToAdd.Any())
            {
                foreach (var from in entitiesToAdd)
                {
                    var objectToAdd = CopyPropsHelper.CopySimplePropsFromObjectToAnother(from, extraProps);
                    await repo.AddAsync(objectToAdd);
                }
            }

            if (entitiesToRemove.Any())
            {
                repo.RemoveRangeWithoutSave(entitiesToRemove);
            }
        }
    }
}