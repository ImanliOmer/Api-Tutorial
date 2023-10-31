using AutoMapper;
using Business.DTOs.Admin.Common;
using Business.DTOs.Admin.Product.Request;
using Business.DTOs.Admin.Product.Response;
using Business.Exceptions;
using Business.Services.Admin.Abstract;
using Business.Validators.Product;
using Common.Entities;
using DataAccess.Repositiories.Abstract;
using DataAccess.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Admin.Concrete
{
	public class ProductService : IProductService
	{
		private readonly IProductRepository _productRepository;
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;

		public ProductService(IProductRepository productRepository,
								IMapper mapper,
								IUnitOfWork unitOfWork)
        {
			_productRepository = productRepository;
			_mapper = mapper;
			_unitOfWork = unitOfWork;
		}

		public async Task<Response<List<ProductDTO>>> GetAllAsync()
		{
			var products = await _productRepository.GetAllAsync();

			return new Response<List<ProductDTO>>
			{
				Data = _mapper.Map<List<ProductDTO>>(products)
			};
		}

		public async Task<Response<ProductDTO>> GetAsync(int id)
		{
			var product = await _productRepository.GetAsync(id);
			if (product is null)
				throw new NotFoundException("Product is not found");

			return new Response<ProductDTO>
			{
				Data = _mapper.Map<ProductDTO>(product)
			};
		}

		public async Task<Response> CreateAsync(ProductCreateDTO model)
		{
			var result =  await new ProductCreateDTOValidator().ValidateAsync(model);
			if (!result.IsValid)
				throw new ValidationException(result.Errors);

			var product = _mapper.Map<Product>(model);
			await _productRepository.CreateAsync(product);
			await _unitOfWork.CommitAsync();

			return new Response
			{
				Message = "Product Successfully created"
			};
		}

		public async Task<Response> UpdateAsync(int id, ProductUpdateDTO model)
		{
			var result = await new ProductUpdateDTOValidator().ValidateAsync(model);
			if(!result.IsValid)
				throw new ValidationException(result.Errors);

			var product = await _productRepository.GetAsync(id);
			if (product is null)
				throw new NotFoundException("Product is not found");

			_mapper.Map(model, product);
			product.ModifiedAt = DateTime.Now;	

			_productRepository.Update(product);
			await _unitOfWork.CommitAsync();

			return new Response
			{
				Message = "Product successfully updated"
			};
		}

		public async Task<Response> DeleteAsync(int id)
		{
			var product = await _productRepository.GetAsync(id);
			if (product is null)
				throw new NotFoundException("Product not found");

			_productRepository.Delete(product);
			await _unitOfWork.CommitAsync();

			return new Response
			{
				Message = "Product successfully deleted"
			};
		}
	}
}
