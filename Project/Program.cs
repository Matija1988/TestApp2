using Microsoft.EntityFrameworkCore;
using ProjectService.Controllers;
using ProjectService.Data;
using ProjectService.Mappers;
using ProjectService.Model;
using ProjectService.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(ADBC => 
        ADBC.UseSqlServer(builder.Configuration.GetConnectionString(name: "ApplicationContext")));

builder.Services.AddScoped<IMapping, MapperConfiguration>();
builder.Services.AddScoped<IVehicleService<VehicleMake, VehicleMakeDTORead, VehicleMakeDTOInsert>, VehicleMakeService>();
builder.Services.AddScoped<IVehicleService<VehicleModel, VehicleModelDTORead, VehicleModelDTOInsert>, VehicleModelService>();

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

app.Run();
