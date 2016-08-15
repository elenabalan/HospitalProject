using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dto
{
    public class DoctorsWithExperienceDto
    {
        public long IdDoctor { get; set; }
        public int CountSicknessHistory { get; set; }
    }
}
