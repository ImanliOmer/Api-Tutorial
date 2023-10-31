using AutoMapper;
using Business.DTOs.Admin.Auth.Request;
using Business.DTOs.Admin.Auth.Response;
using Business.DTOs.Admin.Common;
using Business.Exceptions;
using Business.Services.Admin.Abstract;
using Business.Validators.Auth;
using Common.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Admin.Concrete
{
	public class AuthService : IAuthService
	{
		private readonly UserManager<User> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly IMapper _mapper;
		private readonly IConfiguration _configuration;

		public AuthService(UserManager<User> userManager,
							RoleManager<IdentityRole> roleManager,
							IMapper mapper,
							IConfiguration configuration)
		{
			_userManager = userManager;
			_roleManager = roleManager;
			_mapper = mapper;
			_configuration = configuration;
		}

		public async Task<Response> RegisterAsync(AuthRegisterDTO model)
		{
			var result = await new AuthRegisterDTOValidator().ValidateAsync(model);
			if (!result.IsValid)
				throw new ValidationException(result.Errors);

			var user = await _userManager.FindByNameAsync(model.Email);
			if (user is not null)
				throw new ValidationException("User under this email exists");

			user = _mapper.Map<User>(model);

			var registerResult = await _userManager.CreateAsync(user, model.Password);
			if (!registerResult.Succeeded)
				throw new ValidationException(registerResult.Errors);

			var role = await _roleManager.FindByNameAsync("User");
			if (role is null)
				throw new NotFoundException("Role was not foun");

			var roleResult = await _userManager.AddToRoleAsync(user, role.Name);
			if(!roleResult.Succeeded)
				throw new ValidationException(roleResult.Errors);

			return new Response
			{
				Message = "User successfully created"
			};
		}

		public async Task<Response<AuthLoginResponseDTO>> LoginAsync(AuthLoginDTO model)
		{
			var result = await new AuthLoginDTOValidator().ValidateAsync(model);
			if (!result.IsValid)
				throw new ValidationException(result.Errors);

			var user = await _userManager.FindByNameAsync(model.Email);
			if (user is null)
				throw new UnauthorizedException("Email or password is not correct");

			var loginSuccess = await _userManager.CheckPasswordAsync(user, model.Password);
			if (!loginSuccess)
				throw new UnauthorizedException("Email or password is not correct");

			var claims = new List<Claim>()
			{
				new Claim(ClaimTypes.NameIdentifier, user.Id),
				new Claim(ClaimTypes.Name, user.UserName),
				new Claim(ClaimTypes.Email, user.Email),
			};

			var roles = await _userManager.GetRolesAsync(user);
			foreach (var role in roles)
				claims.Add(new Claim(ClaimTypes.Role, role));

			var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

			var token = new JwtSecurityToken(
				issuer: _configuration["JWT:Issuer"],
				audience: _configuration["JWT:Audience"],
				expires: DateTime.Now.AddHours(3),
				claims: claims,
				signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
				);

			return new Response<AuthLoginResponseDTO>
			{
				Data = new AuthLoginResponseDTO
				{
					Token = new JwtSecurityTokenHandler().WriteToken(token)
				}
			};
		}

	}
}
