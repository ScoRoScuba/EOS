namespace EOS2.Model.Enums
{
    using System.ComponentModel;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1008:EnumsShouldHaveZeroValue", Justification = "Reference data match by value to db")]
    public enum ScheduleType
    {
        // System Accuracy Test
        [Description("[[[SAT]]]")]
        SAT = 1,

        // Temperature Uniformity Survey
        [Description("[[[TUS]]]")]
        TUS = 2
    }
}