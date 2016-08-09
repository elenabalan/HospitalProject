using Domain;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Mapping
{
    public class PatientMap : SubclassMap<Patient>
    {
        public PatientMap()
        {
            References(x => x.DoctorResponsible);
            Map(x => x.LastDateInHospital).Not.Nullable();
            Map(x => x.State).Not.Nullable();
            HasMany(x => x.SickHistories).Cascade.SaveUpdate().Inverse();
        }
    }
}
