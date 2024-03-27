using ETradeAPI.Application.Repositories.FileRepository;
using ETradeAPI.Persistence.Contexts;
using File = ETradeAPI.Domain.Entities.File;

namespace ETradeAPI.Persistence.Repositories.FileRepository;

public class FileReadRepository : ReadRepository<File>, IFileReadRepository
{
    public FileReadRepository(ETradeAPIDbContext context) : base(context)
    {
    }
}