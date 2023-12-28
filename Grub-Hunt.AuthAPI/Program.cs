using Grub_Hunt.AuthAPI.Data;
using Grub_Hunt.AuthAPI.Implementations;
using Grub_Hunt.AuthAPI.Interfaces;
using Grub_Hunt.AuthAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.Configure<JWTOptions>(builder.Configuration.GetSection("APISettings:JWTOptions"));

builder.Services
    .AddIdentity<ApplicationUser, IdentityRole>(options => 
    {
        options.User.RequireUniqueEmail = true;
    })
    .AddEntityFrameworkStores<AppDBContext>()
    .AddDefaultTokenProviders();

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IJWTTokenGenerator, JWTTokenGenerator>();

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
