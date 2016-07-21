using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Domain
{
    public class Department
    { 
        public string departmentName { get; set; }
        public string idDepartment { get; set; }
        int nrStaff;

        public void AddStaff()
        {
            nrStaff++;
        }
    }
}