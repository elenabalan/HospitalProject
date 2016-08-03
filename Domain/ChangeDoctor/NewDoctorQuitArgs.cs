using System;

namespace Domain.ChangeDoctor
{
    public class NewDoctorQuitArgs:EventArgs
    {
        public Doctor QuitDoctor { get; set; }
        public DateTime DateOut { get; set; }
        public Doctor NewDoctor { get; set; }

        public NewDoctorQuitArgs(Doctor quitD,DateTime d,Doctor newD)
        {
            QuitDoctor = quitD;
            DateOut = d;
            NewDoctor = newD;

        }
    }
}
