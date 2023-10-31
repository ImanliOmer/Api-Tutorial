using Business.DTOs.Admin.Common;
using Business.DTOs.Admin.Product.Request;
using Business.DTOs.Admin.Product.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Admin.Abstract
{
	public interface IProductService
	{
		Task<Response<List<ProductDTO>>> GetAllAsync();
		Task<Response<ProductDTO>> GetAsync(int id);
		Task<Response> CreateAsync(ProductCreateDTO model);
		Task<Response> UpdateAsync(int id , ProductUpdateDTO model);
		Task<Response> DeleteAsync(int id);
	}
}
