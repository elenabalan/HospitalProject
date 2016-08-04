
using Hospital.Infrastructure;
using NHibernate;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implementation
{
    public class Repository : IRepository
    {
        protected readonly ISession _session = SessionGenerator.Instance.GetSession();


        public void Save<TEntity>(TEntity entity) where TEntity : global::Domain.Entity
        {
            using (ITransaction transaction = _session.BeginTransaction())
            {
                _session.SaveOrUpdate(entity);
                transaction.Commit();
            }
        }
        public void Delete<TEntity>(TEntity entity) where TEntity : global::Domain.Entity
        {
            using (ITransaction transaction = _session.BeginTransaction())
            {
                _session.Delete(entity);
                transaction.Commit();
            }
        }


    }
}
