using Business.DTOs.Admin.Role.Request;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validators.Role
{
	public class RoleCreateDTOValidator :AbstractValidator<RoleCreateDTO>
	{
		public RoleCreateDTOValidator()
		{
			RuleFor(x => x.Name)
				.NotEmpty()
				.WithMessage("Name is required");

		}
	}
}
