﻿using System.IdentityModel.Tokens.Jwt;
using System.Text;
using ETradeAPI.Application.Abstractions.Token;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using P = ETradeAPI.Application.DTOs;
// ReSharper disable CommentTypo


namespace ETradeAPI.Infrastructure.Services.Token;

public class TokenHandler : ITokenHandler
{
    private readonly IConfiguration _configuration;

    public TokenHandler(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public P.Token CreateAccessToken(int minute)
    {
        P.Token token = new();


        SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"])); // keyin simetrii alındı

        SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256); // şifrelenmiş kimlik oluşturuldu


        // token ayarları
        token.Expiration = DateTime.UtcNow.AddMinutes(minute);
        JwtSecurityToken securityToken = new(
            audience: _configuration["Token:Audience"],
            issuer: _configuration["Token:Issuer"],
            expires: token.Expiration,
            notBefore: DateTime.UtcNow,
            signingCredentials: signingCredentials
            );

        // token oluşturucu sınıfından bir örnek alalım
        JwtSecurityTokenHandler tokenHandler = new();
        token.AccessToken = tokenHandler.WriteToken(securityToken);
        return token;
    }
}