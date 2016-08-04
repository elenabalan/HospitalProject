using Domain;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Mapping
{
    class DoctorMap : SubclassMap<Doctor>
    {
        public DoctorMap()
        {
            Map(x => x.TipDoctor);
        }
    }
}
