using System;
//using Common.Hospital;
using Domain.ChangeDoctor;
using Common.Hospital;
using Domain;

namespace Domain
{
    public class SicknessHistory : Entity
    {
        public virtual Sickness NameSickness { get; set; }
        //      public virtual SicknessStateEnum SicknessState { get; set; }
        public virtual Patient Patient { get; set; }
        public virtual Doctor Doctor { get; protected set; }
        public virtual DateTime StartDate { get; set; }
        public virtual DateTime? FinishDate { get; set; }

        public static WeakDoctorQuitHandler QuitDocHandler = new WeakDoctorQuitHandler();
        public SicknessHistory(Sickness nameSickness, Patient pacient,
                               Doctor doctor, DateTime? dateStart = null)
        {
            //var sickness = new Sickness();

            NameSickness = nameSickness;
            Patient = pacient;
            Doctor = doctor;

            StartDate = dateStart ?? DateTime.Now;
            //       SicknessState = sicknessState;
            QuitDocHandler.QuitDoc += ChangeDoctors;
        }

        [Obsolete]
        protected SicknessHistory()
        {
        }

        public virtual void CloseSicknessHistory(DateTime dateClose)
        {
            FinishDate = dateClose;
            //           SicknessState = SicknessStateEnum.OFF;
        }

        public virtual void ChangeDoctors(NewDoctorQuitArgs args)
        {
            if (Doctor == args.QuitDoctor)
                Doctor = args.NewDoctor;
        }

        public override string ToString() => $"Doctor  {Doctor}  {NameSickness} " +
                                             //                 $"\n At the moment the sickness is {SicknessState}\n" +
                                             $" Start sickness date is {StartDate:d}\n " +
                                             $"Finish sickness date is {FinishDate:d}";

    }
}
