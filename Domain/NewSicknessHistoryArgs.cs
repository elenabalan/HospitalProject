using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class NewStartSicknessEventArgs : EventArgs
    {
        public string NameSickness { get; }
        public DateTime StartDate { get; }
        public SicknessStateEnum State { get; }

        public NewStartSicknessEventArgs(string nameSickness, DateTime startDate, SicknessStateEnum _state)
        {
            NameSickness = nameSickness;
            StartDate = startDate;
            State = _state;
        }

    }
}
