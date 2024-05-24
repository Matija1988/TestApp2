using Ninject;

namespace ProjectService
{
    public class NinjectServiceProviderFactory : IServiceProviderFactory<IKernel>
    {
        private readonly IKernel _kernel;

        public NinjectServiceProviderFactory() : this(new StandardKernel())
        {

        }

        public NinjectServiceProviderFactory(IKernel kernel)
        {
            _kernel = kernel;
        }


        public IKernel CreateBuilder(IServiceCollection services)
        {
            _kernel.Bind<IServiceCollection>().ToConstant(services);
            return _kernel;
        }


        public IServiceProvider CreateServiceProviderBuilder(IKernel containerBuilder)
        {
            containerBuilder.Bind<IServiceProvider>().ToMethod(ctx => new NinjectServiceProvider(containerBuilder));
            return containerBuilder.Get<IServiceProvider>();
        }

        public IServiceProvider CreateServiceProvider(IKernel containerBuilder)
        {
            containerBuilder.Load(new NinjectController());
            return containerBuilder.Get<IServiceProvider>();
        }
    }

    public class NinjectServiceProvider : IServiceProvider
    {
        private readonly IKernel _kernel;

        public NinjectServiceProvider(IKernel kernel)
        {
            _kernel = kernel;
        }

        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }
    }    
}