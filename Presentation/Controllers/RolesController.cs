using Business.DTOs.Admin.Common;
using Business.DTOs.Admin.Role.Request;
using Business.DTOs.Admin.Role.Response;
using Business.Services.Admin.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class RolesController : ControllerBase
	{
		private readonly IRoleService _roleService;

		public RolesController(IRoleService roleService)
		{
			_roleService = roleService;
		}

		#region Documentation
		/// <summary>
		/// Returns List of Roles
		/// </summary>
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<List<RoleDTO>>))]
		#endregion
		[HttpGet]
		public async Task<Response<List<RoleDTO>>> GetAllAsync()
		{
			return await _roleService.GetAllAsync();	
		}

		#region Documentation
		/// <summary>
		/// Create Role
		/// </summary>
		/// <param name="model"></param>
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response))]
		#endregion
		[HttpPost]
		public async Task<Response> CreateAsync(RoleCreateDTO model)
		{
			return await _roleService.CreateAsync(model);
		}
	}
}
