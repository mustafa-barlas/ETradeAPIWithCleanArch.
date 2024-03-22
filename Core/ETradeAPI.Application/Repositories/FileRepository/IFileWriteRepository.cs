using File = ETradeAPI.Domain.Entities.File;

namespace ETradeAPI.Application.Repositories.FileRepository;

public interface IFileWriteRepository : IWriteRepository<File>
{
    
}