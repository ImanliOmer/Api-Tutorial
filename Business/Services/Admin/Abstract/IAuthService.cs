using Business.DTOs.Admin.Auth.Request;
using Business.DTOs.Admin.Auth.Response;
using Business.DTOs.Admin.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Admin.Abstract
{
	public interface IAuthService
	{
		Task<Response> RegisterAsync(AuthRegisterDTO model);
		Task<Response<AuthLoginResponseDTO>> LoginAsync(AuthLoginDTO model);
	}
}
