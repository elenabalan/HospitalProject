using System.ComponentModel;

namespace Common.Hospital
{
    public enum StatePatient
    {
        [Description("IsSick")]
        IsSick = 1,
        [Description("IsHealthy")]
        IsHealthy = 2,
        [Description("IsChronically")]
        IsChronically = 3,
        [Description("IsDead")]
        IsDead = 4
    }
}