//using Microsoft.EntityFrameworkCore;
//using ProjectService.Data;
//using ProjectService.Service;


//namespace ProjectMVC
//{
//    public class Startup
//    {
//        public IConfiguration Configuration { get; }

//        public Startup(IConfiguration configuration)
//        {
//            Configuration = configuration;
//        }

//        public void ConfigureServices(IServiceCollection services)
//        {
//            services.AddControllersWithViews();
//            services.AddDbContext<ApplicationDbContext>(options =>
//                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
//            services.AddAutoMapper(typeof(Startup));
//            services.AddScoped<IVehicleService<>, VehicleService>();
//        }

//        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
//        {
//            if (env.IsDevelopment())
//            {
//                app.UseDeveloperExceptionPage();
//            }
//            else
//            {
//                app.UseExceptionHandler("/Home/Error");
//                app.UseHsts();
//            }
//            app.UseHttpsRedirection();
//            app.UseStaticFiles();
//            app.UseRouting();
//            app.UseAuthorization();

//            app.UseEndpoints(endpoints =>
//            {

//                endpoints.MapControllerRoute(
//                    name: "vehicleMake",
//                    pattern: "VehicleMake/{action}/{id?}",
//                    defaults: new { controller = "VehicleMake", action = "Index" });

//                endpoints.MapControllerRoute(
//                    name: "vehicleModel",
//                    pattern: "VehicleModel/{action}/{id?}",
//                    defaults: new { controller = "VehicleModel", action = "Index" });

//                endpoints.MapControllerRoute(
//                    name: "default",
//                    pattern: "{controller=Home}/{action=Index}/{id?}");
//            });
//        }
//    }
//}
