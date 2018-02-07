using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;

namespace BookEditorDemo
{
    // I'm using manual implementation here because I've had some troubles
    // wiring up official lib Ninject.MVC (And not only I, for this matter)
    public class NinjectResolver : IDependencyResolver, System.Web.Mvc.IDependencyResolver
    {
        private IKernel _kernel;

        public NinjectResolver(IKernel kernel)
        {
            _kernel = kernel;
        }

        public void Dispose()
        {
            _kernel = null;
        }

        public object GetService(Type serviceType)
        {
            var result = _kernel.TryGet(serviceType);
            return result;
        }

        public IEnumerable<object> GetServices(Type serviceType) => _kernel.GetAll(serviceType).ToArray();

        public IDependencyScope BeginScope() => new DepScope(this);

        class DepScope : IDependencyScope
        {
            private NinjectResolver _depsolver;

            public DepScope(NinjectResolver depsolver)
            {
                _depsolver = depsolver;
            }

            public void Dispose()
            {
                _depsolver = null;
            }

            public object GetService(Type serviceType) => _depsolver.GetService(serviceType);

            public IEnumerable<object> GetServices(Type serviceType) => _depsolver.GetServices(serviceType);
        }
    }
}