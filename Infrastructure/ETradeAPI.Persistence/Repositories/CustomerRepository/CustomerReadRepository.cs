using ETradeAPI.Application.Repositories.CustomerRepository;
using ETradeAPI.Domain.Entities;
using ETradeAPI.Persistence.Contexts;

namespace ETradeAPI.Persistence.Repositories.CustomerRepository;

public class CustomerReadRepository : ReadRepository<Customer>, ICustomerReadRepository
{
    public CustomerReadRepository(ETradeAPIDbContext context) : base(context)
    {
    }
}