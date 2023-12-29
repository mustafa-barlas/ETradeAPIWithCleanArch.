using ETradeAPI.Application.Abstractions;
using ETradeAPI.Domain.Entities;

namespace ETradeAPI.Persistence.Concrete;

public class ProductService : IProductService
{
    public List<Product> GetProducts()
     => new()
     {
         new ()
         {
             Id = Guid.NewGuid(),
             CreatedDate = DateTime.Now,
             Name = "Laptop1",
             Price = 1500,
             Stock = 12
         },
         new ()
         {
             Id = Guid.NewGuid(),
             CreatedDate = DateTime.Now,
             Name = "Laptop2",
             Price = 2000,
             Stock = 12
         },
         new ()
         {
             Id = Guid.NewGuid(),
             CreatedDate = DateTime.Now,
             Name = "Laptop3",
             Price = 2500,
             Stock = 12
         },
         new ()
         {
             Id = Guid.NewGuid(),
             CreatedDate = DateTime.Now,
             Name = "Laptop4",
             Price = 3000,
             Stock = 12
         },
         new ()
         {
             Id = Guid.NewGuid(),
             CreatedDate = DateTime.Now,
             Name = "Laptop5",
             Price = 3500,
             Stock = 12
         },

     };
}