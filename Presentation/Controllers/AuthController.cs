using Business.DTOs.Admin.Auth.Request;
using Business.DTOs.Admin.Auth.Response;
using Business.DTOs.Admin.Common;
using Business.Services.Admin.Abstract;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IAuthService _authService;

		public AuthController(IAuthService authService)
        {
			_authService = authService;
		}

		#region Documentation
		/// <summary>
		/// User registration
		/// </summary>
		/// <param name="model"></param>
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response))]
		#endregion
		[HttpPost("register")]
		public async Task<Response> RegisterAsync(AuthRegisterDTO model)
		{
			return await _authService.RegisterAsync(model);
		}

		#region Documentation
		/// <summary>
		/// User Login
		/// </summary>
		/// <param name="model"></param>
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<AuthLoginResponseDTO>))]
		[ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Response))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response))]
		#endregion
		[HttpPost("login")]
		public async Task<Response<AuthLoginResponseDTO>> LoginAsync(AuthLoginDTO model)
		{
			return await _authService.LoginAsync(model);
		}
    }
}
