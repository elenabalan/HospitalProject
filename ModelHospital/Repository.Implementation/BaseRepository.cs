
using Hospital.Infrastructure;
using NHibernate;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Repository.Implementation
{
    public class BaseRepository : IRepository
    {
        protected readonly ISession _session = SessionGenerator.Instance.GetSession();


        public void Save<TEntity>(TEntity entity) where TEntity : Entity
        {
            using (ITransaction transaction = _session.BeginTransaction())
            {
                _session.SaveOrUpdate(entity);
                transaction.Commit();
            }
        }

        public void Delete<TEntity>(TEntity entity) where TEntity : Entity
        {
            using (ITransaction transaction = _session.BeginTransaction())
            {
                _session.Delete(entity);
                transaction.Commit();
            }
        }


        public void Update<TEntity>(TEntity entity) where TEntity : Entity
        {
            using (ITransaction transaction = _session.BeginTransaction())
            {
                _session.Update(entity);
                transaction.Commit();
            }
        }

        public TEntity Get<TEntity>(long id) where TEntity : Entity
        {
            return _session.Get<TEntity>(id);
        }
    }
}
