using Business.MappingProfiles.Admin;
using Business.Services.Admin.Abstract;
using Business.Services.Admin.Concrete;
using Common.Entities;
using DataAccess.Contexts;
using DataAccess.Repositiories.Abstract;
using DataAccess.Repositiories.Concrete;
using DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Presentation.Middlewares;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
	options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Presentation.xml"));

	options.SwaggerDoc("v1", new OpenApiInfo
	{
		Title = "JWTToken_Auth_API",
		Version = "v1"
	});
	options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
	{
		Name = "Authorization",
		Type = SecuritySchemeType.ApiKey,
		Scheme = "Bearer",
		BearerFormat = "JWT",
		In = ParameterLocation.Header,
		Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
	});
	options.AddSecurityRequirement(new OpenApiSecurityRequirement {
		{
			new OpenApiSecurityScheme {
				Reference = new OpenApiReference {
					Type = ReferenceType.SecurityScheme,
						Id = "Bearer"
				}
			},
			new string[] {}
		}
	});
});

builder.Services.AddAuthentication(options => {
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
	.AddJwtBearer(options => {
		options.SaveToken = true;
		options.RequireHttpsMetadata = false;
		options.TokenValidationParameters = new TokenValidationParameters()
		{
			ValidateIssuer = true,
			ValidateAudience = true,
			ValidAudience = builder.Configuration["JWT:Issuer"],
			ValidIssuer = builder.Configuration["JWT:Audience"],
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
		};
	});

builder.Services.Configure<RouteOptions>(x =>
{
	x.LowercaseUrls = true;
});

builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("Default"), x => x.MigrationsAssembly("DataAccess")));
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
	options.Password.RequireNonAlphanumeric = false;
	options.Password.RequireDigit = false;
	options.Password.RequireLowercase = false;
	options.Password.RequireUppercase = false;
	options.Password.RequiredLength = 0;
	options.Password.RequiredUniqueChars = 0;
})
	.AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

#region Repositories
builder.Services.AddScoped<IProductRepository, ProductRepository>();
#endregion

#region Services
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IAuthService, AuthService>();
#endregion

builder.Services.AddAutoMapper(x =>
{
	x.AddProfile(new ProductMappingProfile());
	x.AddProfile(new RoleMappingProfile());
	x.AddProfile(new AuthMappingProfile());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseMiddleware<CustomExceptionMiddleware>();

app.Run();
