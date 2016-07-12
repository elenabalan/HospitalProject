using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Domain
{
    public class SicknessHistory
    {
        public string NameSickness { get; }
        public SicknessStateEnum SicknessState { get; set; }
        public Patient Pat { get; }
        public Doctor Doc { get; set; }
        public DateTime DateStart { get; } = DateTime.Now;
        public DateTime? DateFinish { get; set; } = null;

        public static WeakDoctorQuitHandler QuitDocHandler = new WeakDoctorQuitHandler();
        public SicknessHistory(string nameSickness, SicknessStateEnum sicknessState, Patient pacient, Doctor doctor, DateTime? dateStart = null)
        {

            if (dateStart != null) DateStart = (DateTime)dateStart;
            Pat = pacient;
            Doc = doctor;
            NameSickness = nameSickness;
            SicknessState = sicknessState;

            QuitDocHandler.QuitDoc += ChangeDoctors;
        }
        public void CloseSicknessHistory(DateTime dateClose)
        {
            DateFinish = dateClose;
            SicknessState = SicknessStateEnum.OFF;
        }

        public void ChangeDoctors(NewDoctorQuitArgs args)
        {
            if (Doc == args .QuitDoctor)
                Doc = args .NewDoctor;
        }

        public override string ToString() => $"Doctor  {Doc}  {NameSickness} \n At the moment the sickness is {SicknessState}\n Start sickness date is {DateStart :d}\n Finish sickness date is {DateFinish :d}";
       
    }
}
