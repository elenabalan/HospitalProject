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
            References(x => x.Profession).Not.Nullable();
            Map(x => x.DateOfStart).Not.Nullable();
            Map(x => x.ProfessionalGrade).Not.Nullable();
            HasMany(x => x.ListSikcnessHistories).Inverse();
        }
    }
}
