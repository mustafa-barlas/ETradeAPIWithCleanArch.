namespace ETradeAPI.Application.Abstractions.Token;
using P = ETradeAPI.Application.DTOs;

public interface ITokenHandler
{
    P.Token CreateAccessToken(int minute);
}