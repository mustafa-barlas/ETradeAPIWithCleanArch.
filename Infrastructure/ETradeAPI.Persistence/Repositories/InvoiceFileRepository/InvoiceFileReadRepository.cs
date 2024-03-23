using ETradeAPI.Application.Repositories.InvoiceFileRepository;
using ETradeAPI.Domain.Entities;
using ETradeAPI.Persistence.Contexts;

namespace ETradeAPI.Persistence.Repositories.InvoiceFileRepository;

public class InvoiceFileReadRepository : ReadRepository<InvoiceFile>, IInvoiceFileReadRepository
{
    public InvoiceFileReadRepository(ETradeApiDbContext context) : base(context)
    {
    }
}