using System.ComponentModel;

namespace Common.Hospital
{
    public enum SymptomSeverity
    {
        [Description("LETAL")]
        LETAL = 1,
        [Description("GRAVE")]
        GRAVE,
        [Description("LIGHT")]
        LIGHT
    }
}