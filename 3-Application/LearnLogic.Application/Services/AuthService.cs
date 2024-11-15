using LearnLogic.Application.Commands;
using AutoMapper;
using LearnLogic.Domain.DTO;
using LearnLogic.Domain.Interfaces.Application;
using LearnLogic.Domain.ViewModels;
using FluentValidation.Results;
using LearnLogic.Infra.CrossCutting.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NetDevPack.Mediator;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LearnLogic.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _mediator;
        private readonly IConfiguration _configuration;

        public AuthService(IConfiguration configuration, IMapper mapper, IMediatorHandler mediator)
        {
            this._mapper = mapper;
            this._mediator = mediator;
            this._configuration = configuration;
        }

        public async Task<ValidationResult> RegisterUser(UserViewModel vm)
        {
            var cmd = _mapper.Map<RegisterNewUserCommand>(vm);
            return await this._mediator.SendCommand(cmd);
        }

        public async Task<ValidationResult> UpdateUser(UserViewModel vm)
        {
            var cmd = _mapper.Map<UpdateUserCommand>(vm);
            return await this._mediator.SendCommand(cmd);
        }

        public string GenerateToken(UserDTO user)
        {
            int hours = _configuration.GetValue<int>("JwtSettings:Expiration");
            var expires = DateTime.UtcNow.AddHours(hours);
            string issuer = _configuration.GetValue<string>("JwtSettings:Issuer");
            string audience = _configuration.GetValue<string>("JwtSettings:Audience");
            string secretKey = _configuration.GetValue<string>("JwtSettings:SecretKey");

            var claims = GenerateClims(user);

            byte[] signingKeyBytes = Encoding.UTF8.GetBytes(secretKey);
            var secKey = new SymmetricSecurityKey(signingKeyBytes);
            var signingCredentials = new SigningCredentials(secKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: expires,
                signingCredentials: signingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private static List<Claim> GenerateClims(UserDTO user)
        {
            var claims = new List<Claim>
            {
                new Claim(CustomClaimTypes.UserId, user.Id.ToString()),
                new Claim(CustomClaimTypes.UserName, $"{user.FirstName} {user.LastName}"),
                new Claim(CustomClaimTypes.IsFirstAccess, user.FirstAccess.ToString().ToLower()),
                new Claim(CustomClaimTypes.Email, user.Email.ToString().ToLower()),
                new Claim(CustomClaimTypes.Punctuation, user.Punctuation.ToString()),
                new Claim(CustomClaimTypes.Role, ((int)user.Type).ToString()),
                new Claim(CustomClaimTypes.Experience, ((int)user.ExperienceLevel).ToString())
            };

            return claims;
        }
    }
}
