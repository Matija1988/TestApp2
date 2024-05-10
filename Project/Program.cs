using Microsoft.EntityFrameworkCore;
using ProjectService.Data;
using ProjectService.Mappers;
using ProjectService.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(ADBC => 
        ADBC.UseSqlServer(builder.Configuration.GetConnectionString(name: "ApplicationContext")));

builder.Services.AddScoped<IVehicleMakeService, VehicleMakeService>();
builder.Services.AddScoped<IMapping, MapperConfiguration>();
builder.Services.AddScoped<IVehicleModelService, VehicleModelService>();

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
