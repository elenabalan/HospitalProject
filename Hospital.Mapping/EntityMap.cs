using Domain;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Mapping
{
    public abstract class EntityMap<TEntity> : ClassMap<TEntity> where TEntity:Entity
    {
        protected EntityMap()
        {
            Id(x => x.Id).GeneratedBy.HiLo("50");

            DynamicUpdate();
        }
    }
}
