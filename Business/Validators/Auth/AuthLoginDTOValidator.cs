using Business.DTOs.Admin.Auth.Request;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validators.Auth
{
	public class AuthLoginDTOValidator : AbstractValidator<AuthLoginDTO>
	{
        public AuthLoginDTOValidator()
        {
			RuleFor(x => x.Email)
				.NotEmpty()
				.WithMessage("Email is required");

			RuleFor(x => x.Email)
				.EmailAddress()
				.WithMessage("Email address is not in correct format");

			RuleFor(x => x.Password.Length)
				.NotEmpty()
				.WithMessage("Password is required");
		}
    }
}
