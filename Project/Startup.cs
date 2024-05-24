using Ninject;
using Microsoft.Extensions.DependencyInjection;

namespace ProjectService
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        //public void ConfigureServices(IServiceCollection services)
        //{
        //    services.AddControllers();
        //}

        //public void Configure(IApplicationBuilder app, IWebHostEnvironment env) 
        //{ 
        //    if(env.IsDevelopment())
        //    {
        //        app.UseDeveloperExceptionPage();
        //        app.UseSwagger();
        //        app.UseSwaggerUI();
        //    }

        //    app.UseHttpsRedirection();
        //    app.UseRouting();
        //    app.UseAuthorization();

        //    app.UseEndpoints(endpoints =>
        //    {
        //        endpoints.MapControllers();
        //    });
        
        //}

        public void ConfigureContainer(IKernel kernel)
        {
            kernel.Load(new NinjectController());
        }
    }
}
