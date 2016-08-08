using Domain;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Mapping
{
    public class PersonMap : EntityMap<Person>
    {
        public PersonMap()
        {
            Map(x => x.IDNP).Not.Nullable().Unique();
            Map(x => x.Name).Not.Nullable();
            Map(x => x.Surname).Not.Nullable();
            Map(x => x.BirthDate).Not.Nullable();
            Map(x => x.Gender).Not.Nullable();
            Map(x => x.AdressHome).Not.Nullable();
            Map(x => x.PhoneNumber).Not.Nullable();
        }
    }
}
