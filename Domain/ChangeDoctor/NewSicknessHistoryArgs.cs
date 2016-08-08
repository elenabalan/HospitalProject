using System;

namespace Domain.ChangeDoctor
{
    public class NewStartSicknessEventArgs : EventArgs
    {
        public string NameSickness { get; }
        public DateTime StartDate { get; }
   //     public SicknessStateEnum State { get; }

        public NewStartSicknessEventArgs(string nameSickness, DateTime startDate)
        {
            NameSickness = nameSickness;
            StartDate = startDate;
  //          State = _state;
        }

    }
}
