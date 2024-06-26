﻿using System.Text.Json;
using ETradeAPI.Application.Abstractions.Services;
using ETradeAPI.Application.Repositories.ProductRepository;
using ETradeAPI.Domain.Entities;

namespace ETradeAPI.Persistence.Services;

public class ProductService : IProductService
{
    private readonly IProductReadRepository _productReadRepository;
    private readonly IProductWriteRepository _productWriteRepository;
    private readonly IQRCodeService _qrCodeService;

    public ProductService(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository, IQRCodeService qrCodeService)
    {
        _productReadRepository = productReadRepository;
        _productWriteRepository = productWriteRepository;
        _qrCodeService = qrCodeService;
    }

    public async Task<byte[]> QrCodeToProductAsync(string productId)
    {
        Product product = await _productReadRepository.GetByIdAsync(productId);
        if (product == null) throw new Exception("Product Not Found");

        var plainObject = new
        {
            product.Id,
            product.Name,
            product.Price,
            product.Stock,
            product.CreatedDate,
            product.UpdatedDate
        };

        string plainText = JsonSerializer.Serialize(plainObject);

        return _qrCodeService.GenerateQRCode(plainText);
    }

    public async Task StockUpdateToProductAsync(string productId, int stock)
    {
        Product product = await _productReadRepository.GetByIdAsync(productId);
        if (product == null) throw new Exception("Product Not Found");


        product.Stock = stock;
        await _productWriteRepository.SaveAsync();
    }
}