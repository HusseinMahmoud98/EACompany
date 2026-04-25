using AutoMapper;
using Company.BLL.Services.Interfaces.IAuth;
using Company.DAL.Entities;
using Company.Shared;
using Company.Shared.Dtos.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Services.Classes.Auth
{
    public class AuthManager
        (UserManager<AppUser> _userManager, IOptions<JwtOptions> _options) : IAuthManager
    {
        public async Task<UserDto> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user is null) throw new Exception("User not found");

            var confirmPassword = await _userManager.CheckPasswordAsync(user, loginDto.Password);

            if (!confirmPassword) throw new Exception($"wrong email or password");

            return new UserDto()
            {
                UserName = user.UserName,
                Email = user.Email,
                Token = await GenerateTokenAsync(user)
            };


        }

        public async Task<UserDto> RegisterAsync(RegisterDto registerDto)
        {
            if (registerDto.Password != registerDto.ConfirmPassword)
                throw new Exception("Password not match user");

            var user = new AppUser()
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber,
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception($"Couldn't create user: {errors}");
            }

            return new UserDto()
            {
                UserName = user.UserName,
                Email = user.Email,
                Token = await GenerateTokenAsync(user)
            };

        }


        private async Task<string> GenerateTokenAsync(AppUser user)
        {
            //Token:
            // 1. Header (type, Algorithm)
            // 2. Payload (Claims) "data of user"
            // 3. Signature (key)

            var authClaims = new List<Claim>()
            {
                 new Claim(ClaimTypes.GivenName, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.MobilePhone, user.PhoneNumber)
            };

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var JwtOptions = _options.Value;


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtOptions.SecurityKey));

            var token = new JwtSecurityToken(
                issuer: JwtOptions.Issuer,
                audience: JwtOptions.Audience,
                claims: authClaims,
                expires: DateTime.Now.AddDays(JwtOptions.DurationInDays),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }


     
    }
