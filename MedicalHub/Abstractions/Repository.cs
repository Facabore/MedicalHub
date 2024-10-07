using MedicalHub.Infrastructure;
using MedicalHub.Repository;
using Microsoft.EntityFrameworkCore;

namespace MedicalHub.Abstractions;

public class Repository<T>(AppDbContext context) : IRepository<T> where T : Entity
{
    private readonly AppDbContext _context = context;
    private readonly DbSet<T> _dbSet = context.Set<T>();

    public async Task<IEnumerable<T>> GetAll()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<T?> GetById(Guid id)
    {
        return await _dbSet.FirstOrDefaultAsync(e => e.Id == id);

    }
    
    public async Task<T?> GetByIdentification(string identificationNumber)
    {
        var entityType = typeof(T);
        var property = entityType.GetProperty("IdentificationNumber");
        if (property == null) throw new InvalidOperationException($"{entityType.Name} does not have a property 'IdentificationNumber'.");
        return await _dbSet.FirstOrDefaultAsync(e => property.GetValue(e).ToString() == identificationNumber);
    }

    public async Task Add(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Update(T entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(T entity)
    {
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
    }
}
