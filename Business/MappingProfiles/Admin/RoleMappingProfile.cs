using AutoMapper;
using Business.DTOs.Admin.Role.Request;
using Business.DTOs.Admin.Role.Response;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.MappingProfiles.Admin
{
	public class RoleMappingProfile : Profile
	{
		public RoleMappingProfile()
		{
			CreateMap<RoleCreateDTO, IdentityRole>();
			CreateMap<IdentityRole, RoleDTO>();
		}
	}
}
