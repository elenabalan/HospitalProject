using System.ComponentModel;

namespace Common.Hospital
{
    public enum DificultyLevel
    {
        [Description ("EASY")]
        EASY = 1,
        [Description("MEDIUM")]
        MEDIUM = 2,
        [Description("HARD")]
        HARD = 3
    }
}