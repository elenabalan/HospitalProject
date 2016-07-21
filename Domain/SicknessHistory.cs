using System;
using Domain.ChangeDoctor;
using Domain.PersonInHospital;

namespace Domain.Sicknesses
{
    public class SicknessHistory
    {
        public string NameSickness { get; }
        public SicknessStateEnum SicknessState { get; protected set; }
        public Patient Patient { get; }
        public Doctor Doctor { get; set; }
        public DateTime SicknessDateStart { get; } 
        public DateTime? SicknessDateFinish { get; set; }

        public static WeakDoctorQuitHandler QuitDocHandler = new WeakDoctorQuitHandler();
        public SicknessHistory(string nameSickness, SicknessStateEnum sicknessState, Patient pacient, 
                               Doctor doctor, DateTime? dateStart = null)
        {
            Patient = pacient;
            Doctor = doctor;
            NameSickness = nameSickness;
            SicknessState = sicknessState;
            SicknessDateStart = dateStart ?? DateTime.Now;
            QuitDocHandler.QuitDoc += ChangeDoctors;
        }
        public void CloseSicknessHistory(DateTime dateClose)
        {
            SicknessDateFinish = dateClose;
            SicknessState = SicknessStateEnum.OFF;
        }

        public void ChangeDoctors(NewDoctorQuitArgs args)
        {
            if (Doctor == args .QuitDoctor)
                Doctor = args .NewDoctor;
        }

        public override string ToString() => $"Doctor  {Doctor}  {NameSickness} " +
                                             $"\n At the moment the sickness is {SicknessState}\n" +
                                             $" Start sickness date is {SicknessDateStart :d}\n " +
                                             $"Finish sickness date is {SicknessDateFinish :d}";
       
    }
}
