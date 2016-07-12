using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Domain
{
    public class WeakDoctorQuitHandler
    {
        public event DoctorQuit QuitDoc;

        public virtual void OnDoctorQuit(NewDoctorQuitArgs a)
        {
            if (QuitDoc != null)
            {
                QuitDoc(a);
            }
        }

        //public void SimulateQuitDoctor(Doctor docQuit,DateTime dQuit, Doctor dNew)
        //{
        //    NewDoctorQuitArgs e = new NewDoctorQuitArgs(docQuit ,dQuit, dNew);
        //    OnDoctorQuit(e);
        //}
    }
}
