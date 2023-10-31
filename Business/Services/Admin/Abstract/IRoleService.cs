using Business.DTOs.Admin.Common;
using Business.DTOs.Admin.Role.Request;
using Business.DTOs.Admin.Role.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Admin.Abstract
{
	public interface IRoleService
	{
		Task<Response<List<RoleDTO>>> GetAllAsync();
		Task<Response> CreateAsync(RoleCreateDTO model);
	}
}
