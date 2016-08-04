using HibernatingRhinos.Profiler.Appender.NHibernate;
using Hospital.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Presentation
{
    class Client
    {
        static Client()
        {
            NHibernateProfiler.Initialize();
            ServiceLocator.RegisterAll();

        }
        static void Main(string[] args)
        {


        }
    }
}
