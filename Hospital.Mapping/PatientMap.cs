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
           // Map(x => x.DateInHospital);
        }
    }
}
