
using Repository.Interfaces;
using Repository.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;

namespace Hospital.Infrastructure
{
    public class ServiceLocator
    {
        private static IKernel _kernel;
        public static void RegisterAll(IKernel kernel)
        {
            _kernel = kernel;
            _kernel.Register(Component.For(typeof(IRepository)).ImplementedBy(typeof(BaseRepository)));
            _kernel.Register(Component.For(typeof(IRepositoryDoctor)).ImplementedBy(typeof(RepositoryDoctor)));
            _kernel.Register(Component.For(typeof(IRepositoryCertificate)).ImplementedBy(typeof(RepositoryCertificate)));
            _kernel.Register(Component.For(typeof(IRepositorySicknessHistory)).ImplementedBy(typeof(RepositorySicknessHistory)));
            _kernel.Register(Component.For(typeof(IRepositoryPatient)).ImplementedBy(typeof(RepositoryPatient)));
          
        }
        public static T Get<T>()
        {
            return _kernel.Resolve<T>();
        }

     
    }
}
