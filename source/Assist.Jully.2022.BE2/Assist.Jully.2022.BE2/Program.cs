using Assist.July._2022.BE2.Application.Helper;
using Assist.July._2022.BE2.Application.Interfaces;
using Assist.July._2022.BE2.Application.Services;
using Assist.July._2022.BE2.Domain.Entities;
using Assist.July._2022.BE2.Infrastructure.Contexts;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

var allowSpecificOrigins = "allowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);
var sqlConnectionBuilder = new SqlConnectionStringBuilder(
    builder
    .Configuration
    .GetConnectionString("SqlDBConnectionString"));
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "version 1", Version = "v1" });
    c.AddSecurityDefinition("bearer", new OpenApiSecurityScheme()
    {
        Name = "authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "bearer token"
    }); ;
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
        new OpenApiSecurityScheme
        {
            Reference=new OpenApiReference
            {
                Type=ReferenceType.SecurityScheme,
                Id="bearer"
            }

        },
        new string[]{}
        }
    }
    );
});
builder.Services.AddDbContext<ApplicationDbContext>(options => 
    options.UseSqlServer(sqlConnectionBuilder.ConnectionString));
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
builder.Services.AddTransient<IMailService,MailService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddCustomConfiguredAutoMapper();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: allowSpecificOrigins,
                      policy =>
                      {
                          policy
                          .WithOrigins("*")
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                      });
});

var app = builder.Build();
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseCors(allowSpecificOrigins);

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
