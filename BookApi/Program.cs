using Application.FeaturesCQRS.Handlers;
using Application.IServices;
using Application.Validators;
using BookApi.Extenstions;
using BookApi.Middleware;
using Domain.Interfaces;
using FluentValidation;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("MyDb"))
);

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("redis");
    options.InstanceName = "bookapi_"; 
});



builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddMediatR(typeof(CreateBookHandler).Assembly);


builder.Services.AddValidatorsFromAssembly(typeof(CreateBookValidator).Assembly);

builder.Services.AddScoped<IBookRepository, BookRepository>();

builder.Services.AddScoped<IEmailService,EmailService>();

builder.Services.AddSingleton<ICacheService, RedisCacheService>();

builder.Services.AddOptionsJWT(builder.Configuration);

builder.Services.OptionsSwagger();


builder.AddServiceDefaults();







builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapDefaultEndpoints();

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


app.UseMiddleware<ErrorHandlingMiddleware>();


app.Run();
