using ApiProject.Controllers;
using ApiProject.Data;
using ApiProject.Interfaces;
using ApiProject.Models;
using ApiProject.Models.Enum;
using ApiProject.Repositoryes;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);




var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddRouting(r => r.SuppressCheckForUnhandledSecurityMetadata = true);
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));
// Add services to the container.





builder.Services.AddControllers();


builder.Services.AddTransient<IRepository, Repository>();






builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();




var app = builder.Build();





// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapControllerRoute(
    name: "default",
    pattern: "api/{controller=Login}");

app.UseRouting();

app.UseHttpsRedirection();



app.MapControllers();

app.Run();
