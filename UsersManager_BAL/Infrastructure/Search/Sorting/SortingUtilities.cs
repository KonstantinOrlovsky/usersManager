using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;

namespace UsersManager_BAL.Infrastructure.Search.Sorting;

public static class SortingUtilities
{
    public static IOrderedQueryable<TEntity> ThenSort<TEntity>(this IOrderedQueryable<TEntity> query, SortedBy sortedBy)
    {
        var pe = Expression.Parameter(typeof(TEntity), string.Empty);
        Expression property = Expression.PropertyOrField(pe, sortedBy.FieldName);

        var lambda = Expression.Lambda(property, pe);

        var orderbydir = sortedBy.ByDescending ? "ThenByDescending" : "ThenBy";

        var call = Expression.Call(
            typeof(Queryable),
            orderbydir,
            new [] { typeof(TEntity), property.Type },
            query.Expression,
            Expression.Quote(lambda));

        var returnquery = (IOrderedQueryable<TEntity>)query.Provider.CreateQuery<TEntity>(call);

        return returnquery;
    }

    public static IQueryable<TEntity> Sort<TEntity>(this IQueryable<TEntity> items, SortedBy sortedBy)
    {
        if (sortedBy.FieldName.IsNullOrEmpty())
        {
            return items;
        }

        var ordered = items.OrderBy(i => 0);
        return ordered.ThenSort(sortedBy);
    }
}