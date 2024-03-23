using ETradeAPI.Application.Repositories.InvoiceFileRepository;
using ETradeAPI.Domain.Entities;
using ETradeAPI.Persistence.Contexts;

namespace ETradeAPI.Persistence.Repositories.InvoiceFileRepository;

public class InvoiceFileWriteRepository : WriteRepository<InvoiceFile>, IInvoiceFileWriteRepository
{
    public InvoiceFileWriteRepository(ETradeApiDbContext context) : base(context)
    {
    }
}