using Business.DTOs.Admin.Product.Request;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validators.Product
{
	public class ProductCreateDTOValidator : AbstractValidator<ProductCreateDTO>
	{
		public ProductCreateDTOValidator()
		{
			RuleFor(x => x.Title)
				.NotEmpty()
				.WithMessage("Title is Required");

			RuleFor(x => x.Title)
				.MinimumLength(10)
				.WithMessage("Title must be at least 10 Characters");

			RuleFor(x => x.Description)
				.NotEmpty()
				.WithMessage("Description is required");

			RuleFor(x => x.Description)
				.MaximumLength(500)
				.WithMessage("Description cannot be longer than 500 characters");

			RuleFor(x => x.Price)
				.NotEmpty()
				.WithMessage("Price is reqquired");

			RuleFor(x => x.Quantity)
				.GreaterThanOrEqualTo(0)
				.WithMessage("Quantity can't be lower than 0");

			RuleFor(x => x.Photo)
				 .Must(IsBase64String)
				 .WithMessage("File is not in right format");

			RuleFor(x => x.Type)
				.IsInEnum()
				.WithMessage("Type was not choosen right");
		}

		private static bool IsBase64String(string str)
		{
			byte[] output;
			try
			{
				output = Convert.FromBase64String(str);
				return true;
			}
			catch (Exception e)
			{
				return false;
			}
		}
	}
}
