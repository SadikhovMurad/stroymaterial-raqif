using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.AutoMapper;
using Business.DependencyResolvers.Autofac;
using Core.Entity.Concrete;
using Core.Utilities.Security.Encyption;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using stroymaterial_raqif.Identity;
using stroymaterial_raqif.Identity.JWT;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        BearerFormat = "JWT",
        Description = "Please enter a valid token in the format **'Bearer {your_token}'**"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});


builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(builder =>
    {
        builder.RegisterModule(new AutofacBusinessModule());
    });

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddScoped<ITokenHelper,JWTHelper>();

builder.Services.AddDbContext<ModelDbContext>(options => options.UseSqlServer(

    @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=EvrostroyDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"

    ));

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ModelDbContext>()
    .AddDefaultTokenProviders();


var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<stroymaterial_raqif.Identity.JWT.TokenOptions>();
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["TokenOptions:Issuer"],
            ValidAudience = builder.Configuration["TokenOptions:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["TokenOptions:SecurityKey"]))
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Bearer", policy => policy.RequireAuthenticatedUser());
});







var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
