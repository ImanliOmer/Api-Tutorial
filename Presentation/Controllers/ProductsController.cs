using Business.DTOs.Admin.Common;
using Business.DTOs.Admin.Product.Request;
using Business.DTOs.Admin.Product.Response;
using Business.Services.Admin.Abstract;
using Common.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize(Roles = "User",AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public class ProductsController : ControllerBase
	{
		private readonly IProductService _productService;

		public ProductsController(IProductService productService)
		{
			_productService = productService;
		}

		#region Documentation
		/// <summary>
		/// Returns list of all products
		/// </summary>
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<List<ProductDTO>>))]
		#endregion
		[HttpGet]
		public async Task<Response<List<ProductDTO>>> GetAllAsync()
		{
			return await _productService.GetAllAsync();
		}

		#region Documentation
		/// <summary>
		///  Returns product by id parameter
		/// </summary>
		/// <param name="id"></param>
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<ProductDTO>))]
		[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response))]
		#endregion
		[HttpGet("{id}")]
		public async Task<Response<ProductDTO>> GetAsync(int id)
		{
			return await _productService.GetAsync(id);
		}

		#region Documentation
		/// <summary>
		/// Creates product 
		/// </summary>
		/// <param name="model"></param>
		/// <remarks>
		/// <ul>
		/// <li><b>Type:</b> 0 - Standart, 1 - New, 2 - Sold, 3 - Sale </li>
		/// </ul>
		/// </remarks>
		[ProducesResponseType(StatusCodes.Status200OK, Type =  typeof(Response))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type =  typeof(Response))]
		#endregion
		[HttpPost]
		public async Task<Response> CreateAsync(ProductCreateDTO model)
		{
			return await _productService.CreateAsync(model);
		}

		#region Documentation
		/// <summary>
		/// Update Product 
		/// </summary> 
		/// <param name="id"></param>
		/// <param name="model"></param>
		/// <remarks>
		/// <ul>
		/// <li><b>Type:</b> 0 - Standart, 1 - New, 2 - Sold, 3 - Sale </li>
		/// </ul>
		/// </remarks>
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
		[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response))]
		#endregion
		[HttpPut("{id}")]
		public async Task<Response> UpdateAsync(int id, ProductUpdateDTO model)
		{
			return await _productService.UpdateAsync(id, model);
		}

		#region Documentation
		/// <summary>
		/// Deletion of Product
		/// </summary>
		/// <param name="id"></param>
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
		[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response))]
		#endregion
		[HttpDelete("{id}")]
		public async Task<Response> DeleteAsync(int id)
		{
			return await _productService.DeleteAsync(id);
		}
	}
}
