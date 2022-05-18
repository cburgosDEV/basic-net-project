using Application.Constants;
using Application.Contracts.Identity;
using Application.DTOs;
using Application.Features.Auth.Commands.Login;
using Application.Features.Auth.Commands.Register;
using Application.Features.Users.Queries.GetAll;
using Application.Features.Users.Queries.GetById;
using Application.Settings;
using Application.Wrappers;
using Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Identity.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JwtSettings _jwtSettings;

        public AuthService
            (
                UserManager<ApplicationUser> userManager, 
                SignInManager<ApplicationUser> signInManager,
                IOptions<JwtSettings> jwtSettings
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<PaginationResponse<List<UserDto>>> GetAll(GetAllQuery query)
        {
            var response = await _userManager.Users.Skip((query.PageNumber - 1) * query.PageSize).Take(query.PageSize).ToListAsync();
            var userDtoList = new List<UserDto>();

            foreach (var userModel in response)
            {
                userDtoList.Add(new UserDto
                {
                    UserName = userModel.UserName,
                    Email = userModel.Email,
                    FirstName = userModel.FirstName,
                    LastName = userModel.LastName,
                });
            }

            return new PaginationResponse<List<UserDto>>(userDtoList, query.PageNumber, query.PageSize);
        }

        public async Task<CustomResponse<UserDto>> GetById(GetByIdQuery query)
        {
            var response = await _userManager.FindByIdAsync(query.Id.ToString());

            if(response == null) throw new KeyNotFoundException($"User was not found {query.Id}");

            var userDto = new UserDto
            {
                FirstName = response.FirstName,
                Email = response.Email,
                LastName = response.LastName,   
                UserName = response.UserName,
            };

            return new CustomResponse<UserDto>(userDto);
        }

        public async Task<CustomResponse<AuthResponse>> Login(LoginAuthCommand command)
        {
            var user = await _userManager.FindByEmailAsync(command.Email);
            if (user == null)
            {
                throw new Exception($"Email does not exist");
            }

            var response = await _signInManager.PasswordSignInAsync(user.UserName, command.Password, false, lockoutOnFailure: false);
            if (!response.Succeeded)
            {
                throw new Exception($"Incorrect credentials");
            }

            var authResponse = new AuthResponse
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Token = new JwtSecurityTokenHandler().WriteToken(await GenerateToken(user))
            };

            return new CustomResponse<AuthResponse>(authResponse);
        }
        public async Task<CustomResponse<AuthResponse>> Register(RegisterAuthCommand command)
        {
            var existingUser = await _userManager.FindByNameAsync(command.UserName);

            if (existingUser != null)
            {
                throw new Exception($"Username already exists");
            }

            var existingEmail = await _userManager.FindByEmailAsync(command.Email);
            if (existingEmail != null)
            {
                throw new Exception($"Email already exists");
            }

            var user = new ApplicationUser()
            {
                Email = command.Email,
                FirstName = command.FirstName,
                LastName = command.LastName,
                UserName = command.UserName,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, command.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "CustomRole");

                var authResponse = new AuthResponse
                {
                    Email = user.Email,
                    Token = new JwtSecurityTokenHandler().WriteToken(await GenerateToken(user)),
                    Id = user.Id,
                    UserName = user.UserName,
                };
                return new CustomResponse<AuthResponse>(authResponse);
            }
            else
            {
                var authResponse = new AuthResponse();

                return new CustomResponse<AuthResponse>
                (
                    authResponse,
                    result.Errors.Select(x => x.Description).ToList(),
                    false,
                    "There were some validation errors"
                );
            }
        }
        private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
            {
                roleClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(CustomClaimTypeConstant.Uid, user.Id),
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            return new JwtSecurityToken
            (
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.Duration),
                signingCredentials: signingCredentials
            );
        }
    }
}
