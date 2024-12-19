using _6.Repositories.DB;
using _7.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace _6.Repositories.Repository;
public class _BaseModuleRepository<T> where T : class
{
    private readonly MyDbContext _dbContext;
    private readonly DbSet<T> _dbSet;

    public _BaseModuleRepository(MyDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<T>();
    }

    /// <summary>
    /// Get records from any table based on a dynamic condition.
    /// </summary>
    /// <param name="condition">The condition to filter the records.</param>
    /// <param name="fields">An optional list of fields to select.</param>
    /// <param name="result">Specify "row" to get a single record, "result" to get a list.</param>
    /// <returns>A list of records matching the condition.</returns>
    public async Task<IEnumerable<T>> SelectAllDataAsync(
        Dictionary<string, object> condition,
        string[] fields = null,
        string result = "result")
    {
        IQueryable<T> query = _dbSet.Where(BuildPredicate(condition));

        if (fields != null && fields.Length > 0)
        {
            // If specific fields are provided, select those fields using projection
            query = query.Select(BuildProjection(fields));
        }

        var data = await query.ToListAsync();

        if (result == "row" && data.Any())
        {
            return data.Take(1);  // Return only the first record
        }
        else
        {
            return data;  // Return all matching records
        }
    }


    private Expression<Func<T, bool>> BuildPredicate(Dictionary<string, object> condition)
    {
        var parameter = Expression.Parameter(typeof(T), "x");
        Expression predicate = Expression.Constant(true);  // Start with 'true'

        foreach (var conditionItem in condition)
        {
            // Get the property from the parameter
            var property = Expression.Property(parameter, conditionItem.Key);

            // Get the value from the condition
            var value = Expression.Constant(conditionItem.Value);

            // Handle nullable and non-nullable types
            if (property.Type.IsGenericType && property.Type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                // Property is nullable
                var nullableValue = Expression.Convert(value, property.Type); // Convert value to the nullable type
                var equality = Expression.Equal(property, nullableValue);
                predicate = Expression.AndAlso(predicate, equality);
            }
            else
            {
                // Property is non-nullable
                var equality = Expression.Equal(property, value);
                predicate = Expression.AndAlso(predicate, equality);
            }
        }

        return Expression.Lambda<Func<T, bool>>(predicate, parameter);
    }


    private Expression<Func<T, T>> BuildProjection(string[] fields)
    {
        var parameter = Expression.Parameter(typeof(T), "x");
        var bindings = new List<MemberBinding>();

        foreach (var field in fields)
        {
            var property = typeof(T).GetProperty(field);
            if (property != null)
            {
                var memberExpression = Expression.Property(parameter, property);
                bindings.Add(Expression.Bind(property, memberExpression));
            }
        }

        var body = Expression.MemberInit(Expression.New(typeof(T)), bindings);
        return Expression.Lambda<Func<T, T>>(body, parameter);
    }
}

