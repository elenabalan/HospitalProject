using Ninject;
using Repository.Interfaces;
using Repository.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Infrastructure
{
    public class ServiceLocator
    {
        static readonly IKernel Kernel = new StandardKernel();
        public static void RegisterAll()
        {
            Kernel.Bind<IRepository>().To<BaseRepository>();
            Kernel.Bind<IRepositoryDoctor>().To<RepositoryDoctor>();
            Kernel.Bind<IRepositoryCertificate>().To<RepositoryCertificate>();
            Kernel.Bind<IRepositorySicknessHistory>().To<RepositorySicknessHistory>();
            Kernel.Bind<IRepositoryPatient>().To<RepositoryPatient>();

        }
        public static T Resolver<T>()
        {
            return Kernel.Get<T>();
        }

     
    }
}
