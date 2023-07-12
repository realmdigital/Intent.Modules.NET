﻿using System;
using System.Threading.Tasks;
using EntityFrameworkCore.CosmosDb.TestApplication.Application.Common.Interfaces;

namespace EntityFrameworkCore.CosmosDb.TestApplication.Infrastructure.Services;

public class DummyCurrentUserService : ICurrentUserService
{
    public string? UserId { get; set; } = Guid.NewGuid().ToString();
    public string? UserName { get; set; } = "Test User";
    
    public Task<bool> IsInRoleAsync(string role)
    {
        return Task.FromResult(true);
    }

    public Task<bool> AuthorizeAsync(string policy)
    {
        return Task.FromResult(true);
    }
}