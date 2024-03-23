using ETradeAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ETradeAPI.Persistence;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ETradeApiDbContext>
{
    public ETradeApiDbContext CreateDbContext(string[] args)
    {


        DbContextOptionsBuilder<ETradeApiDbContext> dbContextOptionsBuilder = new();
        dbContextOptionsBuilder.UseSqlServer(Configuration.ConnectionString);

        return new ETradeApiDbContext(dbContextOptionsBuilder.Options);
    }
}