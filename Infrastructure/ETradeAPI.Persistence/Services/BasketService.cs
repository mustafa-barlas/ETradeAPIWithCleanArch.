using ETradeAPI.Application.Abstractions.Services;
using ETradeAPI.Application.Repositories.BasketItemRepository;
using ETradeAPI.Application.Repositories.BasketRepository;
using ETradeAPI.Application.Repositories.OrderRepository;
using ETradeAPI.Application.ViewModels.Basket;
using ETradeAPI.Domain.Entities;
using ETradeAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ETradeAPI.Persistence.Services;

public class BasketService : IBasketService
{
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly UserManager<AppUser> _userManager;
    private readonly IOrderReadRepository _orderReadRepository;
    private readonly IBasketWriteRepository _basketWriteRepository;
    private readonly IBasketItemWriteRepository _basketItemWriteRepository;
    private readonly IBasketItemReadRepository _basketItemReadRepository;
    private readonly IBasketReadRepository _basketReadRepository;

    public BasketService(IHttpContextAccessor contextAccessor, UserManager<AppUser> userManager, IOrderReadRepository orderReadRepository, IBasketWriteRepository basketWriteRepository, IBasketItemWriteRepository basketItemWriteRepository, IBasketItemReadRepository basketItemReadRepository, IBasketReadRepository basketReadRepository)
    {
        _contextAccessor = contextAccessor;
        _userManager = userManager;
        _orderReadRepository = orderReadRepository;
        _basketWriteRepository = basketWriteRepository;
        _basketItemWriteRepository = basketItemWriteRepository;
        _basketItemReadRepository = basketItemReadRepository;
        _basketReadRepository = basketReadRepository;
    }

    private async Task<Basket?> ContextUser()
    {
        var username = _contextAccessor.HttpContext?.User.Identity?.Name;
        if (!string.IsNullOrEmpty(username))
        {
            AppUser? user = await _userManager.Users
                .Include(x => x.Baskets)
                .FirstOrDefaultAsync(x => x.UserName.Equals(username));

            var query = from basket in user?.Baskets
                        join order in _orderReadRepository.Table
                            on basket.Id equals order.Id into BasketOrders
                        from order in BasketOrders.DefaultIfEmpty()
                        select new
                        {
                            Basket = basket,
                            Order = order
                        };

            Basket? targetBasket = null;

            if (query.Any(x => x.Order is null))
                targetBasket = query.FirstOrDefault(x => x.Order is null)?.Basket;

            else
            {
                targetBasket = new();
                user.Baskets.Add(targetBasket);
            }

            await _basketWriteRepository.SaveAsync();
            return targetBasket;

        };

        throw new Exception("Beklenmeyen bir hata ile karşılaşıldı");

    }


    public async Task<List<BasketItem>> GetBasketItemsAsync()
    {
        Basket? basket = await ContextUser();
        Basket? result = await _basketReadRepository.Table
             .Include(x => x.BasketItems)
             .ThenInclude(x => x.Product)
             .FirstOrDefaultAsync(x => x.Id.Equals(basket.Id));

        return result?.BasketItems.ToList();
    }

    public async Task AddItemToBasketAsync(VM_Create_BasketItem basketItem)
    {
        Basket? basket = await ContextUser();
        if (basket != null)
        {
            var _basketItem = await _basketItemReadRepository.GetSingleAsync(x =>
                 x.BasketId.Equals(basket.Id) && x.ProductId.Equals(Guid.Parse(basketItem.ProductId)));

            if (_basketItem != null)
                _basketItem.Quantity++;
            else
                await _basketItemWriteRepository.AddAsync(new()
                {
                    BasketId = basket.Id,
                    ProductId = Guid.Parse(basketItem.ProductId),
                    Quantity = basketItem.Quantity,
                });

            await _basketItemWriteRepository.SaveAsync();
        }
    }

    public async Task UpdateQuantityAsync(VM_Update_BasketItem basketItem)
    {
        BasketItem? _basketItem = await _basketItemReadRepository.GetByIdAsync(basketItem.BasketItemId);

        if (_basketItem is not null)
        {
            _basketItem.Quantity = basketItem.Quantity;
            await _basketItemWriteRepository.SaveAsync();
        }
    }

    public async Task RemoveBasketItemAsync(string basketItemId)
    {
        BasketItem? basketItem = await _basketItemReadRepository.GetByIdAsync(basketItemId);

        if (basketItem is not null)
        {
            _basketItemWriteRepository.Remove(basketItem);
            await _basketItemWriteRepository.SaveAsync();
        }

    }
}