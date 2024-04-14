using ETradeAPI.Application.ViewModels.Basket;
using ETradeAPI.Domain.Entities;

namespace ETradeAPI.Application.Abstractions.Services;

public interface IBasketService
{
    public Task<List<BasketItem>> GetBasketItemsAsync();

    public Task AddItemToBasketAsync(VM_Create_BasketItem basketItem);

    public Task UpdateQuantityAsync(VM_Update_BasketItem basketItem);

    public Task RemoveBasketItemAsync(string basketItemId);
}