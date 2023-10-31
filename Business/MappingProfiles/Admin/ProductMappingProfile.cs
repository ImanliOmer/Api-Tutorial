using AutoMapper;
using Business.DTOs.Admin.Product.Request;
using Business.DTOs.Admin.Product.Response;
using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.MappingProfiles.Admin
{
	public class ProductMappingProfile : Profile
	{
		public ProductMappingProfile()
		{
			CreateMap<ProductCreateDTO, Product>();
				//.ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title));  To map to andouther property in destiantion

			CreateMap<Product, ProductDTO>();

			CreateMap<ProductUpdateDTO, Product>();
				//.ForMember(dest => dest.Title, opt => opt.Ignore());  To not map a specific property fom src to dest
		}
	}
}
