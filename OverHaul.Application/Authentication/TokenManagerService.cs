using Overhaul.Application.Utilitty;
using Overhaul.Domain.Common;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Overhaul.Application.Public;
using Overhaul.Domain.Entities;


namespace Overhaul.Application.Authentication
{
    public class TokenManagerService : ITokenManagerService
    {
        //private readonly IStateManagerService _stateManagerService;
        private readonly ICryptoService _cryptoService;
       // private readonly ICurrentContextService _currentContextService;
        //private readonly IErrorFactory _errorFactory;
        public TokenManagerService(
          //IStateManagerService stateManagerService,
          ICryptoService cryptoService
          //ICurrentContextService currentContextService,
          //IErrorFactory errorFactory
          )
        {
            //_stateManagerService = stateManagerService;
            _cryptoService = cryptoService;
            //_currentContextService = currentContextService;
           // _errorFactory = errorFactory;
        }
        public async Task<bool> CheckSecurityStamp(string userId, string hashValue)
        {
            //var value = await _stateManagerService.GetState<string>($"user:{userId}");
            //var tokenStamp = Encoding.ASCII.GetBytes(hashValue);
            //var mainStamp = Encoding.ASCII.GetBytes(value);
            //if (CheckStamp(tokenStamp, mainStamp))
                return true;
            //else
            //    return false;
        }

        public async Task<bool> DeactivateToken(string token, DateTime expireDate)
        {
            //var date = DateTime.Now;
            //var expirationTimeSpan = expireDate.Subtract(date);
            //return await _stateManagerService.SetState($"tokens:{token}", "deactivated", expirationTimeSpan);
            return true;
        }

        public string GenerateSecurityStamp(AppUser user)
        {
            var securityStamp = user.SecurityStamp;
            return _cryptoService.Hash(securityStamp);
        }

        public async Task<string> GenerateToken(TokenSetting tokenSetting, params Claim[] claims)
        {
            var secretKey = Encoding.UTF8.GetBytes(tokenSetting.SecretKey);
            var signingCredentials = new SigningCredentials(key: new SymmetricSecurityKey(secretKey),
                                                            algorithm: SecurityAlgorithms.HmacSha256Signature);
            var encryptionkey = Encoding.UTF8.GetBytes(tokenSetting.EncryptKey);
            var encryptingCredentials = new EncryptingCredentials(key: new SymmetricSecurityKey(encryptionkey),
                                                                  alg: SecurityAlgorithms.Aes256KW,
                                                                  enc: SecurityAlgorithms.Aes256CbcHmacSha512);
            var descriptor = new SecurityTokenDescriptor
            {
                Issuer = tokenSetting.Issuer,
                Audience = tokenSetting.Audience,
                IssuedAt = DateTime.Now,
                Expires = DateTime.Now.AddMinutes(tokenSetting.ExpirationMinutes),
                SigningCredentials = signingCredentials,
                EncryptingCredentials = encryptingCredentials,
                Subject = new ClaimsIdentity(claims)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(descriptor);
            var jwt = tokenHandler.WriteToken(securityToken);
            return await Task.Run(() => jwt.ToString());
        }

        public async Task<bool> IsActiveToken()
        {
            //var hashToken = _cryptoService.Hash(_currentContextService.GetUserToken());
            //var value = await _stateManagerService.GetState<string>($"tokens:{hashToken}");
            //if (string.IsNullOrEmpty(value))
                return true;
            //else
            //    return false;
        }

        public async Task<bool> SetSecurityStamp(string userId, string securityStamp)
        {
            // return await _stateManagerService.SetState($"user:{userId}", securityStamp);
            return true;
        }

        public async Task<ClaimsPrincipal> ValidateToken(string token, TokenSetting tokenSetting)
        {
            var secretkey = Encoding.UTF8.GetBytes(tokenSetting.SecretKey);
            var encryptionkey = Encoding.UTF8.GetBytes(tokenSetting.EncryptKey);
            var validationParameters = new TokenValidationParameters
            {
                ClockSkew = TimeSpan.Zero,
                RequireSignedTokens = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(secretkey),
                RequireExpirationTime = true,
                ValidateLifetime = false,
                ValidateAudience = true,
                ValidAudience = tokenSetting.Audience,
                ValidateIssuer = true,
                ValidIssuer = tokenSetting.Issuer,
                TokenDecryptionKey = new SymmetricSecurityKey(encryptionkey),
                NameClaimType = ClaimTypes.NameIdentifier
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var claimsPrincipal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken != null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.Aes256KW))
            {
                //throw _errorFactory.InvalidToken();
                throw new Exception();
            }
            return await Task.Run(() => claimsPrincipal);
        }

        private static bool CheckStamp(byte[] sourceA, byte[] sourceB)
        {
            return sourceB.SequenceEqual(sourceA);
        }
    }
}
