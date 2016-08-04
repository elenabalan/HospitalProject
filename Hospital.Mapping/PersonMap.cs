using Domain;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Mapping
{
    class PersonMap : SubclassMap<Person>
    {
        public PersonMap()
        {
            Map(x => x.Name);
            Map(x => x.Surname);
            
        }
    }
}
