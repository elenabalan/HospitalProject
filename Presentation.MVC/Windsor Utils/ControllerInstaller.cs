using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Presentation.MVC.Windsor_Utils
{
    public class ControllersInstaller : IWindsorInstaller
    {
        #region IWindsorInstaller Members

        /// <summary>
        /// Installation configuration
        /// </summary>
        /// <param name="container"></param>
        /// <param name="store"></param>
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(FindControllers().LifestyleTransient());
        }

        #endregion

        #region Non-public static members

        /// <summary>
        /// Find Controller classes
        /// </summary>
        /// <returns></returns>
        private static BasedOnDescriptor FindControllers()
        {
            return Classes.FromThisAssembly().BasedOn<IController>()
                          .If(t => t.Name.EndsWith("Controller"));
        }

        #endregion
    }
}