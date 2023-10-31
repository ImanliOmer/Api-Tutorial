using Common.Constants.Products;
using Common.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities
{
	public class Product : BaseEntity
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }
		public int Quantity { get; set; }
		public string Photo { get; set; }
		public ProductType Type { get; set; }
	}
}
