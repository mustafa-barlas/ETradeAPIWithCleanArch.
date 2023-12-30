using System.Linq.Expressions;
using ETradeAPI.Application.Repositories;
using ETradeAPI.Domain.Entities.Common;
using ETradeAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ETradeAPI.Persistence.Repositories;

public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
{
    private readonly ETradeAPIDbContext _context;

    public ReadRepository(ETradeAPIDbContext context)
    {
        _context = context;
        
    }

    public DbSet<T> Table => _context.Set<T>();


    public IQueryable<T> GetAll() => Table;


    public IQueryable<T> GetWhere(Expression<Func<T, bool>> method) => Table.Where(method);


    public async Task<T> GetSingleAsync(Expression<Func<T, bool>> method) => await Table.FirstOrDefaultAsync(method);

    public async Task<T> GetByIdAsync(string id) =>await Table.FirstOrDefaultAsync(data => data.Id.Equals(Guid.Parse(id)));
}