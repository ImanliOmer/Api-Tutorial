using AutoMapper;
using Business.DTOs.Admin.Auth.Request;
using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.MappingProfiles.Admin
{
	public class AuthMappingProfile : Profile
	{
        public AuthMappingProfile()
        {
            CreateMap<AuthRegisterDTO, User>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(scr => scr.Email));
        }
    }
}
