﻿using ETradeAPI.Application.DTOs.ConfigurationDto;

namespace ETradeAPI.Application.Abstractions.Services.Configurations;

public interface IApplicationService
{
    List<Menu> GetAuthorizeDefinitionEndpoints(Type type);
}