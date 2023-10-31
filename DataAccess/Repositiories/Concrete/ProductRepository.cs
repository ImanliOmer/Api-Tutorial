using Common.Entities;
using DataAccess.Contexts;
using DataAccess.Repositiories.Abstract;
using DataAccess.Repositiories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositiories.Concrete
{
	public class ProductRepository : Repository<Product>, IProductRepository
	{
		public ProductRepository(AppDbContext context) : base(context) { }
	}
}
