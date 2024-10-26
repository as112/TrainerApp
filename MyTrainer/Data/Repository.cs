using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MyTrainer.Data;

public class Repository<T> where T : class
{
    private readonly TrainingAppContext _context;
    private DbSet<T> Set { get; }
    protected IQueryable<T> QueryableSet => Set.AsQueryable();

    public Repository(TrainingAppContext context)
    {
        _context = context;
        Set = _context.Set<T>();
    }

    public int Count() => Set.Count();
    public async Task<List<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public async Task AddAsync(T entity)
    {
        _context.Set<T>().Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        _context.Set<T>().Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync();
    }
    public async Task<IEnumerable<T>?> GetAllWithPredicate(Expression<Func<T, bool>> predicate)
    {
        return await QueryableSet.Where(predicate).ToArrayAsync();
    }
}
