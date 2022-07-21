using Assist.July._2022.BE2.Application.Helper;
using Assist.July._2022.BE2.Application.Interfaces;
using Assist.July._2022.BE2.Application.MiddleWare;
using Assist.July._2022.BE2.Application.Services;
using Assist.July._2022.BE2.Application.Utils;
using Assist.July._2022.BE2.Application.QuartzJobs;
using Assist.July._2022.BE2.Domain.Entities;
using Assist.July._2022.BE2.Infrastructure.Contexts;
using Assist.July._2022.BE2.Infrastructure.Interfaces;
using Assist.July._2022.BE2.Infrastructure.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Quartz;

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
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
builder.Services.AddTransient<IMailService,MailService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddScoped<IFavoriteRepository, FavoriteRepository>();
builder.Services.AddScoped<IFavoriteService, FavoriteService>();
builder.Services.AddScoped<IJwtUtils, JwtUtils>();
builder.Services.AddTransient<IListingService, ListingService>();
builder.Services.AddTransient<IListingRepository, ListingRepository>();

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

builder.Services.AddQuartz(q =>
{
    q.UseMicrosoftDependencyInjectionJobFactory();

    var quartzAdminAlertJobKey = new JobKey("QuartzAdminAlertJobKey");
    q.AddJob<AdminAlertJob>(opts => opts.WithIdentity(quartzAdminAlertJobKey));
    q.AddTrigger(opts => opts
        .ForJob(quartzAdminAlertJobKey)
        .WithIdentity("QuartzAdminAlertJobKey-trigger")
        .WithSimpleSchedule(x => x
            .WithIntervalInHours(2)
            .RepeatForever()));

});
builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

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

app.UseMiddleware<JwtMiddleware>();

app.MapControllers();

app.Run();
