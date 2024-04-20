﻿using ETradeAPI.Application.Abstractions.Services.Authentications;

namespace ETradeAPI.Application.Abstractions.Services;

public interface IAuthService : IExternalAuthentication, IInternalAuthentication
{
    Task PasswordResetAsync(string mail);
    Task<bool> VerifyResetTokenAsync(string resetToken, string userId);
}