using Ninject;
using Ninject.Modules;


namespace ProjectService
{
    public class NinjectServiceProvider : IServiceProvider, IDisposable
    {
        private readonly IKernel _kernel;

        public NinjectServiceProvider(params INinjectModule[] modules)
        {
            _kernel = new StandardKernel(modules);
        }

        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }

        public void Dispose()
        {
            _kernel.Dispose();
        }
    }
}