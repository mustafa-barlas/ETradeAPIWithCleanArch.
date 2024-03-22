using ETradeAPI.Application.Repositories.FileRepository;
using ETradeAPI.Persistence.Contexts;
using File = ETradeAPI.Domain.Entities.File;

namespace ETradeAPI.Persistence.Repositories.FileRepository;

public class FileWriteRepository : WriteRepository<File>, IFileWriteRepository
{
    public FileWriteRepository(ETradeAPIDbContext context) : base(context)
    {
    }
}