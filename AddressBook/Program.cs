using BusinessLayer.Interface;
using BusinessLayer.Service;
using BusinessLayer.MappingProfiles;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Context;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AddressBookDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"));
});


builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();

//Dependency Injection for Repository Layer
builder.Services.AddScoped<IAddressBookRL, AddressBookRL>();
//Dependency Injection for Business Layer
builder.Services.AddScoped<IAddressBookBL, AddressBookBL>();

//Dependency Injection for AutoMapper
builder.Services.AddAutoMapper(typeof(AddressBookProfile));


var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
