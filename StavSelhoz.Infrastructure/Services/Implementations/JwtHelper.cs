﻿using StavSelhoz.Infrastructure.Services.Interfaces;
using StavSelhoz.Domain.Commons.Response;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Newtonsoft.Json;

namespace StavSelhoz.Infrastructure.Services.Implementations;

public class JwtHelper : IJwtHelper
{
    public JwtResponse CreateJwtAsync(int userId)
    {
        var expires = DateTime.UtcNow.AddHours(720);
        var jwt = new JwtSecurityToken(
            issuer: "SERVER",
            expires: expires,
            claims: new Claim[]
            {
                new ("userId", userId.ToString())
            },
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes("sdjfhjjkhjkhbh32748g83r3278y8r73h287rbn8743y87hf487h843fh437rf3948hf934h93nbn8b3c48g9812")), SecurityAlgorithms.HmacSha256)
        );

        var access = new JwtSecurityTokenHandler().WriteToken(jwt);
        var refresh = CreateRefresh();
        return new JwtResponse { AccessToken = access, RefreshToken = refresh, Expires = expires };
    }

    

    private string CreateRefresh()
    {
        var randomBytes = new byte[64];
        var token = Convert.ToBase64String(randomBytes);
        return token;
    }

    public async Task<int> DecodJwt(string accessToken)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        try
        {
            var jwtToken = tokenHandler.ReadJwtToken(accessToken);

            var claims = jwtToken.Payload;
            foreach (var claim in claims)
            {
                if (claim.Key == "userId") return Convert.ToInt32(claim.Value);
            }

            throw new InvalidOperationException("UserId not found in the token");
        }
        catch (Exception ex)
        {
            throw new Exception($"Error decoding JWT: {ex.Message}", ex);
        }
    }

}
