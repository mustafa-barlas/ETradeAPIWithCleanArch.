using ETradeAPI.Application.Repositories.CustomerRepository;
using ETradeAPI.Domain.Entities;
using ETradeAPI.Persistence.Contexts;

namespace ETradeAPI.Persistence.Repositories.CustomerRepository;

public class CustomerWriteRepository : WriteRepository<Customer>, ICustomerWriteRepository
{
    public CustomerWriteRepository(ETradeApiDbContext context) : base(context)
    {
    }
}