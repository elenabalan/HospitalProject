﻿using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Hospital.Mapping;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Infrastructure
{
    class SessionGenerator
    {
        #region Non-public members
        private SessionGenerator()
        {

        }

        private static readonly SessionGenerator _sessionGenerator = new SessionGenerator();
        private static readonly ISessionFactory SessionFactory = CreateSessionFactory();

        #endregion

        #region Non-public static members           CreateSessionFactory()
        private static ISessionFactory CreateSessionFactory()
        {
            FluentConfiguration configuration = Fluently.Configure()
                                                        .Database(MsSqlConfiguration.MsSql2012
                                                                                    .ConnectionString(
                                                                                        builder => 
                                                                                        builder.Database("HospitalProject")
                                                                                               .Server(@"MDDSK40054\SQLEXPRESS")
                                                                                               .TrustedConnection()))
                                                        .Mappings(
                                                            x => 
                                                            x.FluentMappings.AddFromAssembly(typeof(EntityMap<>).Assembly))
                                                        .ExposeConfiguration(
                                                            cfg => new SchemaUpdate(cfg).Execute(false, true));
            return configuration.BuildSessionFactory();
        }
        #endregion

        #region Public members

        public ISession GetSession()
        {
            return SessionFactory.OpenSession();
        }
        public static SessionGenerator Instance
        {
            get { return _sessionGenerator; }
        }
        #endregion
    }
}
