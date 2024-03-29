﻿using System.Security.Claims;

namespace SkamBook.Core.Interfaces.Services
{
    public interface IJwtService
    {
        Task<string> GenerateJwtToken(string email);
    }
}