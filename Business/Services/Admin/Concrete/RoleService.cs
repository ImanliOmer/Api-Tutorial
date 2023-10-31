using AutoMapper;
using Business.DTOs.Admin.Common;
using Business.DTOs.Admin.Role.Request;
using Business.DTOs.Admin.Role.Response;
using Business.Exceptions;
using Business.Services.Admin.Abstract;
using Business.Validators.Role;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Admin.Concrete
{
	public class RoleService : IRoleService
	{
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly IMapper _mapper;

		public RoleService(RoleManager<IdentityRole> roleManager,
							IMapper mapper)
		{
			_roleManager = roleManager;
			_mapper = mapper;
		}

		public async Task<Response<List<RoleDTO>>> GetAllAsync()
		{
			return new Response<List<RoleDTO>>
			{
				Data = _mapper.Map<List<RoleDTO>>(await _roleManager.Roles.ToListAsync())
			};
		}

		public async Task<Response> CreateAsync(RoleCreateDTO model)
		{
			var result = await new RoleCreateDTOValidator().ValidateAsync(model);
			if (!result.IsValid)
				throw new ValidationException(result.Errors);

			var role = await _roleManager.FindByNameAsync(model.Name);
			if (role is not null)
				throw new ValidationException("Role under this name already exists");

			var roleResult = await _roleManager.CreateAsync(_mapper.Map<IdentityRole>(model));
			if (!roleResult.Succeeded)
				throw new ValidationException(roleResult.Errors);

			return new Response
			{
				Message = "Role successfully created"
			};
		}
	}
}
