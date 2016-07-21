using Domain.PersonInHospital;

namespace Domain.ChangeDoctor
{
    public class WeakDoctorQuitHandler
    {
        public event DoctorQuit QuitDoc;

        public virtual void OnDoctorQuit(NewDoctorQuitArgs a)
        {
            QuitDoc?.Invoke(a);
        }

        //public void SimulateQuitDoctor(Doctor docQuit,DateTime dQuit, Doctor dNew)
        //{
        //    NewDoctorQuitArgs e = new NewDoctorQuitArgs(docQuit ,dQuit, dNew);
        //    OnDoctorQuit(e);
        //}
    }
}
