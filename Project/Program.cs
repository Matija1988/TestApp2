
using Microsoft.EntityFrameworkCore;
using ProjectService;
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

//var hostBuilder = Host.CreateDefaultBuilder(args)
//    .UseServiceProviderFactory(new NinjectServiceProviderFactory())
//    .ConfigureWebHostDefaults(webBuilder =>
//    {
//        webBuilder.UseStartup<Startup>();
//    });



builder.Services.AddScoped<IMapping, MapperConfiguration>();

builder.Services.AddScoped
    <IVehicleService<VehicleMake,
                     VehicleMakeDTORead,
                     VehicleMakeDTOInsert,
                     VehicleMakeDTOReadWithoutID>,
                     VehicleMakeService>();

builder.Services.AddScoped
    <IVehicleService<VehicleModel,
                     VehicleModelDTORead,
                     VehicleModelDTOInsert,
                     VehicleModelDTOReadWithoutID>,
                     VehicleModelService>();

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
