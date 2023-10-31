using Common.Constants.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs.Admin.Product.Request
{
	public class ProductUpdateDTO
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }
		public int Quantity { get; set; }
		public string Photo { get; set; }
		public ProductType Type { get; set; }
	}
}
